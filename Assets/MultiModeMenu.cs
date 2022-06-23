using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiModeMenu : MonoBehaviour
{
    public void FindRoom()
    {

    }

    public void NavigateBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }


}
