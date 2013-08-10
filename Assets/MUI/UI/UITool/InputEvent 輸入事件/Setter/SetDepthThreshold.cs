using UnityEngine;
using System.Collections;

/// <summary>
/// 設定深度的門檻值
/// </summary>
/// 備註：此部分是設定深度值，MButton會抓取Tex2D的Depth做比較，如果Tex2D的Depth值比門檻高，則Tex2D的按鈕不作用
/// 簡單來說，利用Depth的值來控制按鈕是否作用
public class SetDepthThreshold : MonoBehaviour
{
    private int oldDepthThreshold;
    public int newDepthThreshold;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        oldDepthThreshold = MButton.DepthThreshold;
        MButton.DepthThreshold = newDepthThreshold;
    }

    void OnDisable()
    {
        MButton.DepthThreshold = oldDepthThreshold;
    }
}
