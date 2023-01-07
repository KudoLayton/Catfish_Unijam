using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using com.zibra.liquid.Solver;

namespace com.zibra.liquid.Editor
{
    internal class ZibraWelcomeWindow : EditorWindow
    {
        public static GUIContent WindowTitle => new GUIContent("Zibra Liquids Welcome Screen");

        internal static void ShowWindowDelayed()
        {
            ShowWindow();
            EditorApplication.update -= ShowWindowDelayed;
        }

        [InitializeOnLoadMethod]
        internal static void InitializeOnLoad()
        {
            string PrefsKey = "ZibraLiquidsWelcomeScreenSeen";

            PrefsKey += ZibraLiquid.PluginVersion;

#if ZIBRA_LIQUID_FREE_VERSION
            PrefsKey += "Free";
#endif
#if ZIBRA_LIQUID_PRO_VERSION
            PrefsKey += "Pro";
#endif

            if (!EditorPrefs.GetBool(PrefsKey))
            {
                EditorApplication.update += ShowWindowDelayed;
                EditorPrefs.SetBool(PrefsKey, true);
            }
        }

        [MenuItem("Zibra AI/Zibra Liquids/Open Welcome Screen", false, 0)]
        private static void ShowWindow()
        {
            ZibraWelcomeWindow window = (ZibraWelcomeWindow)GetWindow(typeof(ZibraWelcomeWindow));
            window.titleContent = WindowTitle;
            window.Show();
        }

        private void OnEnable()
        {
            // Reference to the root of the window.
            var root = rootVisualElement;

            int width = 480;
            int height = 609;

#if ZIBRA_LIQUID_FREE_VERSION
            height += 25;
#endif
            minSize = maxSize = new Vector2(width, height);

            var uxmlAssetPath = AssetDatabase.GUIDToAssetPath("4a3534c7503328247ab95f240be69432");
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlAssetPath);
            visualTree.CloneTree(root);

            var commonUSSAssetPath = AssetDatabase.GUIDToAssetPath("08d62ea1d249d9d4a98499f0cb1b6c3b");
            var commonStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(commonUSSAssetPath);
            root.styleSheets.Add(commonStyleSheet);

#if ZIBRA_LIQUID_FREE_VERSION
            var versionSpecificUSSAssetPath = AssetDatabase.GUIDToAssetPath("aec7970fdff804e428d1ef07405b5c80");
#else
            var versionSpecificUSSAssetPath = AssetDatabase.GUIDToAssetPath("1facc61fe8c80a74881451154e98d65c");
#endif
            var versionSpecificStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(versionSpecificUSSAssetPath);
            root.styleSheets.Add(versionSpecificStyleSheet);

            root.Q<Button>("Info").clicked += InfoClick;
            root.Q<Button>("OnePager").clicked += OnePagerClick;
            root.Q<Button>("Discord").clicked += DiscordClick;
            root.Q<Button>("E-Mail").clicked += EmailClick;
            root.Q<Button>("User Guide").clicked += UserGuideClick;
            root.Q<Button>("API Reference").clicked += APIReferenceClick;
            root.Q<Button>("Changelog").clicked += ChangelogClick;
            root.Q<Button>("Tutorials").clicked += TutorialsClick;
            root.Q<Button>("TutorialScenes").clicked += TutorialScenesClick;
            root.Q<Button>("FullVersion").clicked += FullVersionClick;
        }

        private void InfoClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Info");
        }

        private void OnePagerClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Plugin Description");
        }

        private void DiscordClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Contact us/Discord");
        }

        private void EmailClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Contact us/Support E-Mail");
        }

        private void UserGuideClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Open User Guide");
        }

        private void APIReferenceClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Open API Reference");
        }

        private void ChangelogClick()
        {
            EditorApplication.ExecuteMenuItem("Zibra AI/Zibra Liquids/Open Changelog");
        }

        private void TutorialsClick()
        {
            Application.OpenURL("https://bit.ly/3K113vk");
        }

        private void TutorialScenesClick()
        {
#if ZIBRA_LIQUID_FREE_VERSION
            Application.OpenURL(
                "https://zibra.ai/get-zibra-liquids-tutorials/?cgi_idsr_=&utm_source=tutorials&utm_campaign=zibra_asset_free");
#else
            Application.OpenURL("https://bit.ly/3c1zsh1");
#endif
        }

        private void FullVersionClick()
        {
            Application.OpenURL(
                "https://assetstore.unity.com/packages/tools/physics/zibra-liquids-200718?aid=1011lmkNI&pubref=freeversion");
        }
    }
}