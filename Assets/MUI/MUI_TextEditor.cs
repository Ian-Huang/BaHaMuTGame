using UnityEngine;
using System.Collections;

public class MUI_TextEditor : MonoBehaviour
{

    public string longString;

    void Awake()
    {
        if (this.GetComponent<MUI_AutoType>()) this.GetComponent<MUI_AutoType>().Text = longString;
        if (this.GetComponent<MUI_Label>()) this.GetComponent<MUI_Label>().Text = longString;
    }
    void Srart()
    {

    }

    void Update()
    {

    }
}
