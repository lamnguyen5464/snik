using WebSocketSharp;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ws = new WebSocket("ws://localhost:8080");
        // ws.OnMessage += (sender, e) => {
        //     Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data: " + e.Data);

        // };
        // ws.Connect();
        SocketClient.connect();
    }

    // Update is called once per frame
    void Update()
    {
        // if(ws == null) {
        //     return;
        // }
        if(Input.GetKeyDown(KeyCode.Space)) {
            SocketClient.send();
        }
    }
}
