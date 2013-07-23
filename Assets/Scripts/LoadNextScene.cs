using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{   
    public int GUIdepth;                            //�K�ϲ`��(�ȶV�p�V��e)
    public Texture TextureResoure;                  //�K�ϯ���

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
        iTween.ColorTo(this.gameObject, iTween.Hash("a", 0, "looptype", iTween.LoopType.pingPong));
    }

    void OnLevelWasLoaded(int level)
    {
        Destroy(this.gameObject);        
    }
    
    void OnGUI()
    {
        GUI.depth = this.GUIdepth;

        if (Application.isLoadingLevel)
        {
            GUI.Box(new Rect(
                0,0, Screen.width, Screen.height),
                this.TextureResoure
                );
        }
    }
}