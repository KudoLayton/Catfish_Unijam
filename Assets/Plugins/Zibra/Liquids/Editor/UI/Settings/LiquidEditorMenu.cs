#if UNITY_2019_4_OR_NEWER
using UnityEditor;
using UnityEngine;

namespace com.zibra.liquid
{
    /// <summary>
    /// Class that contains code for useful actions for the plugin
    /// Those actions exposed to user via MenuItem
    /// You can call them from C# via ExecuteMenuItem
    /// </summary>
    internal static class LiquidEditorMenu
    {
        [MenuItem(ZibraAIPackage.RootMenu + "Info", false, 15)]
        internal static void OpenSettings()
        {
            var windowTitle = LiquidSettingsWindow.WindowTitle;
            LiquidSettingsWindow.ShowTowardsInspector(windowTitle.text, windowTitle.image);
        }

        internal static void OpenFile(string GUID)
        {
            string dataPath = Application.dataPath;
            string projectPath = dataPath.Replace("/Assets", "");
            string filePath = AssetDatabase.GUIDToAssetPath(GUID);
            Application.OpenURL("file://" + projectPath + "/" + filePath);
        }

        [MenuItem(ZibraAIPackage.RootMenu + "Plugin Description", false, 29)]
        internal static void OpenPluginDescription()
        {
            OpenFile("e7724bb63ee284c97bdd4fa86f528106");
        }

        [MenuItem(ZibraAIPackage.RootMenu + "Open User Guide", false, 30)]
        internal static void OpenUserGuide()
        {
            OpenFile("09ace81bf2ac0bd4e8c853cda11f7c84");
        }

        [MenuItem(ZibraAIPackage.RootMenu + "Open API Reference", false, 31)]
        internal static void OpenAPIDocumentation()
        {
            string dataPath = Application.dataPath;
            string projectPath = dataPath.Replace("/Assets", "");
            string documentationPath = AssetDatabase.GUIDToAssetPath("d9e57e1e9783349ffa44b5f943410fab");
            Application.OpenURL("file://" + projectPath + "/" + documentationPath + "/index.html");
        }

#if UNITY_EDITOR_WIN
        [MenuItem(ZibraAIPackage.RootMenu + "Open API Reference (Old school)", false, 32)]
        internal static void OpenAPIDocumentationCHM()
        {
            OpenFile("0924fc1a7bd63a94988339b3f572f7ae");
        }
#endif

        [MenuItem(ZibraAIPackage.RootMenu + "Open Changelog", false, 33)]
        internal static void OpenChangelog()
        {
            OpenFile("b667af1f31c554a3299ea0e7db5ad45a");
        }

        [MenuItem(ZibraAIPackage.RootMenu + "Contact us/Discord", false, 1000)]
        internal static void OpenDiscord()
        {
#if ZIBRA_LIQUID_FREE_VERSION
            Application.OpenURL("https://discord.gg/Gs6XSrpZbG");
#else
            Application.OpenURL("https://discord.gg/QzypP8n7uB");
#endif
        }

        [MenuItem(ZibraAIPackage.RootMenu + "Contact us/Support E-Mail", false, 1010)]
        internal static void OpenSupportEmail()
        {
            Application.OpenURL("mailto:support@zibra.ai");
        }
    }
}
#endif
