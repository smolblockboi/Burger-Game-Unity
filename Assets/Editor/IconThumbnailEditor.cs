using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    private Vector3Field m_cameraRotationField;

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
        m_cameraRotationField = rootVisualElement.Q<Vector3Field>("CameraRotation");


        m_list.SetSelection(0);

    }

    private void OnCameraRotationChange(ChangeEvent<Vector3> evt)
    {

    }
    private void OnCameraPositionChange(ChangeEvent<Vector3> evt)
    {

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
            m_cameraObject.transform.position = new Vector3(0, 0, -2);
            m_cameraObject.transform.eulerAngles = new Vector3(0, 0, 0);

            m_sceneCamera = m_cameraObject.AddComponent<Camera>();

            m_sceneCamera.aspect = 1f;
            m_sceneCamera.backgroundColor = Color.black;
            m_sceneCamera.clearFlags = CameraClearFlags.SolidColor;
            m_sceneCamera.targetTexture = new RenderTexture(m_size, m_size, 32, RenderTextureFormat.ARGBFloat);

            SceneManager.MoveGameObjectToScene(m_cameraObject, m_previewScene);

            m_sceneCamera.scene = m_previewScene;
        }

        if (m_instance != null)
        {
            Debug.Log("Destroyed " + m_instance.name);
            DestroyImmediate(m_instance.gameObject);
        }

        GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("IngredientObject"), m_previewScene);

        m_instance = newGameObject.GetComponent<InteractableIngredientObject>();
        m_instance.itemData = m_selectedAsset;
        m_instance.ChangeData(m_selectedAsset);

        m_instance.gameObject.transform.rotation = Quaternion.identity;
        m_instance.gameObject.transform.position = Vector3.zero;

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
