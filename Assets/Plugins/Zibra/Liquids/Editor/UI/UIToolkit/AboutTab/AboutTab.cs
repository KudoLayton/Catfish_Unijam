using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using com.zibra.liquid.Foundation.UIElements;
#if ZIBRA_LIQUID_PAID_VERSION
using com.zibra.liquid.Editor.SDFObjects;
#endif

#if UNITY_2019_4_OR_NEWER
namespace com.zibra.liquid.Plugins.Editor
{
    internal class AboutTab : BaseTab
    {
#if ZIBRA_LIQUID_PAID_VERSION
        private TextField m_AuthKeyInputField;
        private Label m_ValidationProgressLabel;
        private Label m_RegisteredKeyLabel;
#endif

        private const int KEY_LENGTH = 36;

        public AboutTab() : base($"{ZibraAIPackage.UIToolkitPath}/AboutTab/AboutTab")
        {
            VisualElement registrationBlock = this.Q<SettingsBlock>("registrationBlock");
#if ZIBRA_LIQUID_PAID_VERSION
            Button checkAuthKeyBtn = this.Q<Button>("validateAuthKeyBtn");
            Button registerAuthKeyBtn = this.Q<Button>("registerKeyBtn");
            m_AuthKeyInputField = this.Q<TextField>("authKeyInputField");
            m_ValidationProgressLabel = this.Q<Label>("validationProgress");
            m_RegisteredKeyLabel = this.Q<Label>("registeredKeyLabel");

            ZibraServerAuthenticationManager.GetInstance();
            registerAuthKeyBtn.clicked += OnRegisterAuthKeyBtnOnClickedHandler;
            m_AuthKeyInputField.value = ZibraServerAuthenticationManager.GetInstance().PluginLicenseKey;
            checkAuthKeyBtn.clicked += OnAuthKeyBtnOnClickedHandler;
            // Hide if key is valid.
            if (ZibraServerAuthenticationManager.GetInstance().GetStatus() ==
                ZibraServerAuthenticationManager.Status.OK)
            {
                registerAuthKeyBtn.style.display = DisplayStyle.None;
                checkAuthKeyBtn.style.display = DisplayStyle.None;
                m_AuthKeyInputField.style.display = DisplayStyle.None;
                m_RegisteredKeyLabel.style.display = DisplayStyle.Flex;
            }
            else
            {
                m_RegisteredKeyLabel.style.display = DisplayStyle.None;
            }

#if ZIBRA_LIQUID_PRO_VERSION
            registerAuthKeyBtn.style.display = DisplayStyle.None;
#endif
            m_ValidationProgressLabel.style.display = DisplayStyle.None;

#else
            registrationBlock.style.display = DisplayStyle.None;
#endif
        }

#if ZIBRA_LIQUID_PAID_VERSION
        private void OnRegisterAuthKeyBtnOnClickedHandler()
        {
            Application.OpenURL("https://registration.zibra.ai/");
        }

        private void OnAuthKeyBtnOnClickedHandler()
        {
            string key = m_AuthKeyInputField.text.Trim();

            if (key.Length == KEY_LENGTH)
            {
                ZibraServerAuthenticationManager.GetInstance().RegisterKey(m_AuthKeyInputField.text);
                m_RegisteredKeyLabel.style.display = DisplayStyle.None;
            }
            else
            {
                EditorUtility.DisplayDialog("Zibra Liquid Key Error", "Incorrect key format.", "Ok");
            }
        }
#endif
    }
}
#endif