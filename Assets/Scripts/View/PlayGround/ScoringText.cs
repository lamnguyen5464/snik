using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text name_1;
    public TMP_Text name_2;
    public TMP_Text score_1;
    public TMP_Text score_2;
    public static ScoringText instance;
    void Awake()
    {
        name_1.text = "User 1";
        score_1.text = "0";
        name_2.text = "User 2";
        score_2.text = "0";
        if (instance == null)
            instance = this;
    }
    public void changeSecondNickname(string _name_2) {
        name_2.text = _name_2;
    }
    public void changeNickname(string _name_1) {
        name_1.text = _name_1;
    }
    public void changeSecondScore(int _score_2) {
        score_2.text = (_score_2).ToString();
    }
    public void changeScore(int _score_1) {
        score_1.text = (_score_1).ToString();
    }
    public void applySingleMode() {
        score_2.text="";
        name_2.text="";
    }
}
