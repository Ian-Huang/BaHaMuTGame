using UnityEngine;
using System.Collections;

public class BorderDestroy : MonoBehaviour
{
    public float DestroyRadius = 15;     //��ɲy�Ϊ��b�|
    
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(this.transform.position, this.DestroyRadius))
        { 
            Collider[] objs;
            objs = Physics.OverlapSphere(this.transform.position, this.DestroyRadius);
            foreach(var obj in objs)
                Destroy(obj.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�������
        Gizmos.DrawWireSphere(this.transform.position, this.DestroyRadius);
    }
}
