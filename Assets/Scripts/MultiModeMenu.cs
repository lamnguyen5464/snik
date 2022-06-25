using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiModeMenu : MonoBehaviour
{
    string a;
    public void Start()
    {
        SocketClient.connect();
    }

    public void FindRoom()
    {
        
    }

    public void NavigateBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void CreateRoom()
    {
        Debug.Log(UserName.getName());
        CreateRoomRequest req = new CreateRoomRequest(new CreateRoomData("Qhuy"), "CREATE_ROOM");
        SocketClient.send(req);
    }


}
