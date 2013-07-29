using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSoundPlayer : MonoBehaviour
{
    public static AudioSoundPlayer script;

    //°Ñ¼ÆºÊ±±Dictionary
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

    public void PlayAudio(string name)
    {
        if (SoundDictionary.ContainsKey(name))
            this.audio.PlayOneShot(SoundDictionary[name]);
        else
            Debug.Log("Error Name , Dictionary not Contain the name : " + name);
    }
}
