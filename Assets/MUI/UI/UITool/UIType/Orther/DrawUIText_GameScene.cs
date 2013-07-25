using UnityEngine;
using System.Collections;


public class DrawUIText_GameScene : MonoBehaviour
{
    public float MaxWidth;      //��r���̤j�e�סA�p�G�W�L�۰ʴ��U�@�C(px)
    public bool isAutoWordwarp = true;  //�O�_�۰ʴ���
    public string TextString;   //��J�r��

    private string talkContent;
    /*
    private UIBase uiBase;
    private GUIContent currentContent = new GUIContent();

    //�U���OValueTo Function
    
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

        this.uiBase.TargetValue = this.talkContent.Length;  //�p���r����

        if (this.uiBase.RunValueto) //�v�r�X�{ n���� (0 ~ this.talkContent.Length)
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

        this.currentContent.text = this.talkContent.Substring(0, (int)this.uiBase.CurrentValue);//�v�r�X�{
        if (this.uiBase.TextureStyle.wordWrap)
        {
            Vector2 newContentShape = this.uiBase.TextureStyle.CalcSize(this.currentContent);   //�p��GUIContent�e�h�ּe�M��

            if (newContentShape.x >= this.MaxWidth * GameDefinition.WidthOffset)    //�W�X���w���̤j�e��,�N���U�@��
            {
                this.uiBase.CurrentRect.width = this.MaxWidth * GameDefinition.WidthOffset;
                this.uiBase.CurrentRect.height = this.uiBase.TextureStyle.CalcHeight(this.currentContent, this.MaxWidth * GameDefinition.WidthOffset);//�p��GUIContent �p�G�b�Y�Ӽe�פU �e�h�ְ�
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