using UnityEngine;
public class CreateRoomResponse: PayloadData {

	public string roomId;

    public CreateRoomResponse() {
    } 

	// override
	public  string GetAction() {
		return ActionTypes.CREATE_ROOM;
	} 

	// override
	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}