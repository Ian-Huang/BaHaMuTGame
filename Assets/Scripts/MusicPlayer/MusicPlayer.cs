using UnityEngine;
using System.Collections;

/// <summary>
/// 淡入淡出背景音樂
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    public float FadeTime;

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
        if (!this.GetComponent<AudioSource>()) this.gameObject.AddComponent<AudioSource>();
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
    void BGM_FadeOUT()
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



}
