using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LoadSceneHotKeys
{
    static string F1_Scene;
    static string F2_Scene;
    static string F3_Scene;
    static string F4_Scene;
    static string F5_Scene;

    [MenuItem("Scene/LoadF1Scene &1")]
    private static void LoadF1Scene()
    {
        Debug.Log("Loading Scene: " + F1_Scene);
        EditorSceneManager.OpenScene(F1_Scene);
    }
    [MenuItem("Scene/LoadF2Scene &2")]
    private static void LoadF2Scene()
    {
        Debug.Log("Loading Scene: " + F2_Scene);
        EditorSceneManager.OpenScene(F2_Scene);
    }
    [MenuItem("Scene/LoadF3Scene &3")]
    private static void LoadF3Scene()
    {
        Debug.Log("Loading Scene: " + F3_Scene);
        EditorSceneManager.OpenScene(F3_Scene);
    }
    [MenuItem("Scene/LoadF4Scene &4")]
    private static void LoadF4Scene()
    {
        Debug.Log("Loading Scene: " + F4_Scene);
        EditorSceneManager.OpenScene(F4_Scene);
    }
    [MenuItem("Scene/LoadF5Scene &5")]
    private static void LoadF5Scene()
    {
        Debug.Log("Loading Scene: " + F5_Scene);
        EditorSceneManager.OpenScene(F5_Scene);
    }


    [MenuItem("Scene/HotkeySceneToF1 %1")]
    private static void HotkeySceneToF1()
    {
        Scene scene = EditorSceneManager.GetActiveScene();
        F1_Scene = scene.path;
        Debug.Log("Saving Scene to F1: " + F1_Scene);
    }
    [MenuItem("Scene/HotkeySceneToF2 %2")]
    private static void HotkeySceneToF2()
    {
        Scene scene = EditorSceneManager.GetActiveScene();
        F2_Scene = scene.path;
        Debug.Log("Saving Scene to F2: " + F2_Scene);
    }
    [MenuItem("Scene/HotkeySceneToF3 %3")]
    private static void HotkeySceneToF3()
    {
        Scene scene = EditorSceneManager.GetActiveScene();
        F3_Scene = scene.path;
        Debug.Log("Saving Scene to F3: " + F3_Scene);
    }
    [MenuItem("Scene/HotkeySceneToF4 %4")]
    private static void HotkeySceneToF4()
    {
        Scene scene = EditorSceneManager.GetActiveScene();
        F4_Scene = scene.path;
        Debug.Log("Saving Scene to F4: " + F4_Scene);
    }
    [MenuItem("Scene/HotkeySceneToF5 %5")]
    private static void HotkeySceneToF5()
    {
        Scene scene = EditorSceneManager.GetActiveScene();
        F5_Scene = scene.path;
        Debug.Log("Saving Scene to F5: " + F5_Scene);
    }
}
