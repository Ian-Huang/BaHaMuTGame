using UnityEngine;
using System.Collections;

public class UI_LevelController : MonoBehaviour {

    //根據PlayerPref的關卡目前破關數值
    //將指定的物件Enable

    public int levelValue;
    public GameObject CanUseLevel;
    public GameObject CantUseLevel;
	// Use this for initialization
	void Start () {
        if (PlayerPrefsDictionary.script.GetValue("LevelComplete") > levelValue)
        {
            CanUseLevel.SetActive(true);
            CantUseLevel.SetActive(false);
        }
        else
            CantUseLevel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            PlayerPrefsDictionary.script.SetValue("LevelComplete", 15);

        //print(PlayerPrefsDictionary.script.GetValue("LevelComplete"));
	}
}
