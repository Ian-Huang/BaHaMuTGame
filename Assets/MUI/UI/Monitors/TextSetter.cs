using UnityEngine;
using System.Collections;

/// <summary>
/// 主要目的：根據語系程式檔更改文字
/// 語系文字改變
///  * 以程式的方式填入文字訊息，修改MUI的Label的文字與GUISkin
/// </summary>
public class LanguageLable : MonoBehaviour {

    private Label label;
    public string MappingText;
    private string getText;

    
	// Use this for initialization
    void Start()
    {
        if (this.gameObject.GetComponent<Label>())
            label = this.gameObject.GetComponent<Label>();

        //警告通知
        if (!label) Debug.LogWarning(this.name + "-label" + "-Unset");

        getText = (string)(Languages_Controller.script.languageFile.GetType().GetField(MappingText).GetValue(Languages_Controller.script.languageFile));
        label.Text = getText;
        label.guiSkin = Languages_Controller.script.languageGUISkin;
    }
	
	// Update is called once per frame
	void Update () {

        
	}
}
