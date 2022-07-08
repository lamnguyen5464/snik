using UnityEngine;
public class StartSignalData: PayloadData {

	public string roomId;
	public string nickName;
	public string clientId;
	public string id;

	public string[] clientIds;

    public StartSignalData() {
    } 

	// override
	public  string GetAction() {
		return ActionTypes.START_GAME;
	} 

	// override
	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}