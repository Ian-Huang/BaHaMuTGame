using UnityEngine;
using System.Collections;


public class DrawUIText_GameScene : MonoBehaviour
{
    public float MaxWidth;      //文字的最大寬度，如果超過自動換下一列(px)
    public bool isAutoWordwarp = true;  //是否自動換行
    public string TextString;   //輸入字串

    private string talkContent;
    /*
    private UIBase uiBase;
    private GUIContent currentContent = new GUIContent();

    //下面是ValueTo Function
    
    //public void ValueTo()
    //{
    //    iTween.ValueTo(this.gameObject, iTween.Hash(
    //        "from", this.CurrentValue,
    //        "to", this.TargetValue,
    //        "delay", this.ValueDelay,
    //        "time", this.ValueTime,
    //        "easetype", this.ValueEasetype,
    //        "onupdate", "updateValue",
    //         "loopType", this.ValueLooptype,
    //         "name", "ValueTo"));
    //}

    //--------------------------

    void Start()
    {
        this.talkContent = this.TextString;

        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");

        this.uiBase.TextureStyle.wordWrap = this.isAutoWordwarp;    //GUIStyle

        this.uiBase.TargetValue = this.talkContent.Length;  //計算文字長度

        if (this.uiBase.RunValueto) //逐字出現 n秒內由 (0 ~ this.talkContent.Length)
        {
            //this.uiBase.StopValueTo();
            this.uiBase.ValueTo();
        }
    }

    public void SetTextContent(string content)
    {
        this.talkContent = content;
    }

    // Update is called once per frame
    void Update()
    {

        this.currentContent.text = this.talkContent.Substring(0, (int)this.uiBase.CurrentValue);//逐字出現
        if (this.uiBase.TextureStyle.wordWrap)
        {
            Vector2 newContentShape = this.uiBase.TextureStyle.CalcSize(this.currentContent);   //計算GUIContent占多少寬和高

            if (newContentShape.x >= this.MaxWidth * GameDefinition.WidthOffset)    //超出限定的最大寬度,就換下一行
            {
                this.uiBase.CurrentRect.width = this.MaxWidth * GameDefinition.WidthOffset;
                this.uiBase.CurrentRect.height = this.uiBase.TextureStyle.CalcHeight(this.currentContent, this.MaxWidth * GameDefinition.WidthOffset);//計算GUIContent 如果在某個寬度下 占多少高
            }
            else
            {
                this.uiBase.CurrentRect.width = newContentShape.x;
                this.uiBase.CurrentRect.height = newContentShape.y;
            }
        }
    }

    void OnGUI()
    {
        GUI.depth = this.uiBase.GUIdepth;
        GUI.color = this.uiBase.CurrentColor;

        GUI.Box(new Rect(
                this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
                this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                this.uiBase.CurrentRect.width,
                this.uiBase.CurrentRect.height),
        this.currentContent, this.uiBase.TextureStyle);
    }
     * */
}