using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// Represents a mod package containing basic information essential for compatibility checks.
/// </summary>
public struct ModPackageUser
{
    ///<summary> The name of the mod. </summary>
    public string Name;
    ///<summary> The description of the mod. </summary>
    public string Description;
    ///<summary> The author of the mod. </summary>
    public string Author;
    ///<summary> The version of the mod. </summary>
    public string Version;
    ///<summary> The version of unity. </summary>
    public string UnityVersion;
    ///<summary> The name of the asset. </summary>
    public string AssetName;
    ///<summary> The name of the asset. </summary>
    public string BundleName;
}

public class CreateBundleTool : EditorWindow
{
    ///<summary> Enumeration representing build targets for asset bundles. </summary>
    private enum CustomBuildTarget
    {
        StandaloneWindows,
        StandaloneLinux64,
        StandaloneOSX
    }

    ///<summary> Selected asset to bundle. </summary>
    private Object _Asset;
    ///<summary> ame of the folder where bundles are saved. </summary>
    private string _FolderName = "Folder Name";
    ///<summary> Name of the asset bundle to create. </summary>
    private string _BundleName = "Bundle Name";
    ///<summary> Target platform for the bundle. </summary>
    private CustomBuildTarget _BuildTarget = CustomBuildTarget.StandaloneWindows;
    ///<summary>  Path where the bundle will be saved. </summary>
    private string _BundlePath = "BundlesFolder";
    ///<summary> Index of the currently selected tab in the editor window. </summary>
    private int _CurrentTab = 0;
    ///<summary> Labels for different tabs in the editor window. </summary>
    private GUIContent[] _TabLabels = new GUIContent[] { new GUIContent("Bundle"), new GUIContent("JSON") };
    ///<summary> Instance of mod package. </summary>
    private ModPackageUser _ModPackage;

    /// <summary>
    /// Shows the editor window for creating asset bundles.
    /// </summary>
    [MenuItem("Modding Toolkit/Create Asset Bundle")]
    private static void ShowWindow()
    {
        GetWindow<CreateBundleTool>("Create Asset Bundle");
    }

    /// <summary>
    /// Loads previously saved values from EditorPrefs when the window is enabled.
    /// </summary>
    private void OnEnable()
    {
        _ModPackage = new ModPackageUser()
        {
            Name = EditorPrefs.GetString("ModPackage_Name", " Mod Name"),
            Description = EditorPrefs.GetString("ModPackage_Description", "Mod Description"),
            Author = EditorPrefs.GetString("ModPackage_Author", "Author Name"),
            Version = EditorPrefs.GetString("ModPackage_Version", "1.0"),
            UnityVersion = EditorPrefs.GetString("ModPackage_UnityVersion", "1.0"),
            AssetName = EditorPrefs.GetString("ModPackage_AssetName", ""),
            BundleName = EditorPrefs.GetString("ModPackage_BundleName", "")
        };
    }

    /// <summary>
    /// Saves current values to EditorPrefs when the window is disabled.
    /// </summary>
    private void OnDisable()
    {
        EditorPrefs.SetString("ModPackage_Name", _ModPackage.Name);
        EditorPrefs.SetString("ModPackage_Description", _ModPackage.Description);
        EditorPrefs.SetString("ModPackage_Author", _ModPackage.Author);
        EditorPrefs.SetString("ModPackage_Version", _ModPackage.Version);
        EditorPrefs.SetString("ModPackage_UnityVersion", _ModPackage.UnityVersion);
        EditorPrefs.SetString("ModPackage_AssetName", _ModPackage.AssetName);
        EditorPrefs.SetString("ModPackage_BundleName", _ModPackage.BundleName);
    }

    /// <summary>
    /// Draws the editor window GUI.
    /// </summary>
    private void OnGUI()
    {
        _CurrentTab = GUILayout.Toolbar(_CurrentTab, _TabLabels);

        switch (_CurrentTab)
        {
            case 0:
                DrawBundleTab();
                break;
            case 1:
                DrawJsonTab();
                break;
        }
    }

    /// <summary>
    /// Draws the bundle creation tab in the editor window.
    /// </summary>
    private void DrawBundleTab()
    {
        GUILayout.Label("Create Asset Bundle", EditorStyles.boldLabel);

        // Select the asset to bundle
        Object newAsset = EditorGUILayout.ObjectField(new GUIContent("Asset to Bundle", "Select the asset to add to the bundle."), _Asset, typeof(Object), false);

        if (newAsset != _Asset)
        {
            if (CheckIfScript(newAsset))
            {
                Debug.LogError("Scripts cannot be included in AssetBundles.");
                _Asset = null;
            }
            else
            {
                _Asset = newAsset;
            }
        }

        if (_Asset != null)
        {
            _ModPackage.AssetName = _Asset.name;
            _ModPackage.BundleName = _BundleName;

            if (CheckForScripts(_Asset))
            {
                EditorGUILayout.HelpBox("The selected GameObject has scripts attached. Scripts cannot be included in AssetBundles.", MessageType.Error);
            }
        }
        else
        {
            _ModPackage.AssetName = string.Empty;
            _ModPackage.BundleName = string.Empty;
        }

        // Display asset name (disabled field)
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(new GUIContent("Asset Name", "The Name of the selected asset."), _ModPackage.AssetName);
        EditorGUI.EndDisabledGroup();

        // Choose folder and bundle name
        EditorGUILayout.BeginHorizontal();
        _FolderName = EditorGUILayout.TextField(new GUIContent("Folder Name", "Name of the folder where you want to save the bundles."), _FolderName);
        if (GUILayout.Button("Select Folder", GUILayout.Width(100)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                _FolderName = new DirectoryInfo(selectedPath).Name;
                _BundlePath = selectedPath;
            }
        }
        EditorGUILayout.EndHorizontal();

        _BundleName = EditorGUILayout.TextField(new GUIContent("Bundle Name", "Name of the bundle to be created."), _BundleName);

        // Choose build target platform
        _BuildTarget = (CustomBuildTarget)EditorGUILayout.EnumPopup(new GUIContent("Build Target", "Select the target platform for the bundle."), _BuildTarget);

        // Button to create the bundle
        if (GUILayout.Button("Create Bundle"))
        {
            CreateBundle();
        }
    }

