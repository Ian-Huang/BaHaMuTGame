using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour
{
    public Vector2 OffsetSpeed;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        this.renderer.material.mainTextureOffset += OffsetSpeed * Time.deltaTime;

        if (this.renderer.material.mainTextureOffset.x >= 1)
            this.renderer.material.mainTextureOffset -= new Vector2(1, 0);
        else if (this.renderer.material.mainTextureOffset.x <= -1)
            this.renderer.material.mainTextureOffset += new Vector2(1, 0);

        if (this.renderer.material.mainTextureOffset.y >= 1)
            this.renderer.material.mainTextureOffset -= new Vector2(0, 1);
        else if (this.renderer.material.mainTextureOffset.y <= -1)
            this.renderer.material.mainTextureOffset += new Vector2(0, 1);
    }
}