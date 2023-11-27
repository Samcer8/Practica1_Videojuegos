using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 20f);
    }
}
