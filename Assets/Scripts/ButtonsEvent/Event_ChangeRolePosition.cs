using UnityEngine;
using System.Collections;

public class Event_ChangeRolePosition : MonoBehaviour
{

    public GameDefinition.ChangeRoleMode changeMode;
    // Use this for initialization
    void Start()
    {
        RolesController.script.ChangeRolePosition(changeMode);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
