using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerDataRequest 
{
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
    public PlayerGender gender;
    public string scene;

    public PlayerDataRequest(GameObject obj)
    {
        Transform t = obj.transform;
        Animator anim = obj.GetComponent<Animator>();
        InputHorizontal = anim.GetFloat("InputHorizontal");
        InputVertical   = anim.GetFloat("InputVertical");
        InputMagnitude  = anim.GetFloat("InputMagnitude");
        GroundDistance  = anim.GetFloat("GroundDistance");
        IsGrounded      = anim.GetBool("IsGrounded");
        IsStrafing      = anim.GetBool("IsStrafing");
        IsSprinting     = anim.GetBool("IsSprinting");

        position = t.position;
        rotation = t.rotation;
        scale = t.localScale;

        gender = ClientPlayerHandler.localPlayerGender;
        scene = SceneManager.GetActiveScene().name;
    }
}
