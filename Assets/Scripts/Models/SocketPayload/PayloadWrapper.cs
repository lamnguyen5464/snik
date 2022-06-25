using UnityEngine;
public class PayloadWrapper<T> where T: PayloadData {
	public string action;
	public string payload;

	private T data;

	public static PayloadWrapper<D> FromData<D>(D data) where D: PayloadData {
		return new PayloadWrapper<D>(
		 	data.GetAction(),
		 	data.ToJsonString(),
			data
		);
	}
	public static PayloadWrapper<D> FromString<D>(string payload) where D: PayloadData {
		PayloadWrapper<D> wrapper = JsonUtility.FromJson<PayloadWrapper<D>>(payload);
		D data = JsonUtility.FromJson<D>(wrapper.payload);

		return new PayloadWrapper<D>(
		 	wrapper.action,
			wrapper.payload,
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

	public bool isValid() {
		return this.action == this.data.GetAction();
	}

	public string GetPayload() {
		return JsonUtility.ToJson(this);
	}

}