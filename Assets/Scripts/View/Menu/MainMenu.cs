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

    public void PlayMultiModeOffline()
    {
        //AudioManager.instance.Play("ButtonClick");
        Profile.getInstance().currentGameMode = 3;
        Debug.Log(Profile.getInstance().currentGameMode);  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    public void NavigateToMultiModeMenu()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
