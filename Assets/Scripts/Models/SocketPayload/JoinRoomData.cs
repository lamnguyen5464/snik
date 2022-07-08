using UnityEngine;

public class JoinRoomData: PayloadData {

    public string roomId;
	public string nickName;



	public JoinRoomData(string roomId , string nickName) {
		this.roomId = roomId;
		this.nickName = nickName;
	}

	public  string GetAction() {
		return ActionTypes.JOIN_ROOM;
	} 

	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}