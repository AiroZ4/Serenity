using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backtomenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }
    public void QuitButton1()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
