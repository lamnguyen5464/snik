using UnityEngine;
public class TestModel: PayloadModel {
	public string varA;
    public int varB;
    public TestModel() {
        this.varA = "AAAA";
        this.varB = 111;
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