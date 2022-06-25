using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInputTextBtnClick : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputUser;
    void Start()
    {

    }

    public void GetInputText()
    {
        Debug.Log("input " + inputUser.text);
        Profile.getInstance().nickName = inputUser.text;
    }
}
