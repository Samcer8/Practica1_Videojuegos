using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour, ITriggerTarget
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private bool expiresInstantly;

    [SerializeField]
    protected float timeToExpire;

    protected AudioSource m_audioSource;
    protected FPSPlayer m_player;

    private float m_expireTimer;
    private bool m_isActionDone;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.RotateAroundLocal(Vector3.up, 0.06f);
    }

    public void HitByPlayer(FPSPlayer player)
    {
        m_player = player;
        m_audioSource.Play();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        m_expireTimer = 0;
        ExecuteAction();
        m_isActionDone = false;
    }

    //virtual es para poder sobreescribir luego en los hijos/los que hereden de esta clase
    protected virtual void ExecuteAction()
    {
        //Ejecutar accion en override
        if(expiresInstantly)
        {
            ExitAction();
        }
    }

    private void ExitAction()
    {
        m_isActionDone = true;
    }
}
