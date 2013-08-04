using UnityEngine;
using System.Collections;

public class Event_LayoutEnable : MonoBehaviour
{
    public int ObjectCount;
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
                ObjectCount++;
                break;
            case Type.Dec:
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
