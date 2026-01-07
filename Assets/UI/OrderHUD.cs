using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class OrderHUD : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset orderSlipTemplate;

    private VisualElement uiRoot;
    private TemplateContainer orderSlipsContainer;
    private VisualElement orderPanel;

    private void OnEnable()
    {
        UIDocument UIDocument = GetComponent<UIDocument>();

        if (UIDocument != null)
        {
            uiRoot = UIDocument.rootVisualElement;

        }

        orderSlipsContainer = uiRoot.Q<TemplateContainer>("OrderSlipsContainer");
        orderPanel = orderSlipsContainer.Q<VisualElement>("OrderPanel");
    }

    public void OnOrderGenerated(OrderData orderData)
    {
        InstantiateOrderSlip(orderData);

        Debug.Log("HUD received burger order");
    }

    public void OnOrderCompleted(int index)
    {
        orderPanel.RemoveAt(index);
    }

    private void InstantiateOrderSlip(OrderData orderData)
    {
        VisualElement newSlip = orderSlipTemplate.Instantiate();

        VisualElement ingredientsGroup = newSlip.Q<VisualElement>("Ingredients");

        List<string> orderStrings = new();

        foreach (var item in orderData.burgerData.ingredients)
        {
            orderStrings.Add(item.internalID);
        }

        List<string> slipStrings = new();

        foreach (var item in ingredientsGroup.hierarchy.Children())
        {
            item.visible = false;

            slipStrings.Add(item.name);

            if (orderStrings.Contains(item.name))
            {
                item.visible = true;
            }
            else
            {
                item.style.display = DisplayStyle.None;
            }
        }

        Debug.Log(string.Join(", ", orderStrings));
        Debug.Log(string.Join(", ", slipStrings));

        Label orderNumberLabel = newSlip.Q<Label>("OrderLabel");
        orderNumberLabel.bindingPath = "orderNumber";

        newSlip.Bind(new SerializedObject(orderData));

        orderPanel.Add(newSlip);
    }

}
