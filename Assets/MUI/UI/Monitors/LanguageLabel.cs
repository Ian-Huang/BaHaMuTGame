using UnityEngine;
using System.Collections;

/// <summary>
/// �D�n�ت��G�ھڻy�t�{���ɧ���r
/// �y�t��r����
///  * �H�{�����覡��J��r�T���A�ק�MUI��Label����r�PGUISkin
/// </summary>
public class LanguageLabel : MonoBehaviour {

    private MUI_Label label;
    public string MappingText;
    private string getText;

    
	// Use this for initialization
    void Start()
    {
        if (this.gameObject.GetComponent<MUI_Label>())
            label = this.gameObject.GetComponent<MUI_Label>();

        //ĵ�i�q��
        if (!label) Debug.LogWarning(this.name + "-label" + "-Unset");

        getText = (string)(Languages_Controller.script.languageFile.GetType().GetField(MappingText).GetValue(Languages_Controller.script.languageFile));
        label.Text = getText;
        label.guiSkin = Languages_Controller.script.languageGUISkin;
    }
	
	// Update is called once per frame
	void Update () {

        
	}
}
