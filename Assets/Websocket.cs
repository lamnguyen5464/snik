using WebSocketSharp;
using UnityEngine;

public class Websocket : MonoBehaviour
{
    Websocket = ws;
    // Start is called before the first frame update
    void Start()
    {
        ws = new Websocket("");
        ws.OnMessage += (sender, e) => 
        {
            Debug.Log("Message received from " + ((Websocket)sender).Url + ", Data : " + e.Data);
        }
        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}
