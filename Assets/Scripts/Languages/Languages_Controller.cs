using UnityEngine;
using System.Collections;

public class Languages_Controller : MonoBehaviour
{

    //靜態副本
    public static Languages_Controller script;
    private Languages_en language_en;
    private Languages_zhTW language_zhTW;
    private Languages_jp language_jp;

    public enum Language { English, Traditional_Chinese, Japanese };
    public Language language;

    public GUISkin GUISkin_en;
    public GUISkin GUISkin_zhTW;
    public GUISkin GUISkin_jp;

    [HideInInspector]
    public Object languageFile;
    [HideInInspector]
    public GUISkin languageGUISkin;
    void Awake()
    {
        script = this;
        DontDestroyOnLoad(this.gameObject);

        switch (language)
        {
            case Language.English:
                if (!this.GetComponent<Languages_en>())
                    language_en = this.gameObject.AddComponent<Languages_en>();
                break;
            case Language.Traditional_Chinese:
                if (!this.GetComponent<Languages_zhTW>())
                    language_zhTW = this.gameObject.AddComponent<Languages_zhTW>();
                break;
            case Language.Japanese:
                if (!this.GetComponent<Languages_jp>())
                    language_jp = this.gameObject.AddComponent<Languages_jp>();
                break;
            default:
                languageFile = language_zhTW;
                break;
        }

        ChangeLanguage(language);
    }
    // Use this for initialization
    void Start()
    {

    }

    public void ChangeLanguage(Language language)
    {
        this.language = language;
        switch (language)
        {
            case Language.English:
                languageFile = language_en;
                languageGUISkin = GUISkin_en;
                break;
            case Language.Traditional_Chinese:
                languageFile = language_zhTW;
                languageGUISkin = GUISkin_zhTW;
                break;
            case Language.Japanese:
                languageFile = language_jp;
                languageGUISkin = GUISkin_jp;
                break;
            default:
                languageFile = language_zhTW;
                languageGUISkin = GUISkin_zhTW;
                break;
        }
    }
}
