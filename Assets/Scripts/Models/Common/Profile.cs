

public class Profile {
	private static Profile? instance = null;
	public static Profile getInstance() {
		if (Profile.instance == null) {
			Profile.instance = new Profile();
		}
		return Profile.instance;
	}

	public string clientId;
	public string nickName;
}