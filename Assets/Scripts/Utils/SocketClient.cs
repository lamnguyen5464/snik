using UnityEngine;
using WebSocketSharp;
using System;

public class SocketClient
{
    private static WebSocket ws;

    public static void connect()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();

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
    }

    public static void addHandler(Action<object, WebSocketSharp.MessageEventArgs> handler) {
        if (ws == null) {
            SocketClient.connect();
        }

        ws.OnMessage += (sender, eventData) => {
            handler(sender, eventData);
        };
    }

    public static void send()
    {
        if (ws == null) {
            SocketClient.connect();
        }
        if (ws == null) return;

        var model = new TestModel("aaaa",222);

        PayloadWrapper<TestModel> payload = PayloadWrapper<TestModel>.FromData<TestModel>(model);
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
