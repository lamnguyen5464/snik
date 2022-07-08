using UnityEngine;
public class TestModel: PayloadData {
	public string varA;
    public int varB;
    public TestModel(
		string varA,
		int varB
	) {
        this.varA = varA;
        this.varB = varB;
    } 

	// override
	public  string GetAction() {
		return "TestModel"; // Get from ActionType
	} 

	// override
	public string ToJsonString() {
		return JsonUtility.ToJson(this);
	}
}