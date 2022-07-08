using UnityEngine;

public class NewCoordinateData: PayloadData {

	public string nickName;
	public int x;
	public int y;


	public NewCoordinateData(string nickName, int x, int y) {
		this.nickName = nickName;
		this.x = x;
		this.y = y;
	}

	public  string GetAction() {
		return ActionTypes.NEW_COORDINATE;
	} 

	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}