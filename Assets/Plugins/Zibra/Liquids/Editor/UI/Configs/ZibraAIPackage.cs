namespace com.zibra.liquid
{
    internal static class ZibraAIPackage
    {
        internal const string ZibraAiSupportEmail = "support@zibra.ai";
        internal const string ZibraAiCeoEMail = "hello@zibra.ai";
        internal const string ZibraAiWebsiteRootUrl = "https://zibra.ai/";

        internal const string PackageName = "com.zibraai.liquid";
        internal const string DisplayName = "Zibra Liquids";
        internal const string RootMenu = "Zibra AI/" + DisplayName + "/";

        internal static readonly string RootPath = "Assets/Plugins/Zibra/Liquids";

        internal static readonly string WindowTabsPath = $"{RootPath}/Editor/UI/Window/Tabs";

        internal static readonly string UIToolkitPath = $"{RootPath}/Editor/UI/UIToolkit";
        internal static readonly string UIToolkitControlsPath = $"{UIToolkitPath}/Controls";

        internal static readonly string EditorArtAssetsPath = $"{RootPath}/Editor/UI/Art";
        internal static readonly string EditorIconAssetsPath = $"{EditorArtAssetsPath}/Icons";
        internal static readonly string EditorFontAssetsPath = $"{EditorArtAssetsPath}/Fonts";
    }
}
