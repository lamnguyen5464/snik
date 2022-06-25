using UnityEngine;
using System.Collections.Generic;
using System;

public class OnMoveData: PayloadData {

	[System.Serializable]	
	public class UserItem {
		public int id;
		public string clientId;
		public string nickName;
		public Coordinate2D position;
	}

	public UserItem[] items;
	public string clientId;

	public OnMoveData(UserItem[] items){
		this.items = items;
	}

	public  string GetAction() {
		return ActionTypes.ON_MOVE;
	} 

	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}