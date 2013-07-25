using UnityEngine;
using System.Collections;

/// <summary>
/// 將超出邊界的物件刪除
/// </summary>
public class BorderDestroy : MonoBehaviour
{
    public float DestroyRadius = 15;     //邊界球形的半徑
    public LayerMask DestroyLayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //確認是否有物體進入範圍
        if (Physics.CheckSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
        {
            //刪除進入範圍內的物件
            foreach (var obj in Physics.OverlapSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
                Destroy(obj.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測邊界
        Gizmos.DrawWireSphere(this.transform.position, this.DestroyRadius);
    }
}
