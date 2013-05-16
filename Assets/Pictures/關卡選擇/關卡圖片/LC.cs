using UnityEngine;
using System.Collections;

public class LC : MonoBehaviour
{

    public GameObject HPbar;
    GameObject HPtext;
    public int nowHP;
    int maxHP;


    void Update()
    {

        HPbar.guiTexture.pixelInset = new Rect(200, 200, nowHP, 17);
    }


}