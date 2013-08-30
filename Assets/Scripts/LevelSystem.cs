using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-30
/// Author：Ian
/// Description：
///     角色能力等級系統
/// </summary>
public class LevelSystem : MonoBehaviour
{
    public static LevelSystem script;
    private Dictionary<GameDefinition.Role, string> RoleNameDictionary = new Dictionary<GameDefinition.Role, string>();

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        //跨場景使用
        DontDestroyOnLoad(this.gameObject);

        //設定角色名字典檔
        this.RoleNameDictionary.Add(GameDefinition.Role.狂戰士, "BSK");
        this.RoleNameDictionary.Add(GameDefinition.Role.盾騎士, "KNI");
        this.RoleNameDictionary.Add(GameDefinition.Role.獵人, "HUN");
        this.RoleNameDictionary.Add(GameDefinition.Role.魔法師, "WIZ");

        //讀取系統儲存的角色屬性資料
        for (int i = 0; i < GameDefinition.RoleList.Count; i++)
        {
            GameDefinition.RoleData data = GameDefinition.RoleList[i];

            //設定角色當前各項能力值
            PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_HP", data.Life + PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_HP_LV") * data.LifeAdd);
            PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_ATK", data.Damage + PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ATK_LV") * data.DamageAdd);
            PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_DEF", data.Defence + PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_DEF_LV") * data.DefenceAdd);
            data.UpdateAbilityValue(PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_HP"), PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ATK"), PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_DEF"));

            //設定角色各項能力當前升級所需花費的金錢數量 (如已達最大等級，則升級花費為0)
            int level = -1;
            if (GameDefinition.AbilityCostLevel.Count == (level = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ATK_LV")))
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_ATK_M", 0);
            else
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_ATK_M", GameDefinition.AbilityCostLevel[PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ATK_LV")]);

            if (GameDefinition.AbilityCostLevel.Count == (level = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_DEF_LV")))
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_DEF_M", 0);
            else
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_DEF_M", GameDefinition.AbilityCostLevel[PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_DEF_LV")]);

            if (GameDefinition.AbilityCostLevel.Count == (level = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_HP_LV")))
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_HP_M", 0);
            else
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_HP_M", GameDefinition.AbilityCostLevel[PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_HP_LV")]);

            if (GameDefinition.AttackLVCostLevel.Count == (level = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_BASIC_LV")))
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_BASIC_LV_M", 0);
            else
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_BASIC_LV_M", GameDefinition.AttackLVCostLevel[PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_BASIC_LV")]);

            if (GameDefinition.UltimateSkillCostLevel.Count == (level = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ULT_LV")))
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_ULT_LV_M", 0);
            else
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[data.RoleName] + "_ULT_LV_M", GameDefinition.UltimateSkillCostLevel[PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[data.RoleName] + "_ULT_LV")]);
        }
    }

    /// <summary>
    /// 提升角色能力等級
    /// </summary>
    /// <param name="role">角色名</param>
    /// <param name="type">提升能力類型</param>
    public void LevelUp(GameDefinition.Role role, LevelUpType type)
    {
        int currentLV = -1;
        int cost = -1;
        GameDefinition.RoleData getData;

        switch (type)
        {
            case LevelUpType.生命:
                currentLV = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_HP_LV");
                //如果該能力已經升至最大等級，則不進行升級
                if (currentLV >= GameDefinition.AbilityCostLevel.Count)
                    return;

                //將目前金幣數扣除升級能力所需的費用
                if (0 > (cost = PlayerPrefsDictionary.script.GetValue("Money") - PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_HP_M")))
                    return;
                PlayerPrefsDictionary.script.SetValue("Money", cost);

                //將能力升一級
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_HP_LV", currentLV + 1);

                //如果該能力在升級後，達最大等級，則升級所需金額設為0
                if ((currentLV + 1) == GameDefinition.AbilityCostLevel.Count)
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_HP_M", 0);
                else
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_HP_M", GameDefinition.AbilityCostLevel[currentLV + 1]);

                //讀取系統儲存的角色屬性資料
                getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == role; });
                //儲存升級後的能力至系統
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_HP", getData.Life + getData.LifeAdd);
                getData.UpdateAbilityValue(PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_HP"), getData.Damage, getData.Defence);
                break;

            case LevelUpType.攻擊:
                currentLV = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_ATK_LV");
                //如果該能力已經升至最大等級，則不進行升級
                if (currentLV >= GameDefinition.AbilityCostLevel.Count)
                    return;

                //將目前金幣數扣除升級能力所需的費用
                if (0 > (cost = PlayerPrefsDictionary.script.GetValue("Money") - PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_ATK_M")))
                    return;
                PlayerPrefsDictionary.script.SetValue("Money", cost);

                //將能力升一級
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ATK_LV", currentLV + 1);

                //如果該能力在升級後，達最大等級，則升級所需金額設為0
                if ((currentLV + 1) == GameDefinition.AbilityCostLevel.Count)
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ATK_M", 0);
                else
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ATK_M", GameDefinition.AbilityCostLevel[currentLV + 1]);

                //讀取系統儲存的角色屬性資料
                getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == role; });
                //儲存升級後的能力至系統
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ATK", getData.Damage + getData.DamageAdd);
                getData.UpdateAbilityValue(getData.Life, PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_ATK"), getData.Defence);
                break;

            case LevelUpType.防禦:
                currentLV = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_DEF_LV");
                //如果該能力已經升至最大等級，則不進行升級
                if (currentLV >= GameDefinition.AbilityCostLevel.Count)
                    return;

                //將目前金幣數扣除升級能力所需的費用
                if (0 > (cost = PlayerPrefsDictionary.script.GetValue("Money") - PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_DEF_M")))
                    return;
                PlayerPrefsDictionary.script.SetValue("Money", cost);

                //將能力升一級
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_DEF_LV", currentLV + 1);

                //如果該能力在升級後，達最大等級，則升級所需金額設為0
                if ((currentLV + 1) == GameDefinition.AbilityCostLevel.Count)
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_DEF_M", 0);
                else
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_DEF_M", GameDefinition.AbilityCostLevel[currentLV + 1]);

                //讀取系統儲存的角色屬性資料
                getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == role; });
                //儲存升級後的能力至系統
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_DEF", getData.Defence + getData.DefenceAdd);
                getData.UpdateAbilityValue(getData.Life, getData.Damage, PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_DEF"));
                break;

            case LevelUpType.普攻等級:
                currentLV = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_BASIC_LV");
                //如果該能力已經升至最大等級，則不進行升級
                if (currentLV >= GameDefinition.AttackLVCostLevel.Count)
                    return;

                //將目前金幣數扣除升級能力所需的費用
                if (0 > (cost = PlayerPrefsDictionary.script.GetValue("Money") - PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_BASIC_LV_M")))
                    return;
                PlayerPrefsDictionary.script.SetValue("Money", cost);

                //將能力升一級
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_BASIC_LV", currentLV + 1);

                //如果該能力在升級後，達最大等級，則升級所需金額設為0
                if ((currentLV + 1) == GameDefinition.AttackLVCostLevel.Count)
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_BASIC_LV_M", 0);
                else
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_BASIC_LV_M", GameDefinition.AttackLVCostLevel[currentLV + 1]);
                break;

            case LevelUpType.絕技等級:
                currentLV = PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_ULT_LV");
                //如果該能力已經升至最大等級，則不進行升級
                if (currentLV >= GameDefinition.UltimateSkillCostLevel.Count)
                    return;

                //將目前金幣數扣除升級能力所需的費用
                if (0 > (cost = PlayerPrefsDictionary.script.GetValue("Money") - PlayerPrefsDictionary.script.GetValue(this.RoleNameDictionary[role] + "_ULT_LV_M")))
                    return;
                PlayerPrefsDictionary.script.SetValue("Money", cost);

                //將能力升一級
                PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ULT_LV", currentLV + 1);

                //如果該能力在升級後，達最大等級，則升級所需金額設為0
                if ((currentLV + 1) == GameDefinition.UltimateSkillCostLevel.Count)
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ULT_LV_M", 0);
                else
                    PlayerPrefsDictionary.script.SetValue(this.RoleNameDictionary[role] + "_ULT_LV_M", GameDefinition.UltimateSkillCostLevel[currentLV + 1]);
                break;
        }
    }

    public enum LevelUpType
    {
        生命 = 0, 攻擊 = 1, 防禦 = 2, 普攻等級 = 3, 絕技等級 = 4
    }
}