using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-25
/// Modify Date�G2013-08-09
/// Author�GIan
/// Description�G
///     �N�W�X��ɪ�����R��
///     0809�s�W�G�������󪺦P�ɡA�������U��GameManager AllBoneAnimationList������T
/// </summary>
public class BorderDestroy : MonoBehaviour
{
    public float DestroyRadius;         //��ɲy�Ϊ��b�|
    public LayerMask DestroyLayer;      //�n�Q�R����Layer

    // Update is called once per frame
    void Update()
    {
        //�T�{�O�_������i�J�d��
        if (Physics.CheckSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
        {
            //�R���i�J�d�򤺪�����
            foreach (var obj in Physics.OverlapSphere(this.transform.position, this.DestroyRadius, this.DestroyLayer))
            {
                //�������󪺦P�ɡA�������U��GameManager AllBoneAnimationList������T
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
        //�e�X�������
        Gizmos.DrawWireSphere(this.transform.position, this.DestroyRadius);
    }
}
