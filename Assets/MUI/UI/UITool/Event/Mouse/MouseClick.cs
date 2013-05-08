using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

    public Object DisplayObject;
    private Rect rect;

    public GameObject EffectObject;
    
    public GameObject Event;

    private GameObject MouseOver;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));


        if (rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Event)
                {
                    GameObject newGameObject = (GameObject)Instantiate(Event);
                    newGameObject.SetActive(true);
                }
                
                if (this.GetComponent<MouseOver>())
                    this.GetComponent<MouseOver>().gameObject.SetActive(false);
            }

	}
}
