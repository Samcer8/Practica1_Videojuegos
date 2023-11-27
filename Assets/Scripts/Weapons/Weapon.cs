using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Transform m_raycastSpot;
    [SerializeField]
    protected float m_damage;
    [SerializeField]
    protected float m_forceToApply;
    [SerializeField]
    protected float m_weaponRange;
    [SerializeField]
    protected Texture2D m_crosshairTexture;
    [SerializeField]
    protected AudioClip m_fireSound;
    protected bool m_canShot = true;

    [Header("HOLA")]
    [SerializeField]
    protected int m_maxAmmo;
    protected int m_currentAmmo;

    [SerializeField]
    protected int m_maxBulletsPerShoot;

    [SerializeField]
    protected int m_bulletsPerSecond;
    protected float m_timeBetweenShots;
    protected float m_shotTimer = 0;

    [SerializeField]
    protected float m_accuracyDropPerShot;
    [SerializeField]
    protected float m_accuracyRecoveryPerSecond;
    [SerializeField]
    protected float m_accuracyMax;
    protected float m_currentAccuracy;

    protected AudioSource m_audioSource;

    [SerializeField]
    protected ParticleSystem m_impactParticleSystem, m_shootParticleSystem;

    [SerializeField]
    protected Transform launchPoint;

    /*[SerializeField]
    private TMP_Text ammoText;*/

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_currentAmmo = m_maxAmmo;
        m_shotTimer = 0;
        m_timeBetweenShots = 1f / m_bulletsPerSecond;
        GameManager.Instance.OnWeaponChange += ResetAmmo;
        GameManager.Instance.currentAmmo = m_currentAmmo;
        GameManager.Instance.SetAmmo();
        m_currentAccuracy = 0f;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnWeaponChange += ResetAmmo;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnWeaponChange -= ResetAmmo;
    }

    public void ResetAmmo()
    {
        GameManager.Instance.currentAmmo = m_currentAmmo;
        GameManager.Instance.SetAmmo();
    }

    protected void OnGUI()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Rect auxRect = new Rect(center.x - 100, center.y - 100, 100, 100);
        GUI.DrawTexture(auxRect, m_crosshairTexture, ScaleMode.StretchToFill);
    }

    protected virtual void Shot()
    {
        m_currentAmmo = GameManager.Instance.currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        m_shotTimer += Time.deltaTime;

        m_currentAccuracy += Mathf.Lerp(m_currentAccuracy, m_accuracyMax, Time.deltaTime * m_accuracyRecoveryPerSecond);

        if (Input.GetButtonDown("Fire2"))
        {
            m_currentAmmo = m_maxAmmo;
            ResetAmmo();
            OldSoundManager.Instance.PlayFx(AudiosFX.pistol_reload, m_audioSource);
        }

        //m_canShot = m_isAMachineGun || m_canShot;

        if (m_shotTimer >= m_timeBetweenShots && m_canShot)
        {
            if (Input.GetButton("Fire1"))
            {
                m_shotTimer = 0;

                Shot();

            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            m_canShot = true;
        }
    }
}
