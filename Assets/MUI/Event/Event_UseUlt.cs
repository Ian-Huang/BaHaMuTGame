using UnityEngine;
using System.Collections;

public class Event_UseUlt : MonoBehaviour
{
    public GameDefinition.Role charClass;

    // Use this for initialization
    void Start()
    {
        foreach (RolePropertyInfo script in GameObject.FindObjectsOfType(typeof(RolePropertyInfo)))
        {
            if (script.Role == charClass)
            {
                //從GameManager 確認BoneAnimation的狀態
                if (GameManager.script.GetBoneAnimationState(script.boneAnimation))
                {
                    // 角色必須未虛弱
                    if (!script.isWeak)
                    {
                        script.gameObject.GetComponent<RoleAttackController>().RunUniqueSkill();
                        break;
                    }
                }
            }
        }
    }
}