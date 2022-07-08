using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class GetInputTextBtnClick : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputUser;
    public int signal = 0;
    void Start()
    {
        SocketClient.connect();
        Action<object, WebSocketSharp.MessageEventArgs> handler = (sender, eventData) => {
			//Debug.Log("From server: "  +eventData.Data);
            PayloadWrapper<CreateRoomResponse> createRoomResponse
                = PayloadWrapper<CreateRoomResponse>.FromString<CreateRoomResponse>(eventData.Data);
            if(createRoomResponse.isValid()){
                // CreateRoomResponse data = createRoomResponse.GetData();
			    Debug.Log("Data: "  + createRoomResponse.GetData().roomId);
                Profile.getInstance().roomId = createRoomResponse.GetData().roomId;
                this.signal = 1;
            }

            PayloadWrapper<JoinRoomResponse> joinRoomResponse
                = PayloadWrapper<JoinRoomResponse>.FromString<JoinRoomResponse>(eventData.Data);
            if(joinRoomResponse.isValid()){
                // JoinRoomResponse data = JoinRoomResponse.GetData();
			    //Debug.Log("Data: "  + joinRoomResponse.GetData().msg);
            }
        };
        
        SocketClient.addHandler(handler);

    }

    public void HandleCreateRoomClick()
    {
        //Debug.Log("input " + inputUser.text);
        Profile.getInstance().nickName = inputUser.text;
        var model = new CreateRoomData(inputUser.text);
        PayloadWrapper<CreateRoomData> payload = PayloadWrapper<CreateRoomData>.FromData<CreateRoomData>(model);
        SocketClient.send(payload.GetPayload());
        Profile.getInstance().currentGameMode = 2;
    }

    public void HandleNavigateJoinRoomClick()
    {
        Profile.getInstance().nickName = inputUser.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleJoinRoomClick()
    {
        //Debug.Log("input join" + inputUser.text);
        var model = new JoinRoomData (inputUser.text, Profile.getInstance().nickName);
        PayloadWrapper<JoinRoomData> payload = PayloadWrapper<JoinRoomData>.FromData<JoinRoomData>(model);
        SocketClient.send(payload.GetPayload());
        Profile.getInstance().currentGameMode = 2;
        Profile.getInstance().roomId = inputUser.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Update()
    {
        if(signal == 1){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
