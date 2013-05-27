using UnityEngine;
using System.Collections;

public class BloodBarController : MonoBehaviour
{
    public RolePropertyInfo RolePropertyInfo_Script;

    // Use this for initialization
    void Start()
    {
        if (!this.RolePropertyInfo_Script)
            this.transform.parent.parent.GetComponent<RolePropertyInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        this.renderer.material.mainTextureOffset = new Vector2(Mathf.Lerp(1, 0, (float)this.RolePropertyInfo_Script.currentLife / this.RolePropertyInfo_Script.maxLife), 0);
    }
}