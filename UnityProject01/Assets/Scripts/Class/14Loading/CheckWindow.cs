using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
public class CheatWindow : EditorWindow
{
    string[] cheatList = new string[] { "ġƮ", "��� ����", "����Ʈ ����", };

    static int selectindex = 0;
    int getInt = 0;
    string getString = "";

    [MenuItem("InhaMenu/SubMenu/ġƮ���â1", false, 0)]
    public static void OpenCheatWindows1()
    {
        CheatWindow cheatWindow = EditorWindow.GetWindow<CheatWindow>(false, "Cheat Window", true);
    }


    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Space(10.0f);

        int getIndex = EditorGUILayout.Popup(selectindex, cheatList, GUILayout.MaxWidth(200.0f));

        if (selectindex != getIndex)
        {
            selectindex = getIndex;
        }

        GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
        string cheatText = "";
        if (selectindex == 0)
        {
            GUILayout.Label("ġƮŰ �Է�", GUILayout.Width(70.0f));
            getString = EditorGUILayout.TextField(getString, GUILayout.Width(100.0f));
            cheatText = string.Format("ġƮŰ : {0}", getString);
        }
        else if (selectindex == 1)
        {
            GUILayout.Label("���", GUILayout.Width(70.0f));
            getString = EditorGUILayout.TextField(getInt.ToString(), GUILayout.Width(100.0f));
            int.TryParse(getString, out getInt);
            cheatText = string.Format("��� : {0}", getInt);
        }
        else if (selectindex == 2)
        {
            GUILayout.Label("����Ʈ", GUILayout.Width(70.0f));
            getString = EditorGUILayout.TextField(getInt.ToString(), GUILayout.Width(100.0f));
            int.TryParse(getString, out getInt);
            cheatText = string.Format("����Ʈ : {0}", getInt);
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20.0f);
        GUILayout.BeginHorizontal(GUILayout.MaxWidth(800.0f));
        {
            GUILayout.BeginVertical(GUILayout.MaxWidth(300.0f));
            {
                GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
                {
                    if (GUILayout.Button("\n����\n", GUILayout.Width(100.0f)))
                    {
                        if (EditorApplication.isPlaying &&
                            EditorSceneManager.GetActiveScene().name != "Title")
                        {
                            getInt = 0;
                            getString = "";
                            Debug.Log(cheatText);
                        }
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
                {
                    if (GUILayout.Button("\n��׶���\n����\n", GUILayout.Width(100.0f)))
                    {
                        Application.runInBackground = true;
                    }
                    if (GUILayout.Button("\n��׶���\n���� ����\n", GUILayout.Width(100.0f)))
                    {
                        Application.runInBackground = false;
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}