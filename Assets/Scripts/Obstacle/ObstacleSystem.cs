using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-29
/// Modify Date�G2013-07-29
/// Author�GIan
/// Description�G
///     ��ê���t�� 
/// </summary>
public class ObstacleSystem : MonoBehaviour
{
    public List<GameDefinition.Obstacle> ObstacleList = new List<GameDefinition.Obstacle>();

    private RolePropertyInfo roleInfo { get; set; }

    void OnTriggerEnter(Collider other)
    {
        ObstaclePropertyInfo infoScript = other.gameObject.GetComponent<ObstaclePropertyInfo>();
        if (infoScript == null)
            return;

        if (this.ObstacleList.Contains(infoScript.Obstacle))
        {
            infoScript.CheckObstacle(true);
        }
        else
        {
            infoScript.CheckObstacle(false);
            this.roleInfo.DecreaseLife(infoScript.Damage);
        }
    }

    // Use this for initialization
    void Start()
    {
        //���J�����T
        this.roleInfo = this.GetComponent<RolePropertyInfo>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
