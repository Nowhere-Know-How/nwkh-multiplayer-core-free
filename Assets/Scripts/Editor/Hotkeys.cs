#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace HappyMaki
{
    static class Hotkeys
    {
        [Shortcut("Clear Console", KeyCode.K, ShortcutModifiers.Action)]
        public static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }



    }
}
#endif