using UnityEngine;
public class ResetRoudnSignal: PayloadData {
    public ResetRoudnSignal(
	) {
    } 

	// override
	public  string GetAction() {
		return ActionTypes.RESET_GAME_ROUND; // Get from ActionType
	} 

	// override
	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}