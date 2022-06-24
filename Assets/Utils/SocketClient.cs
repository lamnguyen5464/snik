using UnityEngine;
using WebSocketSharp;
public class Test
{
    public string data;

    public string type;

    public Test()
    {
        this.data = "Dat";
        this.type = "CREATE_ROOM";
    }
    public Test(string data, string type)
    {
        this.data = data;
        this.type = type;
    }
}

public class SocketClient
{
    static WebSocket ws;

    public static void connect()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) =>
        {
            Test a = JsonUtility.FromJson<Test>(e.Data);
            Debug.Log(a.data);
            Debug.Log(a.type);

        };
        ws.Connect();
    }

    public static void send()
    {
        if (ws == null) return;
        Test a = new Test();
        var json = JsonUtility.ToJson(a, true);
        ws.Send (json);
    }

    public static void stop()
    {
    }
}
