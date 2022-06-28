using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Start() {
        AudioManager.instance.Play("background");
    }

    public void PlaySingleMode()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NavigateToMultiModeMenu()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
