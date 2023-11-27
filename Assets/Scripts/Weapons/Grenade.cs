using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float radius, m_forceToApply, timeForExplosion;
    private Collider[] objects;

    [SerializeField]
    private ParticleSystem m_impactParticleSystem;

    private void Awake()
    {
        StartCoroutine(TimeToExplode());
    }

    private void Explode()
    {
        ParticleSystem currentImpact = Instantiate(m_impactParticleSystem, gameObject.transform.position, Quaternion.LookRotation(Vector3.zero, Vector3.zero));
        currentImpact.transform.SetParent(null, false);
        objects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider obj in objects)
        {
            if (obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>()?.AddExplosionForce(m_forceToApply * 2, transform.position, 100);
            }

            if (obj.transform.CompareTag("Target"))
            {
                GameManager.Instance.setScore(obj.transform.GetComponentInParent<PointsTarget>().targetScore);
                //scoreText.GetComponent<PlayerScore>().setScore(hits[nearGObjectIndex].transform.GetComponentInParent<PointsTarget>().targetScore);
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator TimeToExplode()
    {
        yield return null;
        yield return new WaitForSeconds(timeForExplosion);
        Explode();
    }
}
