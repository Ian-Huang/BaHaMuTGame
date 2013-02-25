using UnityEngine;
using System.Collections;

public class ControllerCollection : MonoBehaviour
{
    public GameObject Sidewalk1;
    public GameObject Sidewalk2;
    public GameObject Sidewalk3;
    public GameObject Sidewalk4;

    private GameObject tempGameObject;
    private Vector3 tempPosition;

    // Use this for initialization
    void Start()
    {

    }

    void SetChange(ref GameObject obj1, ref GameObject obj2)
    {
        GameObject tempGameObject;
        Vector3 tempPosition;

        tempGameObject = obj1;
        tempPosition = obj1.transform.position;
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = tempPosition;
        obj1 = obj2;
        obj2 = tempGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            this.SetChange(ref this.Sidewalk1, ref this.Sidewalk2);

        if (Input.GetKeyUp(KeyCode.S))
            this.SetChange(ref this.Sidewalk2, ref this.Sidewalk3);

        if (Input.GetKeyUp(KeyCode.D))
            this.SetChange(ref this.Sidewalk3, ref this.Sidewalk4);
    }
}