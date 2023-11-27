using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigExplosionEffect : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 2f);
    }
}
