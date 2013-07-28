using UnityEngine;
using System.Collections;

public class MUI_AutoType_Text : MonoBehaviour {

    public string longString;

    void Awake()
    {
        this.GetComponent<MUI_AutoType>().Text = longString;
    }
    void Srart()
    {

    }

    void Update()
    {
        
    }
}
