using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    public GameObject ShootObject;

    public Texture[] ChangeTextures;
    public float[] ChangeTime;             //交換時間間隔

    public int currentTextureIndex;
    private float addValue { get; set; }
    private bool isShoot { get; set; }
    
    void OnTriggerEnter(Collider other)
    {
        this.isShoot = true;
        this.GetComponent<RegularChangePictures>().ChangeState(false);
        this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
    }
    
    // Use this for initialization
    void Start()
    {
        this.currentTextureIndex = 0;
        this.addValue = 0;  
        this.isShoot = false;
    }

    void Reset()
    {
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isShoot)
        {
            if (this.addValue >= this.ChangeTime[currentTextureIndex])
            {                
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.ChangeTextures.Length)
                {                    
                    Instantiate(this.ShootObject, this.transform.position, this.ShootObject.transform.rotation);
                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.Reset();
                    return;
                }
                renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];                
            }

            this.addValue += Time.deltaTime;
        }
    }
}
