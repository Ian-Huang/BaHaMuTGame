using UnityEngine;
using System.Collections;

public class Event_ChangeRolePosition : MonoBehaviour
{

    public GameDefinition.ChangeRoleMode changeMode;
    // Use this for initialization
    void Start()
    {
        RolesCollection.script.ChangeRolePosition(changeMode);
        Destroy(this.gameObject);
        if (AudioSoundPlayer.script)
            AudioSoundPlayer.script.PlayAudio("¤}½b¤âµo®g½b¥Ú");

        PlayerPrefsDictionary.script.SetValue("Money", 10);
    } 

    // Update is called once per frame
    void Update()
    {

    }
}
