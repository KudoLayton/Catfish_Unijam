#if UNITY_2019_4_OR_NEWER
using com.zibra.liquid.Plugins.Editor;
using UnityEngine;
using UnityEngine.UIElements;
#if ZIBRA_LIQUID_PAID_VERSION
using com.zibra.liquid.Editor.SDFObjects;
#endif

namespace com.zibra.liquid
{
    internal class LiquidSettingsWindow : PackageSettingsWindow<LiquidSettingsWindow>
    {
        internal override IPackageInfo GetPackageInfo() => new ZibraAiPackageInfo();

#if ZIBRA_LIQUID_PAID_VERSION
        private ZibraServerAuthenticationManager.Status LatestStatus =
            ZibraServerAuthenticationManager.Status.NotInitialized;
#endif

        protected override void OnWindowEnable(VisualElement root)
        {
            AddTab("Info", new AboutTab());
        }

        internal void Update()
        {
#if ZIBRA_LIQUID_PAID_VERSION
            if (ZibraServerAuthenticationManager.GetInstance().GetStatus() == LatestStatus)
                return;

            LatestStatus = ZibraServerAuthenticationManager.GetInstance().GetStatus();

            if (LatestStatus == ZibraServerAuthenticationManager.Status.OK)
            {
                m_Tabs["Info"].Q<Button>("registerKeyBtn").style.display = DisplayStyle.None;
                m_Tabs["Info"].Q<Button>("validateAuthKeyBtn").style.display = DisplayStyle.None;
                m_Tabs["Info"].Q<TextField>("authKeyInputField").style.display = DisplayStyle.None;
                m_Tabs["Info"].Q<Label>("registeredKeyLabel").style.display = DisplayStyle.Flex;
                m_Tabs["Info"].Q<Label>("validationProgress").style.display = DisplayStyle.None;
            }
            else
            {
                m_Tabs["Info"].Q<Label>("validationProgress").text =
                    ZibraServerAuthenticationManager.GetInstance().GetErrorMessage();
                m_Tabs["Info"].Q<Label>("validationProgress").style.display = DisplayStyle.Flex;
                m_Tabs["Info"].Q<Label>("registeredKeyLabel").style.display = DisplayStyle.None;
            }
#endif
        }

        internal static GUIContent WindowTitle => new GUIContent(ZibraAIPackage.DisplayName);
    }
}
#endif