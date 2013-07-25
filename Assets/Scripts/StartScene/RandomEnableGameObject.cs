using UnityEngine;
using System.Collections;

public class RandomEnableGameObject : MonoBehaviour
{
    public GameObject[] gameObjects;
    private int randomNumber;
    // Use this for initialization
    void OnEnable()
    {
        //
        gameObjects[randomNumber].SetActive(false);


        //
        Random.seed = System.DateTime.Now.Millisecond.GetHashCode();
        randomNumber = Random.Range(0, gameObjects.Length);
        gameObjects[randomNumber].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
