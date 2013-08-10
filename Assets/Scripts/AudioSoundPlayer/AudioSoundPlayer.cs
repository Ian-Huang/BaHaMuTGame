using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 音效庫播放
/// </summary>
/// 使用方法
/// AudioSoundPlayer.script.PlayAudio("音樂名稱");
public class AudioSoundPlayer : MonoBehaviour
{
    public static AudioSoundPlayer script;

    //參數監控Dictionary
    public static Dictionary<string, AudioClip> SoundDictionary = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public class Sound
    {
        public string audioName;
        public AudioClip audioClip;
    }
    public Sound[] audios;

    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);

        if (!this.GetComponent<AudioSource>())
            this.gameObject.AddComponent<AudioSource>();

        script = this;

        //Dump "Sound Data" to Dictionary , so we can use "key" to get the value
        foreach (Sound sound in audios)
            SoundDictionary.Add(sound.audioName, sound.audioClip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">音效名稱</param>
    public void PlayAudio(string name)
    {
        if (SoundDictionary.ContainsKey(name))
            this.audio.PlayOneShot(SoundDictionary[name]);
        else
            Debug.Log("Error Name , Dictionary not Contain the name : " + name);
    }



    public float GetAudioLength(string name)
    {
        if (SoundDictionary.ContainsKey(name))
            return SoundDictionary[name].length;
        else
        {
            Debug.Log("Error Name , Dictionary not Contain the name : " + name);
            return 0;
        }
    }
}
