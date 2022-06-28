using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlaySingleMode()
    {
        //AudioManager.instance.Play("ButtonClick");
        Profile.getInstance().currentGameMode = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlaySingleModeOnline()
    {
        //AudioManager.instance.Play("ButtonClick");
        Profile.getInstance().currentGameMode = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    public void NavigateToMultiModeMenu()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
