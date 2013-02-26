using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;             //交換時間間隔

    private int currentTextureIndex { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerEnter(Collider other)
    {        
        this.isExplsion = true;
        this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
        Destroy(this.GetComponent<MoveObject>());
        Destroy(this.GetComponent<RegularChangePictures>());

        //Destroy(other.gameObject);
        other.GetComponent<Life>().DecreaseLife(1);
    }

    // Use this for initialization
    void Start()
    {
        this.addValue = 0;
        this.currentTextureIndex = 0;
        this.isExplsion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isExplsion)
        {
            if (this.addValue >= this.ChangeTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.ChangeTextures.Length)
                {
                    Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}