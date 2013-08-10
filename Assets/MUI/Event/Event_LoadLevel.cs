using UnityEngine;
using System.Collections;

public class Event_LoadLevel : MonoBehaviour
{

    public string LoadLevelName;
    public float DelayTime;
    // Use this for initialization
    void Start()
    {
        MUI_LoadSceneTransitionsEffect.script.LoadScene(LoadLevelName, DelayTime);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
