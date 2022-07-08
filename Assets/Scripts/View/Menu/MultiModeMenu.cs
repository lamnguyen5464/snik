using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiModeMenu : MonoBehaviour
{
    public void NavigateToFindRoom()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NavigateBack()
    {
        //AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }


}
