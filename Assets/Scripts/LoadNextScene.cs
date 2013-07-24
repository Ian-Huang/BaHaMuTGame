using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public bool showGUI;
    public int GUIdepth;                            //貼圖深度(值越小越後畫)
    public Texture TextureResoure;                  //貼圖素材

    public static void SetLoadScene(string sceneName)
    {
        if (!Application.isLoadingLevel)
        {
            Application.LoadLevelAsync(sceneName);
        }
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //  iTween.ColorTo(this.gameObject, iTween.Hash("a", 0, "looptype", iTween.LoopType.pingPong));
    }

    void Update()
    {
        if (Application.isLoadingLevel)
        {
            showGUI = true;
        }
    }

    IEnumerator OnLevelWasLoaded(int level)
    {
        yield return new WaitForSeconds(5.0F);
        Destroy(this.gameObject);
    }

    void OnGUI()
    {
        if (!showGUI) return;
        GUI.depth = this.GUIdepth;


        GUI.DrawTexture(new Rect(
            0, 0, Screen.width, Screen.height),
            this.TextureResoure
            );

    }
}