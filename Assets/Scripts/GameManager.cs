using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager master;


    public int TotalMorale;

    void Awake()
    {
        master = this;
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
