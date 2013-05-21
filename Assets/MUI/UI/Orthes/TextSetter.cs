using UnityEngine;
using System.Collections;

public class TextSetter : MonoBehaviour {

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
    }
	
	// Update is called once per frame
	void Update () {
        getText = (string)(Languages_Controller.ST.languageFile.GetType().GetField(MappingText).GetValue(Languages_Controller.ST.languageFile));
        label.Text = getText;
	}
}
