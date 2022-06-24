public static class ActionTypes {
	public string CREATE_ROOM = "CREATE_ROOM"; // roomOwnerName -> roomId
	public string FIND_ROOM = "FIND_ROOM"; // roomOwnerName = "" , limit -> [roomId, userName]
	public string JOIN_ROOM = "JOIN_ROOM"; // roomId, userName 

	public string ON_FRIEND_JOINED = "ON_FRIEND_JOINED"; //  -> userNameA, userNameB, roomId
	public string ON_JOINED_SUCCESSFULLY = "ON_JOINED_SUCCESSFULLY"; //  -> userNameB, userNameA, roomId

	public string ON_MOVE = "ON_MOVE"; //  Ax, Ay, Bx, By
	public string NEW_COORDINATE = "NEW_COORDINATE"; // userName x, y,
}