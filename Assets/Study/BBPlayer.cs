using UnityEngine;
using System.Collections;
using HolyTech.GameEntity;
using HolyTech.GuideDate;

public class BBPlayer : BloodBar {
    #region  玩家血条动态去血显示  
    private UISprite hideSprite = null;
    #endregion

    private UISprite mpSprite = null;
    private UILabel labelName = null;
    private UILabel labelLevel = null;


    #region 设置蓝条
    public void SetMp(float mp)
    {
        mpSprite.fillAmount = mp;
    }
    #endregion

    #region 更新蓝条
    public void UpdateMp()
    {
        mpSprite.fillAmount = mOwner.Mp / mOwner.MpMax;
    }

    public void UpdateLevel()
    {
        labelLevel.text = mOwner.Level.ToString();
    }
    #endregion
    Transform hideSlider = null;
    void Awake()
    {
        hpSprite = transform.Find("Control_Hp/Foreground").GetComponent<UISprite>();//绿
        mpSprite = transform.Find("Control_Mp/Foreground").GetComponent<UISprite>();
        labelLevel = transform.Find("Level").Find("Label").GetComponent<UILabel>();
        labelName = transform.Find("Name").GetComponent<UILabel>();
        hideSlider = transform.transform.Find("Control_Hp/ProgressHide");
        hideSprite = hideSlider.transform.Find("HideBg").GetComponent<UISprite>();//灰
        hideSprite.fillAmount = 0;
    }

    #region 玩家血条缓慢扣除   

    void OnDisable()
    {
        hideSprite.fillAmount = 0f;
    }

    IEnumerator SlowDown()
    {
        if (hideSlider != null && gameObject.activeInHierarchy)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                hideSprite.fillAmount -= 0.05f;
                if (hideSprite.fillAmount <= 0)
                {
                    StopCoroutine("slowDown");
                    break;
                }
            }
        }
    }
    #endregion

    #region 初始化血条值
    public override void ResetBloodBarValue()
    {
        base.ResetBloodBarValue();
        mpSprite.fillAmount = 0f;
        //labelName.text = null;
        labelLevel.text = null;
    }
    #endregion


    #region 更新血条信息
    public override void SetXueTiaoInfo()
    {
        base.SetXueTiaoInfo();
        UpdateMp();
        UpdateLevel();
        Iplayer player = (Iplayer)base.mOwner;
        labelName.text = player.GameUserNick;
    }

    #endregion

    protected override float GetHeight()
    {
        HeroConfigInfo info = null;
        if ((SceneGuideTaskManager.Instance().IsNewsGuide() != SceneGuideTaskManager.SceneGuideType.NoGuide)
            && (mOwner.entityType == EntityType.Soldier
            || mOwner.entityType == EntityType.AltarSoldier))
        {
            NpcConfigInfo nInfo = ConfigReader.GetNpcInfo(mOwner.NpcGUIDType);
            return nInfo.NpcXueTiaoHeight;
        }
        else
        {
            info = ConfigReader.GetHeroInfo(mOwner.NpcGUIDType);
        }

        return info.HeroXueTiaoHeight;
    }
}
