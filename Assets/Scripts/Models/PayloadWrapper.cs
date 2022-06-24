using UnityEngine;
public class PayloadWrapper<T> where T: PayloadModel {
	public string action;
	public string payload;

	private T data;

	public static PayloadWrapper<D> FromData<D>(D data) where D: PayloadModel {
		return new PayloadWrapper<D>(
		 	data.GetAction(),
		 	data.ToJsonString(),
			data
		);
	}
	public static PayloadWrapper<D> FromString<D>(string payload) where D: PayloadModel {
		D data = JsonUtility.FromJson<D>(payload);

		return new PayloadWrapper<D>(
		 	data.GetAction(),
			payload,
			data
		);
	}

	private PayloadWrapper(string action, string payload, T data) {
		this.action = action;
		this.payload = payload;
		this.data = data;
	}

	public T GetData() { 
		return this.data;
	}

	public string GetPayload() {
		return JsonUtility.ToJson(this);
	}

}