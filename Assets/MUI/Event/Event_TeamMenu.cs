using UnityEngine;
using System.Collections;

public class Event_TeamMenu : MonoBehaviour {

    public static int ObjectCount;
    public Object[] LayoutObjects;

    public enum Type { None, Add, Dec };
    public Type type;
    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameObject in LayoutObjects)
            gameObject.SetActive(false);


        switch (type)
        {
            case Type.Add:
                if(ObjectCount < LayoutObjects.Length - 1)
                ObjectCount++;
                break;
            case Type.Dec:
                if (ObjectCount > 0)
                ObjectCount--;
                break;
        }
        if (LayoutObjects[ObjectCount]) ((GameObject)LayoutObjects[ObjectCount]).SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
