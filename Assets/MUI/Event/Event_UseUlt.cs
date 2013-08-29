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
                script.gameObject.GetComponent<RoleAttackController>().RunUniqueSkill();
                break;
            }
        }
    }

}
