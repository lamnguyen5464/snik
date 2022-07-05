
using UnityEngine;
public class Profile {
	private static Profile? instance = null;
	public static Profile getInstance() {
		if (Profile.instance == null) {
			Profile.instance = new Profile();
		}
		return Profile.instance;
	}

	public string clientId;
	public string nickName = "Player 1";
	public int currentGameMode = 1;
	public string roomId = "";
}