using WebSocketSharp;
using UnityEngine;
using System.Text.Json;

public class Test {
    public int year;
    public int month;
    public Test() {
        this.year = 1;
        this.month = 123;
    }
}
public class SocketClient {
    
    static WebSocket ws;
    public static void connect() {
        

        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) => {
        };
        ws.Connect();
        

    }
    public static void send() {
        Test a = new Test();
        var json = JsonUtility.ToJson(a);
       ws.Send(json);
    }
    public static void stop() {
        
    }
}