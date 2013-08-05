using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
    public string audioName;
    public bool isLoop;
    public float time;
    private float currentAddTime;
    // Use this for initialization
    void OnEnable()
    {
        if (isLoop)
            InvokeRepeating("PlaySoundLoop", 0.01F, AudioSoundPlayer.script.GetAudioLength(audioName));
        else
            AudioSoundPlayer.script.PlayAudio(audioName);
    }

    void OnDisable()
    {
        CancelInvoke("PlaySoundLoop");
        currentAddTime = 0;
    }

    void Update()
    {
        if (time > 0)
        {
            currentAddTime += Time.deltaTime;
            if (currentAddTime >= time)
                OnDisable();
        }
    }
    void PlaySoundLoop()
    {
        AudioSoundPlayer.script.PlayAudio(audioName);
    }
}
