using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInputTextBtnClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Button btnClick;
    public TMP_InputField inputUser;
    void Start()
    {
        btnClick.onClick.AddListener(GetInputText);
    }

    // Update is called once per frame
    public void GetInputText()
    {
        Debug.Log("input" + inputUser.text);
    }
}
