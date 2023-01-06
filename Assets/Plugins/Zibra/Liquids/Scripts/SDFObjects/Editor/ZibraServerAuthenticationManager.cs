#if ZIBRA_LIQUID_PAID_VERSION && UNITY_EDITOR

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using com.zibra.liquid.Solver;

namespace com.zibra.liquid.Editor.SDFObjects
{
    /// <summary>
    ///     (Editor only, Unavailable in Free version) Class responsible for managing licensing
    ///     and allowing server communication.
    /// </summary>
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    [InitializeOnLoad]
    public class ZibraServerAuthenticationManager
    {
#region Public Interface
        /// <summary>
        ///     License key used for the plugin.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Not neccecarily correct.
        ///         Use <see cref="IsLicenseVerified"/> to check for that.
        ///     </para>
        ///     <para>
        ///         May be empty.
        ///     </para>
        /// </remarks>
        public string PluginLicenseKey { get; private set; } = "";

        /// <summary>
        ///     URL adress for generation API.
        /// </summary>
        /// <remarks>
        ///     Includes license key in the URL.
        /// </remarks>
        public string GenerationURL { get; private set; } = "";

        /// <summary>
        ///     Status of license validation.
        /// </summary>
        public enum Status
        {
            OK,
            KeyValidationInProgress,
            NetworkError,
            NotRegistered,
            NotInitialized,
#if ZIBRA_LIQUID_PRO_VERSION
            NoMaintance,
            Expired,
#endif
        }

        /// <summary>
        ///     Returns current status of license validation.
        /// </summary>
        /// <remarks>
        ///     Never returns NotInitialized, since it initialized validation in this case.
        /// </remarks>
        public Status GetStatus()
        {
            if (CurrentStatus == Status.NotInitialized)
            {
                Initialize();
            }

            return CurrentStatus;
        }

        /// <summary>
        ///     Checks whether license is verified.
        /// </summary>
        /// <returns>
        ///     True if license is valid, false otherwise.
        /// </returns>
        public bool IsLicenseVerified()
        {
            switch (GetStatus())
            {
            case Status.OK:
#if ZIBRA_LIQUID_PRO_VERSION
            case Status.NoMaintance:
#endif
                return true;
            default:
                return false;
            }
        }

        /// <summary>
        ///     Returns human readable string explaining current error with validation.
        /// </summary>
        public string GetErrorMessage()
        {
            switch (CurrentStatus)
            {
            case Status.KeyValidationInProgress:
                return "License key validation in progress. Please wait.";
            case Status.NetworkError:
                return "Network error. Please try again later.";
            case Status.NotRegistered:
                return "License key is invalid.";
#if ZIBRA_LIQUID_PRO_VERSION
            case Status.Expired:
                return "License expired.";
#endif
            default:
                return "";
            }
        }

        /// <summary>
        ///     Returns singleton of this class.
        /// </summary>
        /// <remarks>
        ///     Creates and initializes instance if needed.
        /// </remarks>
        public static ZibraServerAuthenticationManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ZibraServerAuthenticationManager();
                Instance.Initialize();
            }

            return Instance;
        }

        /// <summary>
        ///     Sets license key and starts key validation process.
        /// </summary>
        public void RegisterKey(string key)
        {
            IsLicenseKeyValid = false;
            SendRequest(key);
        }

