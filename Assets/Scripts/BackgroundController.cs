using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController script;

    public float BackgroundMoveSpeed;

    public bool isRunning;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isRunning)
            this.transform.Translate(this.BackgroundMoveSpeed * Time.deltaTime, 0, 0);
    }
}
