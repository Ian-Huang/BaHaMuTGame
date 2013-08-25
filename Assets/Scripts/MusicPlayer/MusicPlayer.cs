using UnityEngine;
using System.Collections;

/// <summary>
/// 淡入淡出背景音樂
/// </summary>
/// 使用方法
/// MusicPlayer.script.ChangeBackgroungMusic(號碼);
/// 
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer script;
    //淡入淡出時間
    public float FadeTime = 2;
    //指定歌曲號碼
    public int number;

    public bool test;

    [System.Serializable]
    public class BGM_Class
    {
        public string BGM_Name;
        public AudioClip audioClip;
    }
    public BGM_Class[] BGM;

    // Use this for initialization
    void Start()
    {
        script = this;
        DontDestroyOnLoad(this.gameObject);

        if (!this.GetComponent<AudioSource>()) this.gameObject.AddComponent<AudioSource>();
        this.audio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            test = false;
            StartCoroutine("ChangeBackgroundMusic");
        }
    }

    void OnEnable()
    {

    }

    void BGM_FadeIn()
    {
        audio.Play();
        this.audio.volume = 0;
        iTween.AudioTo(this.gameObject, 1, 1, FadeTime);

    }
    public void BGM_FadeOUT()
    {
        iTween.AudioTo(this.gameObject, 0, 1, FadeTime);
    }



    IEnumerator ChangeBackgroundMusic()
    {
        if (audio.isPlaying)
        {
            //先將原曲FadeOUT
            BGM_FadeOUT();
            //等待FadeOUT結束，將新曲FadeIN
            yield return new WaitForSeconds(FadeTime);
            this.audio.clip = BGM[number].audioClip;
            BGM_FadeIn();
        }
        else
        {
            this.audio.clip = BGM[number].audioClip;
            BGM_FadeIn();
        }
    }


    /// <summary>
    /// 改變音樂音效
    /// </summary>
    /// <param name="number">背景音樂編號</param>
    public void ChangeBackgroungMusic(int number)
    {
        this.number = number;
        StartCoroutine("ChangeBackgroundMusic");
    }


}
