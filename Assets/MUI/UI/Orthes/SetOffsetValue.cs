using UnityEngine;
using System.Collections;

public class SetOffsetValue : MonoBehaviour {

    public Vector2 From;
    public Vector2 To;
    public Vector2 Percentage;
    public Object DisplayObject;

    public bool x;
    public bool y;

    private Vector2 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        offset = (Vector2)(DisplayObject.GetType().GetField("offset").GetValue(DisplayObject));

        if (x)
        {
            offset.x = Mathf.Lerp(From.x, To.x, Percentage.x / 100);
            this.GetComponent<Texture_2D>().offset.x = offset.x;
        }
        if (y)
        {
            offset.y = Mathf.Lerp(From.y, To.y, Percentage.y / 100);
            this.GetComponent<Texture_2D>().offset.y = offset.y;
        }

	}
}
