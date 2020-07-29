using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataResponse
{
    public string userId;
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public float InputHorizontal;
    public float InputVertical;
    public float InputMagnitude;
    public float GroundDistance;
    public bool IsGrounded;
    public bool IsStrafing;
    public bool IsSprinting;
    public string gender;
    public string scene;
}
