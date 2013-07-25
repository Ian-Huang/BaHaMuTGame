using UnityEngine;
using System.Collections;

public class MUI_LoadSceneTransitionsEffect : MonoBehaviour
{
    public static MUI_LoadSceneTransitionsEffect script;

    //[OPTIONAL]�ثe�X��2D��ܪ���������
    public GameObject currentUserInterface;
    //[OPTIONAL]�ثe�X��3D��ܪ�����(�۾�)
    public Camera currentCamera;
    //�L���S��
    public GameObject TransitionsEffect;

    public float DefaultDelayTime;
    public static float DelayTime;

    /// <summary>
    /// ���P�B�覡Ū�s�����A���L���S��
    /// </summary>
    /// <param name="sceneName">�����W</param>
    /// <param name="delayTime">��"0"�|�H�w�]���Ū���A�D0���۩w�q���</param>
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
            if (TransitionsEffect)      TransitionsEffect.SetActive(true);
            if (currentCamera)          currentCamera.enabled = false;
            if (currentUserInterface)   currentUserInterface.SetActive(false);
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