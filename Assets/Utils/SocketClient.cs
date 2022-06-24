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
            string rawData = e.Data;

            PayloadWrapper<TestModel> payload = PayloadWrapper<TestModel>.FromString<TestModel>(rawData);

            Debug.Log(e.Data);
            Debug.Log(payload.GetPayload());
            Debug.Log(payload.action);
            TestModel data = payload.GetData();
            Debug.Log(data.varA);
            Debug.Log(data.varB);

        };
        ws.Connect();
    }

    public static void send()
    {
        if (ws == null) return;


        PayloadWrapper<TestModel> payload = PayloadWrapper<TestModel>.FromData<TestModel>(new TestModel("aaaa",222));
        ws.Send(payload.GetPayload());

        // PayloadWrapper<TestModel> payload2 = PayloadWrapper<TestModel>.FromString<TestModel>(payload.GetPayload());
        // Debug.Log(payload2.GetData().varA + " " + payload2.GetData().varB);


        // Test a = new Test();
        // var json = JsonUtility.ToJson(a, true);
        // ws.Send (json);
    }

    public static void stop()
    {
    }
}
