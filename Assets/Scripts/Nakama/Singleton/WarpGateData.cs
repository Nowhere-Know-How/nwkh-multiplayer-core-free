using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGateData : SingletonBehaviour<WarpGateData>
{
    public static Vector3 nextScenePosition;
    public static Quaternion nextSceneRotation;
    public static string nextSceneName;
    private WarpGateData() { }

    public static void SetTransform(Transform t)
    {
        nextScenePosition = t.position;
        nextSceneRotation = t.rotation;

    }
}
