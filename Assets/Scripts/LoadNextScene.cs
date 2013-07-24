using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public static LoadNextScene script;

    //目前驅動2D顯示的介面物件
    public GameObject currentUserInterface;
    //目前驅動3D顯示的物件(相機)
    public Camera currentCamera;
    //過場特效
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