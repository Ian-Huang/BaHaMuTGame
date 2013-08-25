using UnityEngine;
using System.Collections;

public class MUI_LoadSceneTransitionsEffect : MonoBehaviour
{
    public static MUI_LoadSceneTransitionsEffect script;

    //[OPTIONAL]目前驅動2D顯示的介面物件
    public GameObject currentUserInterface;
    //[OPTIONAL]目前驅動3D顯示的物件(相機)
    public Camera currentCamera;
    //過場特效
    public GameObject TransitionsEffect;

    public float DefaultDelayTime;
    public static float DelayTime;

    /// <summary>
    /// 不同步方式讀新場景，有過場特效
    /// </summary>
    /// <param name="sceneName">場景名</param>
    /// <param name="delayTime">給"0"會以預設秒數讀取，非0為自定義秒數</param>
    public void LoadScene(string sceneName, float delayTime)
    {
        DelayTime = delayTime;
        if (DelayTime == 0) DelayTime = DefaultDelayTime;
        StartCoroutine("LoadSceneAsync", sceneName);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (!Application.isLoadingLevel)
        {
            if (TransitionsEffect) TransitionsEffect.SetActive(true);
            if (currentCamera) currentCamera.enabled = false;
            if (currentUserInterface) currentUserInterface.SetActive(false);

            MusicPlayer.script.BGM_FadeOUT();
            yield return new WaitForSeconds(DelayTime);       
            Application.LoadLevelAsync(sceneName);
        }
    }

    // Use this for initialization
    void Start()
    {
        script = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        TransitionsEffect.SetActive(false);
    }

}