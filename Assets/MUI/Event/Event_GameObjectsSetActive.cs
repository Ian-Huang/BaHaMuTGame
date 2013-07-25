using UnityEngine;
using System.Collections;

public class Event_GameObjectsSetActive : MonoBehaviour
{
    //Activeªºª¬ºA
    public bool ActiveStatus;
    public GameObject[] gameObjects;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameobject in gameObjects)
            gameobject.SetActive(ActiveStatus);

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
