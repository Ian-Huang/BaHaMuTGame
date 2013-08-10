using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class MPlatformButton : MButton
{
    [HideInInspector]
    public bool[] isPress = new bool[4];
}
