using UnityEngine;
using System.Collections;

public class TeachingSystem_NextPart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine( TeachingSystem.script.NextPart(0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
