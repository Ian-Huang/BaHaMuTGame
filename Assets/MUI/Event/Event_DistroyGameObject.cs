using UnityEngine;
using System.Collections;

public class Event_DistroyGameObject : MonoBehaviour
{
    public bool isThisGameObject;
    public GameObject[] gameObjects;
    public float delayTime;
    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameobject in gameObjects)
            Destroy(gameobject, delayTime);
        if (isThisGameObject)
            Destroy(this.gameObject, delayTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
