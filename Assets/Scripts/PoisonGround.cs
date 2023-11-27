using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGround : MonoBehaviour
{
    [SerializeField]
    private int healtLoss;
    [SerializeField]
    private float timeForHealtLoss;

    private bool isPlayerTouching;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = true;
            StartCoroutine(TimeAction(other));
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = false;
        }
    }

    private IEnumerator TimeAction(Collider player)
    {
        yield return null;

        while(isPlayerTouching)
        {
            GameManager.Instance.looseHealth(healtLoss);
            yield return new WaitForSeconds(timeForHealtLoss);
        }
    }
}
