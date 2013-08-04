using UnityEngine;
using System.Collections;

public class MUI_AutoType : MUI_Base
{
    public float letterPause = 0.2f;
    public AudioClip sound;

    //文字
    public string Text = "Type Text here";

    //顯示中的文字
    public string ShowText = "";

    //文字大小
    public int FontSize = 10;
    //文字對準方式
    public TextAnchor Alignment;

    // Use this for initialization
    new void Start()
    {
        if (!this.GetComponent<AudioSource>())
            this.gameObject.AddComponent<AudioSource>();

        ShowText = "";
        StartCoroutine(TypeText());

        base.Start();
    }

    IEnumerator TypeText()
    {
        foreach (char letter in Text.ToCharArray())
        {
            ShowText += letter;
            if (sound)
                audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    new void Update()
    {
        base.Update();
    }

    void OnGUI()
    {
        if (guiSkin)
            GUI.skin = this.guiSkin;

        GUI.skin.label.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.alignment = Alignment;

        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);

        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (LayoutScale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(LayoutScale, LayoutCenterPosition);

        if (Text != null)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                GUI.Label(new Rect(0, 0, _rect.width, _rect.height), ShowText);
            }
            GUI.EndGroup();
        }
    }
}