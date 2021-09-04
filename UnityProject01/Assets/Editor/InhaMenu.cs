using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class InhaMenu : MonoBehaviour
{
    [MenuItem("InhaMenu/Clear PlayerPrefs")]
    private static void Clear_PlayerPrefsAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear_PlayerPrefsAll");
    }

    [MenuItem("InhaMenu/SubMenu/Select")]
    private static void subMenu_Selected()
    {
        Debug.Log("SubMenu - subMenu_Selected");
    }

    [MenuItem("InhaMenu/SubMenu/HotKey Test 1 %#[")] // % = ctrl, # = shift
    private static void SubMenu_Hotkey_1()
    {
        Debug.Log("HotKey Test : Ctrl + Shift + [");
    }

    [MenuItem("Assets/Load Select Scene")]
    private static void LoadSelectedScene()
    {
        var selected = Selection.activeObject;
        if (EditorApplication.isPlaying)
            EditorSceneManager.LoadScene(AssetDatabase.GetAssetPath(selected));
        else
            EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(selected));
    }
}
