public class ActionTypes {
	public static string CREATE_ROOM = "CREATE_ROOM"; // roomOwnerName -> roomId
	public static string FIND_ROOM = "FIND_ROOM"; // roomOwnerName = "" , limit -> [roomId, userName]
	public static string JOIN_ROOM = "JOIN_ROOM"; // roomId, userName 

	public static string ON_FRIEND_JOINED = "ON_FRIEND_JOINED"; //  -> userNameA, userNameB, roomId
	public static string ON_JOINED_SUCCESSFULLY = "ON_JOINED_SUCCESSFULLY"; //  -> userNameB, userNameA, roomId

	public static string ON_MOVE = "ON_MOVE"; //  Ax, Ay, Bx, By
	public static string NEW_COORDINATE = "NEW_COORDINATE"; // userName x, y,
	public static string START_GAME = "START_GAME"; // userName x, y,
}