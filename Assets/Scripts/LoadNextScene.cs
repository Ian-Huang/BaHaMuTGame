using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public static LoadNextScene script;

    //�ثe�X��2D��ܪ���������
    public GameObject currentUserInterface;
    //�ثe�X��3D��ܪ�����(�۾�)
    public Camera currentCamera;
    //�L���S��
    public GameObject TransitionsEffect;

    public static float DelayTime;

    public void LoadScene(string sceneName, float delayTime)
    { 
        DelayTime = delayTime;
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

    void Update()
    {

    }

    IEnumerator OnLevelWasLoaded(int level)
    {
        yield return new WaitForSeconds(0.0F);
        TransitionsEffect.SetActive(false);
        Destroy(this.gameObject);
    }

}