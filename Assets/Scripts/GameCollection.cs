using UnityEngine;
using System.Collections;

public class GameCollection : MonoBehaviour
{
    public static GameCollection master;

    public int CurrentMorale;
    public int MoraleRestoreRate;
    public int MaxMorale;

    void Awake()
    {
        master = this;
    }

    // Use this for initialization
    void Start()
    {
        this.MaxMorale = GameDefinition.MaxMorale;
        this.CurrentMorale = GameDefinition.MaxMorale;
        this.MoraleRestoreRate = GameDefinition.MoraleRestoreRate;

        InvokeRepeating("RestoreMoralePersecond", 0.1f, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 每秒固定回復士氣值
    /// </summary>
    void RestoreMoralePersecond()
    {
        this.CurrentMorale += this.MoraleRestoreRate;
        if (this.CurrentMorale >= this.MaxMorale)
            this.CurrentMorale = this.MaxMorale;
    }
}
