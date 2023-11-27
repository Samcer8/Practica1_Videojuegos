using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeLauncher : Weapon
{
    [SerializeField]
    private GameObject grenade;
    protected override void Shot()
    {
        //Diria que esto va a ser más o menos lo mismo pero con más delay y más fuerza

        base.Shot();

        if (m_currentAmmo <= 0)
        {
            return;
        }

        GameManager.Instance.currentAmmo--;
        GameManager.Instance.SetAmmo();

        m_canShot = false;

        GameObject grenadeInstance = Instantiate(grenade, launchPoint);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(launchPoint.forward * m_forceToApply, ForceMode.Impulse);
        grenadeInstance.transform.SetParent(null, true);

        OldSoundManager.Instance.PlayFx(AudiosFX.pistol_shot, m_audioSource);
    }
}
