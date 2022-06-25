using UnityEngine;

public class NewCoordinateData: PayloadData {

	public string userName;
	public int x;
	public int y;


	public NewCoordinateData(string userName, int x, int y) {
		this.userName = userName;
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