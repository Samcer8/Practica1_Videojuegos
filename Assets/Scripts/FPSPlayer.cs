using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    private int currentHealth;
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private TMP_Text healthText;

    //Esto va en el GameManager o en el UIManager

    private void Awake()
    {
        //Confined se queda en la pantalla del juego, bueno para menus
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ITriggerTarget>()?.HitByPlayer(this);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<ITriggerExitable>().ExitedByPlayer();
    }

    public void addHealth(int health)
    {
        if(health + currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }

        healthText.text = currentHealth.ToString();
    }

    public void loseHealth(int health)
    {
        currentHealth -= health;
        healthText.text = currentHealth.ToString();

        if(currentHealth < 0)
        {
            GameManager.Instance.FinishGame();
        }
    }
}
