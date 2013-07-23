using UnityEngine;
using System.Collections;

/// <summary>
/// �D�n�\��G�i�H�ǥѥ~�����ܤ����ܼ�
/// Offset �ʱ�
/// ���ܡ@Texture2D�@�Ρ@Label �� Offset ��
///  * �HKey���覡�M��ʱ�Dictionary���������
/// </summary>
public class MUI_OffsetMonitor : MonoBehaviour
{
    public Vector2 From;
    public Vector2 To;
    public string Key;

    [HideInInspector]
    public Vector2 Percentage;
    [HideInInspector]
    public MUI_Enum.MUIType MUI_Type;

    private Vector2 offset;

    private MUI_Monitor mMonitor = new MUI_Monitor();
    // Use this for initialization
    void Start()
    {
       
        //�HKey���r����U�@�ӯ���
        if (Key != "") mMonitor.SubmitKey(Key + "x");
        if (Key != "") mMonitor.SubmitKey(Key + "y");
    }

    // Update is called once per frame
    void Update()
    {
        if (mMonitor.isValid(Key + "x")) Percentage.x = MUI_Monitor.MonitorDictionary[Key + "x"];
        if (mMonitor.isValid(Key + "y")) Percentage.y = MUI_Monitor.MonitorDictionary[Key + "y"];
        offset.x = Mathf.Lerp(From.x, To.x, Percentage.x / 100);
        offset.y = Mathf.Lerp(From.y, To.y, Percentage.y / 100);


        //�X�ʤ������ܤ�
        if (this.GetComponent<MUI_Texture_2D>()) this.GetComponent<MUI_Texture_2D>().offset = offset;
        if (this.GetComponent<MUI_Label>()) this.GetComponent<MUI_Label>().offset = offset;
    }
}
