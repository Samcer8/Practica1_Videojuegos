using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{
    [SerializeField]
    private float radius;
    private Collider[] objects; 
    protected override void Shot()
    {
        //Diria que esto va a ser más o menos lo mismo pero con más delay y más fuerza

        base.Shot();

        if(m_currentAmmo <= 0)
        {
            return;
        }

        GameManager.Instance.currentAmmo--;
        GameManager.Instance.SetAmmo();

        //m_currentAmmo--;

        //Esto cundiría más con una clase abstracta que ya haga esto pero bueno
        //ammoText.text = m_currentAmmo.ToString();

        m_canShot = false;

        for (int i = 0; i < m_maxBulletsPerShoot; i++)
        {
            float accuracyModifier = (100 - m_currentAccuracy) / 1000;
            Vector3 directionForward = m_raycastSpot.forward;
            directionForward.x += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
            directionForward.y += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
            //directionForward.z       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
            m_currentAccuracy -= m_accuracyDropPerShot;
            m_currentAccuracy = Mathf.Clamp(m_currentAccuracy, 0, 100);

            Ray ray = new Ray(m_raycastSpot.position, directionForward);
            Debug.DrawRay(m_raycastSpot.position, directionForward, Color.green, 4);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(m_raycastSpot.position, directionForward, 100.0F);
            float maxDistance = float.MaxValue;
            int nearGObjectIndex = 0;

            for (int j = 0; j < hits.Length; j++)
            {
                RaycastHit aux = hits[j];
                float distance = Vector3.Distance(transform.position, aux.point);

                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    nearGObjectIndex = j;
                }
            }

            if (hits.Length != 0)
            {
                Instantiate(m_impactParticleSystem, hits[nearGObjectIndex].point, Quaternion.LookRotation(hits[nearGObjectIndex].normal));
                objects = Physics.OverlapSphere(hits[nearGObjectIndex].transform.position, radius);

                foreach(Collider obj in objects)
                {
                    if(obj.GetComponent<Rigidbody>() != null)
                    {
                        obj.GetComponent<Rigidbody>()?.AddExplosionForce(m_forceToApply * 2, m_raycastSpot.position, 100);
                    }

                    if (obj.transform.CompareTag("Target"))
                    {
                        GameManager.Instance.setScore(obj.transform.GetComponentInParent<PointsTarget>().targetScore);
                    }
                }

                Debug.Log("Hit " + hits[nearGObjectIndex].transform.name);
            }
        }

        OldSoundManager.Instance.PlayFx(AudiosFX.pistol_shot, m_audioSource);
    }
}
