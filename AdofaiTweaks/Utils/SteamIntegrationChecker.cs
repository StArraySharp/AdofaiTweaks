using System.Reflection;
using ADOFAI.SteamIntegration;
using HarmonyLib;

namespace AdofaiTweaks.Utils;

/// <summary>
/// Wrapper class for backwards compatibility with Steam Integration detection.
/// </summary>
public static class SteamIntegrationChecker {
    /// <summary>
    /// Returns true if Steam Integration is available.
    /// </summary>
    /// <returns><c>true</c> if Steam Integration is available.</returns>
    public static bool Check() {
        if (AdofaiTweaks.ReleaseNumber < 131) {
            return OldSteamIntegrationCheck();
        }

        return SteamController.initialized;
    }

    private static readonly FieldInfo SteamIntegrationInstanceField =
        AccessTools.Field(typeof(SteamController), "Instance");
    private static readonly FieldInfo SteamIntegrationInitializedField =
        AccessTools.Field(typeof(SteamController), "initialized");

    private static bool OldSteamIntegrationCheck() {
        var integration = (SteamController)SteamIntegrationInstanceField.GetValue(null);
        if (integration == null) {
            return false;
        }

        return (bool)SteamIntegrationInitializedField.GetValue(integration);
    }
}