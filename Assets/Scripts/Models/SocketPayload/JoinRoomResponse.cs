using UnityEngine;
public class JoinRoomResponse: PayloadData {

	public string msg;

    public JoinRoomResponse() {
    } 

	// override
	public string GetAction() {
		return ActionTypes.JOIN_ROOM;
	} 

	// override
	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}