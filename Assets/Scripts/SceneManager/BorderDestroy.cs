using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-25
/// Modify Date：2013-08-09
/// Author：Ian
/// Description：
///     將超出邊界的物件刪除
///     0809新增：移除物件的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
/// </summary>
public class BorderDestroy : MonoBehaviour
{
    public float DestroyRadius;         //邊界球形的半徑
    public LayerMask DestroyLayer;      //要被刪除的Layer

    // Update is called once per frame
    void Update()
    {
        //確認是否有物體進入範圍
        if (Physics.CheckSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
        {
            //刪除進入範圍內的物件
            foreach (var obj in Physics.OverlapSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
            {
                //移除物件的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
                SmoothMoves.BoneAnimation boneAnimation = obj.gameObject.GetComponent<SmoothMoves.BoneAnimation>();
                if (boneAnimation != null)
                    if (GameManager.script.AllBoneAnimationList.ContainsKey(boneAnimation))
                        GameManager.script.AllBoneAnimationList.Remove(boneAnimation);

                Destroy(obj.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測邊界
        Gizmos.DrawWireSphere(this.transform.position, this.DestroyRadius);
    }
}
