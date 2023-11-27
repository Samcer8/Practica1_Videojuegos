using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCanvas;

    public void OnExitClicked()
    {
        Debug.Log("Game was exited");
        Application.Quit();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(0);
    }
}
