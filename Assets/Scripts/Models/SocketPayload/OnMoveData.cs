using UnityEngine;

public class OnMoveData: PayloadData {

	public int firstX;
	public int firstY;

	public int secondX;
	public int secondY;

	public OnMoveData(
		int firstX,
		int firstY,
		int secondX,
		int secondY
	){
		this.firstX = firstX;
		this.firstY = firstY;
	}

	public  string GetAction() {
		return ActionTypes.ON_MOVE;
	} 

	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}