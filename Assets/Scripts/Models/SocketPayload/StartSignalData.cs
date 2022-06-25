using UnityEngine;
public class StartSignalData: PayloadData {
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