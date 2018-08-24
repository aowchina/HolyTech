using System;
using UnityEngine;
using HolyTech.GameEntity;

public class BBManager : MonoBehaviour {
    
    #region 血条路径
    private static string pathPlayerXuetiao = "HeroLifePlate{0}";
    private static string pathBuildingXuetiao = "TowerLifePlate{0}";
    private static string pathSummonedXuetiao = "Summoned{0}";
    private static string pathSoldierXuetiao = "SoldierLifePlate{0}";
    public static string pathMonster = "CreepsLifePlate";

    public static string xueTiaoName = "XueTiao_";
    #endregion


    public static BBManager Instance{
        private set;
        get;
    }

    void OnEnable(){
        if(Instance == null){
            Instance = this;
        }
    }


    private string GetBarPrefabPath(Ientity entity){
        string barColor = "Green";

        if (PlayerManager.Instance.LocalPlayer == null || PlayerManager.Instance.LocalPlayer.realObject == null)
        {
            int campEntity = (int)entity.EntityCamp % 2;
            int playerCamp = (int)PlayerManager.Instance.LocalAccount.GameUserSeat % 2;
            if (playerCamp != campEntity) {
                barColor = "Red";
            }
        }
        else if (entity.EntityCamp == EntityCampType.CampTypeBad || (PlayerManager.Instance.LocalPlayer != null && entity.EntityCamp != PlayerManager.Instance.LocalPlayer.EntityCamp))
        {            
            barColor = "Red";
        }
        
        if (PlayerManager.Instance.LocalAccount.ObjType == GameDefine.ObPlayerOrPlayer.PlayerObType && entity.EntityCamp != PlayerManager.Instance.LocalAccount.EntityCamp)
        {
            barColor = "Red";
        }
        string path = String.Format(pathPlayerXuetiao, barColor);
        if (entity.NPCCateChild == ENPCCateChild.eNPCChild_BUILD_Summon)
        {
            path = String.Format(pathSummonedXuetiao, barColor);
        }
        return GameDefine.GameConstDefine.GuisPlay + path;
    }

    public BloodBar CreateBloodBar(Ientity entity){
        string path = GetBarPrefabPath(entity);
        BloodBar bloodBar = LoadBarPrefab(entity, path);
        bloodBar.Init(entity);
        bloodBar.ResetBloodBarValue();
        bloodBar.UpdatePosition(entity.realObject.transform);
        bloodBar.gameObject.transform.parent = transform;
        bloodBar.transform.localScale = Vector3.one;
        bloodBar.gameObject.transform.name = xueTiaoName + entity.ModelName;
        return bloodBar;
    }

    public BloodBar LoadBarPrefab(Ientity entity,string path)
    {        
        GameObject obj = GameObjectPool.Instance.GetGO(path);
        if (obj == null)
            Debug.LogError("obj = null");
        BloodBar bar = obj.GetComponent<BloodBar>();
        bar.mResName = path;

        return bar;
    } 
}
