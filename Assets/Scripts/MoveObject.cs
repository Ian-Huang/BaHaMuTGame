using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public float speed = 5;
    public Vector3 Direction = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.Direction * Time.deltaTime * this.speed;
        
    }
}