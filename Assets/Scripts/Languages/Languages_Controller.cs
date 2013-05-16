using UnityEngine;
using System.Collections;

public class Languages_Controller : MonoBehaviour {


    public static Languages_Controller ST;
    private Languages_en language_en;
    private Languages_zhTW language_zhTW;

    public enum Language { English, Traditional_Chinese };
    public Language language;

    public Object languageFile;

	// Use this for initialization
	void Start () {
        ST = this;
        language_en = this.GetComponent<Languages_en>();
        language_zhTW = this.GetComponent<Languages_zhTW>();
        ChangeLanguage(language);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void ChangeLanguage(Language language)
    {
        this.language = language;
        switch (language)
        {
            case Language.English:
                languageFile = language_en;
                break;
            case Language.Traditional_Chinese:
                languageFile = language_zhTW;
                break;
            default:
                languageFile = language_zhTW;
                break;
        }
    }
}
