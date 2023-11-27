using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
