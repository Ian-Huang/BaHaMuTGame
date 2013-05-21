using UnityEngine;
using System.Collections;

public class SetRectValue : MonoBehaviour {

    public Vector2 From;
    public Vector2 To;
    public Vector2 Percentage;
    public Object DisplayObject;

    private Rect rect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        rect.x = Mathf.Lerp(From.x,To.x,Percentage.x/100);
        rect.y = Mathf.Lerp(From.y,To.y,Percentage.y/100);

        this.GetComponent<Texture_2D>().rect.x = rect.x;
        this.GetComponent<Texture_2D>().rect.y = rect.y;

	}
}
