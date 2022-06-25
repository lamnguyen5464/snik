using UnityEngine;

public class MainApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SocketClient.connect();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)) {
        //     SocketClient.send();
        // }
    }
}
