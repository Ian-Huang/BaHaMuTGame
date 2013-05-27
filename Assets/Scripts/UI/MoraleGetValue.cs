using UnityEngine;
using System.Collections;

public class MoraleGetValue : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SetOffsetValue>().Percentage.x = GameCollection.master.GetCurrentMorale();
    }
}