    /// <summary>
    /// Draws the JSON configuration tab in the editor window.
    /// </summary>
    private void DrawJsonTab()
    {
        GUILayout.Label("JSON Configuration", EditorStyles.boldLabel);

        _ModPackage.Name = EditorGUILayout.TextField(new GUIContent("Name", "The name of the mod."), _ModPackage.Name);
        _ModPackage.Description = EditorGUILayout.TextField(new GUIContent("Description", "The description of the mod."), _ModPackage.Description);
        _ModPackage.Author = EditorGUILayout.TextField(new GUIContent("Author", "The author of the mod."), _ModPackage.Author);
        _ModPackage.Version = EditorGUILayout.TextField(new GUIContent("Version", "The version of the mod."), _ModPackage.Version);
        _ModPackage.UnityVersion = EditorGUILayout.TextField(new GUIContent("UnityVersion", "The version of the unity."), _ModPackage.UnityVersion);
    }

    /// <summary>
    /// Checks if the given object is a script.
    /// </summary>
    private bool CheckIfScript(Object _obj)
    {
        if (_obj == null) return false;

        string assetPath = AssetDatabase.GetAssetPath(_obj);
        return assetPath.EndsWith(".cs");
    }

    /// <summary>
    /// Checks if the given object has scripts attached.
    /// </summary>
    private bool CheckForScripts(Object _obj)
    {
        if (_obj is GameObject)
        {
            GameObject go = _obj as GameObject;
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
            return scripts.Length > 0;
        }
        return false;
    }

    /// <summary>
    /// Creates the asset bundle.
    /// </summary>
    private void CreateBundle()
    {
        if (_Asset == null)
        {
            Debug.LogError("Please select an asset to bundle.");
            return;
        }

        // Get asset path
        string assetPath = AssetDatabase.GetAssetPath(_Asset);
        // Set default bundle path if not specified
        if (_BundlePath == null)
        {
            _BundlePath = Path.Combine(Application.dataPath, "AssetBundles");
        }


        // Create directory for the bundle
        string bundleDirectory = Path.Combine(_BundlePath, _BundleName, "Bundle");
        if (Directory.Exists(bundleDirectory))
        {
            Debug.LogWarning($"Directory '{bundleDirectory}' already exists. Please choose a different name for the folder.");
            return;
        }

        Directory.CreateDirectory(bundleDirectory);

        // Build the asset bundle
        BuildPipeline.BuildAssetBundles(bundleDirectory, new AssetBundleBuild[]
        {
            new AssetBundleBuild
            {
                assetBundleName = _BundleName,
                assetNames = new[] { assetPath }
            }
        }, BuildAssetBundleOptions.None, CustomBuild(_BuildTarget));

        Debug.Log($"Bundle created at {bundleDirectory}/{_BundleName}.bundle");

        // Create JSON file with mod package details
        string bundleDirectoryForJson = Path.Combine(_BundlePath, _BundleName);
        CreateJsonFile(bundleDirectoryForJson);
    }

    /// <summary>
    /// Creates a JSON file containing mod package details.
    /// </summary>
    private void CreateJsonFile(string _bundleDirectory)
    {
        string json = JsonUtility.ToJson(_ModPackage, true);
        File.WriteAllText(Path.Combine(_bundleDirectory, "modInfo.json"), json);

        Debug.Log("JSON file created at " + Path.Combine(_bundleDirectory, "modInfo.json"));
    }

    /// <summary>
    /// Converts custom build target enum to Unity BuildTarget.
    /// </summary>
    private BuildTarget CustomBuild(CustomBuildTarget _customBuildTarget)
    {
        switch (_customBuildTarget)
        {
            case CustomBuildTarget.StandaloneWindows:
                return BuildTarget.StandaloneWindows;
            case CustomBuildTarget.StandaloneLinux64:
                return BuildTarget.StandaloneLinux64;
            case CustomBuildTarget.StandaloneOSX:
                return BuildTarget.StandaloneOSX;
            default:
                return BuildTarget.StandaloneWindows;
        }
    }
}
