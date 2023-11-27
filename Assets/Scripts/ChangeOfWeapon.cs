using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOfWeapon : MonoBehaviour
{
    [Header("Teclas para armas")]
    [SerializeField]
    private KeyCode key1, key2, key3, key4, key5;
    [SerializeField]
    private List<GameObject> weapons;
    private int currentWeaponNumber;

    private void Start()
    {
        currentWeaponNumber = 0;
        DisableWeapons();
        weapons[currentWeaponNumber].SetActive(true);
    }
    private void Update()
    {
        //Estoy seguro de que es la forma menos eficiente de hacer esto pero meh
        if(Input.GetKey(key1))
        {
            DisableWeapons();
            currentWeaponNumber = 0;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        else if(Input.GetKey(key2))
        {
            DisableWeapons();
            currentWeaponNumber = 1;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        else if (Input.GetKey(key3))
        {
            DisableWeapons();
            currentWeaponNumber = 2;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        else if (Input.GetKey(key4))
        {
            DisableWeapons();
            currentWeaponNumber = 3;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        else if (Input.GetKey(key5))
        {
            DisableWeapons();
            currentWeaponNumber = 4;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        //Si la rueda del raton va para arriba
        else if(Input.mouseScrollDelta.y > 0)
        {
            DisableWeapons();
            currentWeaponNumber = (currentWeaponNumber + 1) % weapons.Count;
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
        //Si va para abajo
        else if (Input.mouseScrollDelta.y < 0)
        {
            DisableWeapons();
            currentWeaponNumber = Mathf.Abs((currentWeaponNumber - 1) % weapons.Count);
            weapons[currentWeaponNumber].SetActive(true);
            GameManager.Instance.ChangeWeapon();
        }
    }

    private void DisableWeapons()
    {
        foreach(GameObject gameObject in weapons)
        {
            gameObject.SetActive(false);
        }
    }
}
