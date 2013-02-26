using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;

    private int currentTexture { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerEnter(Collider other)
    {
        this.isExplsion = true;
        Destroy(this.GetComponent<MoveObject>());
        Destroy(this.GetComponent<RegularChangePictures>());

        //Destroy(other.gameObject);
        other.GetComponent<Life>().DecreaseLife(1);
    }

    // Use this for initialization
    void Start()
    {
        this.addValue = this.ChangeTime;  // 一開始就觸發
        this.currentTexture = 0;
        this.isExplsion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isExplsion)
        {
            if (this.addValue >= this.ChangeTime)
            {
                if (this.currentTexture >= this.ChangeTextures.Length)
                {
                    Destroy(this.gameObject);
                    return;
                }

                this.addValue = 0;
                this.renderer.material.mainTexture = this.ChangeTextures[this.currentTexture];
                this.currentTexture++;
            }

            this.addValue += Time.deltaTime;
        }
    }
}