using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T>
{
    public static T instance { get; protected set; }
 
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = (T)this;
        }
    }
}
