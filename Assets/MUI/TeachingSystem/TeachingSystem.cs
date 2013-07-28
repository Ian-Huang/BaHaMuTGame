using UnityEngine;
using System.Collections;

public class TeachingSystem : MonoBehaviour
{

    public static TeachingSystem script;
    //目前階段編號
    private int currentPartNumber;
    public GameObject[] TechingParts;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //測試用
        if (Input.GetKeyDown(KeyCode.C))
            NextPart();
    }

    public void NextPart()
    {
        if (currentPartNumber - 1 >= 0)
            TechingParts[currentPartNumber - 1].SetActive(false);

        if (currentPartNumber < TechingParts.Length)
        {
            TechingParts[currentPartNumber].SetActive(true);
            currentPartNumber++;
        }

    }
}
