using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton
{
    public static GameManager Instance { get; private set; }

    public delegate void OnStart();
    public event OnStart OnWeaponChange;

    [SerializeField]
    private TMP_Text scoreText, healthText, ammoText;

    [SerializeField]
    private FPSPlayer player;

    //Creo que estoy haciendo variables de más
    public int currentScore, currentHealth, currentAmmo, currentCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentCoins = 0;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Game was exited");
            Application.Quit();
        }
    }

    public void FinishGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }

    public void setScore(int score)
    {
        scoreText.GetComponent<PlayerScore>().SetScore(score);
    }

    public void addAmmo(int ammo)
    {
        currentAmmo = int.Parse(ammoText.text);
        currentAmmo += ammo;
        SetAmmo();
    }

    public void SetAmmo()
    {
        ammoText.text = currentAmmo.ToString();
    }

    public void addHealth(int health)
    {
        player.addHealth(health);
    }

    public void looseHealth(int health)
    {
        player.loseHealth(health);
    }
    public void ChangeWeapon()
    {
        OnWeaponChange?.Invoke();
    }
}
