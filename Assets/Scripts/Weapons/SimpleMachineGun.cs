using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMachineGun : Weapon
{
    [SerializeField]
    private GameObject holePrefab;
    protected override void Shot()
    {
        base.Shot();

        if (m_currentAmmo <= 0)
        {
            return;
        }

        GameManager.Instance.currentAmmo--;
        GameManager.Instance.SetAmmo();

        //Esto cundiría más con una clase abstracta que ya haga esto pero bueno
        //ammoText.text = m_currentAmmo.ToString();

        //m_canShot = false;

        for (int i = 0; i < m_maxBulletsPerShoot; i++)
        {
            float accuracyModifier = (100 - m_currentAccuracy) / 400;
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
                if (hits[nearGObjectIndex].rigidbody != null)
                {
                    hits[nearGObjectIndex].rigidbody.AddForce(ray.direction * m_forceToApply);
                }

                GameObject hole = Instantiate(holePrefab, hits[nearGObjectIndex].point, Quaternion.FromToRotation(Vector3.up, hits[nearGObjectIndex].normal));

                hole.transform.SetParent(hits[nearGObjectIndex].transform, true);

                Debug.Log("Hit " + hits[nearGObjectIndex].transform.name);

                /*Podemos: a) comprobar si el tag es "Target"
						   b) comprobar si tiene x componente*/

                if (hits[nearGObjectIndex].transform.CompareTag("Target"))
                {
                    GameManager.Instance.setScore(hits[nearGObjectIndex].transform.GetComponentInParent<PointsTarget>().targetScore);
                    //scoreText.GetComponent<PlayerScore>().setScore(hits[nearGObjectIndex].transform.GetComponentInParent<PointsTarget>().targetScore);
                }
            }
        }

        OldSoundManager.Instance.PlayFx(AudiosFX.pistol_shot, m_audioSource);
        //m_audioSource.PlayOneShot(m_fireSound);
        //StartCoroutine(TimePerBullet());
    }
}
