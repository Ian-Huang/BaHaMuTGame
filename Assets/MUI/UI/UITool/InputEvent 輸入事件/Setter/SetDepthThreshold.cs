using UnityEngine;
using System.Collections;

public class SetDepthThreshold : MonoBehaviour
{
    private int oldDepthThreshold;
    public int newDepthThreshold;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        oldDepthThreshold = MButton.DepthThreshold;
        MButton.DepthThreshold = newDepthThreshold;
    }

    void OnDisable()
    {
        MButton.DepthThreshold = oldDepthThreshold;
    }
}
