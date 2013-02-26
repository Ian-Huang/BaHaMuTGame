using UnityEngine;
using System.Collections;

public class RolesController : MonoBehaviour
{
    public GameObject Role1;
    public GameObject Role2;
    public GameObject Role3;
    public GameObject Role4;

    private GameObject tempGameObject;
    private Vector3 tempPosition;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 交換物體位置
    /// </summary>
    /// <param name="obj1">物體1</param>
    /// <param name="obj2">物體2</param>
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
            this.SetChange(ref this.Role1, ref this.Role2);

        if (Input.GetKeyUp(KeyCode.S))
            this.SetChange(ref this.Role2, ref this.Role3);

        if (Input.GetKeyUp(KeyCode.D))
            this.SetChange(ref this.Role3, ref this.Role4);
    }
}