#endregion
#region Implementation details
        private ZibraServerAuthenticationManager()
        {
        }

        static ZibraServerAuthenticationManager()
        {
            GetInstance();
        }

        private const string BASE_URL = "https://generation.zibra.ai/";
 #if ZIBRA_LIQUID_PRO_VERSION
        private const string BASE_PRO_URL = "https://license.zibra.ai/";
        private const string VERSION_DATE = "";
 #endif
        private string UserHardwareID = "";
        private string UserID = "";
        private UnityWebRequestAsyncOperation Request;
        private bool IsLicenseKeyValid = false;

        private Status CurrentStatus = Status.NotInitialized;

        private static ZibraServerAuthenticationManager Instance = null;

        private string GetValidationURL(string key)
        {
            PluginLicenseKey = key;
#if ZIBRA_LIQUID_PRO_VERSION
            Int32 randomNumber = 0;
#if !ZIBRA_LIQUID_PRO_VERSION_NO_LICENSE_CHECK
            randomNumber = ZibraLiquidBridge.GetRandomNumber();
#endif
            return BASE_PRO_URL + "api/licenseExpiration?license_key=" + key + "&random_number=" + randomNumber;
#else
            return BASE_URL + "api/apiKey?api_key=" + key + "&type=validation";
#endif
        }

        private string GetRenewalKeyURL()
        {
            return BASE_URL + "api/apiKey?user_id=" + UserID + "&type=renew";
        }

        private void SendRequest(string key)
        {
            string requestURL;
            if (key != "")
            {
                // check if key is valid
                requestURL = GetValidationURL(key);
            }
            else if (UserID != "")
            {
                // request new key based on User and Hardware ID
                requestURL = GetRenewalKeyURL();
            }
            else
            {
                CurrentStatus = Status.NotRegistered;
                return;
            }

            Request = UnityWebRequest.Get(requestURL).SendWebRequest();
            Request.completed += UpdateKeyRequest;
            CurrentStatus = Status.KeyValidationInProgress;
        }

        private string GetEditorPrefsLicenceKey()
        {
            if (EditorPrefs.HasKey("ZibraLiquidLicenceKey"))
            {
                return EditorPrefs.GetString("ZibraLiquidLicenceKey");
            }

            return "";
        }

        // Pass true when Initialize is called as a result of user interaction
        private void Initialize()
        {
#if ZIBRA_LIQUID_PRO_VERSION && !ZIBRA_LIQUID_PRO_VERSION_NO_LICENSE_CHECK
            if (ZibraLiquidBridge.IsLicenseValidated() != 0)
            {
                CurrentStatus = Status.OK;
                GenerationURL = CreateGenerationRequestURL("compute");
                return;
            }
#endif

            if (CurrentStatus != Status.OK && CurrentStatus != Status.KeyValidationInProgress)
            {
                // Get user ID
                CollectUserInfo();

                PluginLicenseKey = GetEditorPrefsLicenceKey();
                SendRequest(PluginLicenseKey);
            }
        }

        // C# doesn't know we use it with JSON deserialization
#pragma warning disable 0649
#if ZIBRA_LIQUID_PRO_VERSION
        private class LicenseKeyResponse
        {
            public string license_info;
            public string signature;
        }

        private class LincenseInfo
        {
            public string license;
            public string latest_version;
            public string random_number;
            public string message;
            public string warning;
            public string URL;
        }
#else
        private class LicenseKeyResponse
        {
            public string api_key;
        }
