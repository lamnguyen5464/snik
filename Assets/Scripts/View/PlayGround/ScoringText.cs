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

    void Start()
    {
        name_1.text = "Dat";
        score_1.text = (123).ToString();
        name_2.text = "Lam";
        score_2.text = (99).ToString();
    }
}
