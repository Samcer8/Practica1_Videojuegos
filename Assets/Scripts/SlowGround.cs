using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGround : MonoBehaviour
{
    [SerializeField]
    private float takenSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeSpeed(takenSpeed);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().RecoverSpeed();
        }
    }
}
