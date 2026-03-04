#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class NGOResetPrompt
{
    [MenuItem("Tools/Netcode/Reset NetworkObject Add Prompt")]
    public static void ResetNGOWarning()
    {
        EditorPrefs.DeleteKey("NetcodeForGameObjects.AddNetworkObjectConfirmationSuppressed");
        Debug.Log("NGO auto-add NetworkObject prompt reset. You will now be asked again.");
    }
}
#endif