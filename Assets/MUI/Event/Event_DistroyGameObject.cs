using UnityEngine;
using System.Collections;

public class Event_DistroyGameObject : MonoBehaviour
{
    public bool isThisGameObject;
    public GameObject[] gameObjects;
    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameobject in gameObjects)
            Destroy(gameobject);
        if (isThisGameObject)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
