using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.IO;

public class IconThumbnailEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Tools/Icon Editor")]
    public static void ShowExample()
    {
        IconThumbnailEditor wnd = GetWindow<IconThumbnailEditor>();
        wnd.titleContent = new GUIContent("IconThumbnailEditor");
    }

    private ListView m_list;
    private List<ItemData> m_items;

    [SerializeField]
    private ItemData m_selectedAsset;
    [SerializeField]
    private Texture2D m_previewTexture;

    //Preview
    private int m_size = 512;
    private Scene m_previewScene;
    private GameObject m_cameraObject;
    private Camera m_sceneCamera;
    private InteractableIngredientObject m_instance;
    private Vector3Field m_cameraPositionField;
    private Vector3Field m_objectRotation;
    private Button m_saveBtn;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;


        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        m_list = rootVisualElement.Q<ListView>("List");
        m_items = new List<ItemData>();

        string[] itemGuids = AssetDatabase.FindAssets("t:ItemData");
        foreach(string guid in itemGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            m_items.Add(itemData);
        }

        m_list.itemsSource = m_items;
        m_list.selectionChanged += OnSelectItem;

        rootVisualElement.Q<VisualElement>("Content").dataSource = this;

        m_cameraPositionField = rootVisualElement.Q<Vector3Field>("CameraPosition");
        m_objectRotation = rootVisualElement.Q<Vector3Field>("ObjectRotation");

        m_saveBtn = rootVisualElement.Q<Button>("SaveBtn");
        m_saveBtn.clicked += Export;

        m_cameraPositionField.RegisterValueChangedCallback(OnCameraPositionChange);
        m_objectRotation.RegisterValueChangedCallback(OnRotationChange);

        m_list.SetSelection(0);

    }

    private void OnCameraPositionChange(ChangeEvent<Vector3> evt)
    {
        m_cameraObject.transform.position = evt.newValue;
        UpdateCamera();
    }
    private void OnRotationChange(ChangeEvent<Vector3> evt)
    {
        m_instance.transform.eulerAngles = evt.newValue;
        UpdateCamera();
    }
    private void Export()
    {
        m_sceneCamera.depthTextureMode = DepthTextureMode.Depth;
        m_sceneCamera.backgroundColor = new Color(0, 0, 0, 0);

        UpdateCamera();

        SaveTextureAsPNG(m_previewTexture, m_selectedAsset.name);

        m_sceneCamera.backgroundColor = Color.black;

        UpdateCamera();
    }
    private void SaveTextureAsPNG(Texture2D texture, string fileName)
    {
        if (texture == null)
        {
            Debug.LogError("No texture provided to save.");
            return;
        }

        string path = EditorUtility.SaveFilePanel("Save Texture As PNG", "", $"{fileName}.png", "png");

        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("Save operation cancelled.");
            return;
        }

        byte[] pngData = texture.EncodeToPNG();
        if (pngData != null)
        {
            File.WriteAllBytes(path, pngData);
            Debug.Log("Texture saved to: " + path);
        }
        else
        {
            Debug.Log("Failed to encode texture to PNG.");
        }
    }
    private void OnSelectItem(object item)
    {
        m_selectedAsset = m_items[m_list.selectedIndex];

        if (!m_previewScene.IsValid())
        {
            m_previewScene = EditorSceneManager.NewPreviewScene();
        }
        if(m_cameraObject == null)
        {
            m_cameraObject = new GameObject("Camera");
            m_cameraObject.transform.position = new Vector3(0, 0, -1);
            m_cameraObject.transform.eulerAngles = new Vector3(0, 0, 0);

            m_sceneCamera = m_cameraObject.AddComponent<Camera>();

            m_sceneCamera.aspect = 1f;
            m_sceneCamera.backgroundColor = Color.black;
            m_sceneCamera.clearFlags = CameraClearFlags.SolidColor;
            m_sceneCamera.targetTexture = new RenderTexture(m_size, m_size, 32, RenderTextureFormat.ARGBFloat);

            SceneManager.MoveGameObjectToScene(m_cameraObject, m_previewScene);

            m_sceneCamera.scene = m_previewScene;

            m_cameraPositionField.value = m_cameraObject.transform.position;
        }

        if (m_instance != null)
        {
            Debug.Log("Destroyed " + m_instance.name);
            DestroyImmediate(m_instance.gameObject);
        }

        GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("IngredientObjectPreview"), m_previewScene);

        m_instance = newGameObject.GetComponent<InteractableIngredientObject>();
        m_instance.itemData = m_selectedAsset;
        m_instance.ChangeData(m_selectedAsset);

        MeshFilter meshFilter = newGameObject.GetComponent<MeshFilter>();
        Bounds bounds = meshFilter.sharedMesh.bounds;

        m_instance.gameObject.transform.eulerAngles = m_objectRotation.value;
        m_instance.gameObject.transform.position = new Vector3(0, -bounds.center.y, 0);

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        m_sceneCamera.Render();

        if(m_previewTexture == null)
        {
            m_previewTexture = new Texture2D(m_size, m_size, TextureFormat.ARGB32, false);
        }

        RenderTexture.active = m_sceneCamera.targetTexture;

        m_previewTexture.ReadPixels(new Rect(0, 0, m_size, m_size), 0, 0);
        m_previewTexture.Apply();

        RenderTexture.active = null;
    }
}
