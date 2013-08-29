using UnityEngine;
using System.Collections;

public class TeachSystemLayoutController : MonoBehaviour
{

    public int fontSize;
    public Texture2D tex;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (MUI_AutoType autoType in GameObject.FindObjectsOfType(typeof(MUI_AutoType)))
        {
            autoType.FontSize = fontSize;
        }

        foreach (MUI_Texture_2D tex2D in GameObject.FindObjectsOfType(typeof(MUI_Texture_2D)))
        {
            if (tex2D.name == "Tex2D - 字框背景")
            {
                tex2D.Texture2d = tex;
            }
        }
    }
}
