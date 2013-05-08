using UnityEngine;
using System.Collections;

public class AudioPlayOneShot : MonoBehaviour {

    public AudioPlayOneShot ST;
    public AudioClip[] Audio;
    public int audioCount;
    public bool playOneShot;
    
	// Use this for initialization
	void Start () {
        if (!this.GetComponent<AudioSource>())
            this.gameObject.AddComponent<AudioSource>();

        ST = this;
	}
	
	// Update is called once per frame
	void Update () {
      
        if (playOneShot)
        {
            playOneShot = false;
            if(Audio[audioCount] && !this.audio.isPlaying)
            {
                this.audio.Stop();
                PlayAudio(audioCount);
            }
        }
	}



    public void PlayAudio(int _audioCount)
    {
        this.audio.PlayOneShot(Audio[_audioCount]);
    }
}
