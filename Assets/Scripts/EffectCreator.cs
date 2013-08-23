using UnityEngine;
using System.Collections;

//特效製造機
public class EffectCreator : MonoBehaviour
{
    public static EffectCreator script;

    public GameObject[] 道路危險提示;
    public GameObject 遊戲開始提示;
    public GameObject 魔王接近提示;

    public bool isBossUIEffectShow;
    public GameObject UIBoss_Health_LayoutMove;
    public GameObject UIMap_Progress_LayoutMove;
    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossUIEffectShow)
        {
            isBossUIEffectShow = false;
            UIBoss_Health_LayoutMove.SetActive(true);
            UIMap_Progress_LayoutMove.SetActive(true);
        }
    }


}
