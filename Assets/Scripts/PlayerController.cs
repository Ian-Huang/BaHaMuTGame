using UnityEngine;
using System.Collections;

/// <summary>
/// 各種特殊播放管理器 製作原因是不想跟 GameManager 造成衝突
/// </summary>
public class PlayerController : MonoBehaviour
{
    //背景音樂的Index編號
    public bool PlayBackGroundMusic;
    public int BackGroundMusicIndex;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayBackGroundMusic)
        {
            PlayBackGroundMusic = false;
            if(MusicPlayer.script)
            MusicPlayer.script.ChangeBackgroungMusic(BackGroundMusicIndex);
        }

    }
}
