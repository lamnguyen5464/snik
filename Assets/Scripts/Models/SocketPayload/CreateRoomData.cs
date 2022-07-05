using UnityEngine;

public class CreateRoomData: PayloadData {

	public string nickName;



	public CreateRoomData(string nickName) {
		this.nickName = nickName;
	}

	public  string GetAction() {
		return ActionTypes.CREATE_ROOM;
	} 

	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}