#endif
#pragma warning restore 0649

        private void ProcessServerResponse(string response)
        {
#if ZIBRA_LIQUID_PRO_VERSION
            LicenseKeyResponse parsedResponse = JsonUtility.FromJson<LicenseKeyResponse>(response);
            if (parsedResponse.signature == null || parsedResponse.license_info == null)
            {
                CurrentStatus = Status.NotRegistered;
                return;
            }

            LincenseInfo licenseInfo = JsonUtility.FromJson<LincenseInfo>(parsedResponse.license_info);

            Debug.Log("Zibra Liquids Pro license info:" + licenseInfo.message);
            if (licenseInfo.warning != "")
            {
                Debug.LogWarning(licenseInfo.warning);
                if (licenseInfo.URL != "")
                {
                    bool open = EditorUtility.DisplayDialog("Zibra Liquids license warning", licenseInfo.warning,
                                                            "Open in browser", "Ignore");
                    if (open)
                    {
                        Application.OpenURL(licenseInfo.URL);
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Zibra Liquids license warning", licenseInfo.warning, "OK");
                }
            }

            switch (licenseInfo.license)
            {
            case "ok":
                CurrentStatus = Status.OK;
                break;
            case "old_version_only":
                int comparison = String.Compare(licenseInfo.latest_version, VERSION_DATE, StringComparison.Ordinal);
                if (comparison < 0)
                {
                    CurrentStatus = Status.Expired;
                    return;
                }
                CurrentStatus = Status.NoMaintance;
                break;
            case "expired":
                CurrentStatus = Status.Expired;
                return;
            default:
                CurrentStatus = Status.NotRegistered;
                return;
            }

            UnityEditor.VSAttribution.ZibraAI.VSAttribution.SendAttributionEvent("ZibraLiquids_Login", "ZibraAI",
                                                                                 PluginLicenseKey);
#if !ZIBRA_LIQUID_PRO_VERSION_NO_LICENSE_CHECK
            ZibraLiquidBridge.ValidateLicense(response, response.Length);
#endif
#else
            LicenseKeyResponse licenseKeyResponse = JsonUtility.FromJson<LicenseKeyResponse>(response);
            string apiKey = licenseKeyResponse.api_key;

            if (apiKey == "")
            {
                CurrentStatus = Status.NotRegistered;
                return;
            }

            PluginLicenseKey = apiKey;
            SetLicenceKey(apiKey);

            CurrentStatus = Status.OK;
#endif
            IsLicenseKeyValid = true;
            SetLicenceKey(PluginLicenseKey);
            // populate server request URL if everything is fine
            GenerationURL = CreateGenerationRequestURL("compute");
        }

        private void UpdateKeyRequest(AsyncOperation obj)
        {
            if (IsLicenseKeyValid)
                return;

            if (Request.isDone)
            {
                var result = Request.webRequest.downloadHandler.text;
#if UNITY_2020_2_OR_NEWER
                if (result != null && Request.webRequest.result == UnityWebRequest.Result.Success)
#else
                if (result != null && !Request.webRequest.isHttpError && !Request.webRequest.isNetworkError)
#endif
                {
                    ProcessServerResponse(result);
                }
#if UNITY_2020_2_OR_NEWER
                else if (Request.webRequest.result != UnityWebRequest.Result.Success)
#else
                else if (Request.webRequest.isHttpError || Request.webRequest.isNetworkError)
#endif
                {
                    CurrentStatus = Status.NetworkError;
                    Debug.Log(Request.webRequest.error);
                }
            }
            return;
        }

        private void SetLicenceKey(string key)
        {
            EditorPrefs.SetString("ZibraLiquidLicenceKey", key);
        }

        private void CollectUserInfo()
        {
            UserHardwareID = SystemInfo.deviceUniqueIdentifier;

            var assembly = Assembly.GetAssembly(typeof(UnityEditor.EditorWindow));
            var uc = assembly.CreateInstance("UnityEditor.Connect.UnityConnect", false,
                                             BindingFlags.NonPublic | BindingFlags.Instance, null, null, null, null);
            // Cache type of UnityConnect.
            if (uc == null)
            {
                return;
            }

            var t = uc.GetType();
            // Get user info object from UnityConnect.
            var userInfo = t.GetProperty("userInfo")?.GetValue(uc, null);
            // Retrieve user id from user info.
            if (userInfo == null)
            {
                return;
            }

            var userInfoType = userInfo.GetType();
            var isValid = userInfoType.GetProperty("valid");
            if (isValid == null || isValid.GetValue(userInfo, null).Equals(false))
            {
                return;
            }

            UserID = userInfoType.GetProperty("userId")?.GetValue(userInfo, null) as string;
            if (UserID == "")
            {
                return;
            }
        }

        private string CreateGenerationRequestURL(string type)
        {
            string generationURL;

            generationURL = BASE_URL + "api/unity/" + type + "?";

            if (UserID != "")
            {
                generationURL += "&user_id=" + UserID;
            }

            if (UserHardwareID != "")
            {
                generationURL += "&hardware_id=" + UserHardwareID;
            }

            if (PluginLicenseKey != "")
            {
                generationURL += "&api_key=" + PluginLicenseKey;
            }

            return generationURL;
        }
#endregion
    }
}

#endif
