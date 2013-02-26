using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject ShootObject;              
    void OnTriggerEnter(Collider other)
    {
        Instantiate(this.ShootObject, this.transform.position, this.ShootObject.transform.rotation);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
