using UnityEngine;
using HolyTech.GameEntity;
using GameDefine;

public class BloodBar : MonoBehaviour { 
    public Ientity mOwner;
	public string mResName;
    private bool mNeedUpdate = true;   
    
    #region  私有变量区   
    protected UISprite hpSprite = null;
    private Camera _ui2dCamera;
    private Camera _mainCamera;
    private float barHeight = 0f;
    protected UILabel labelCost;
    protected const int CanControl = 1;
    #endregion

    public UILabel uiDebugInfo;

    #region 获得所需要的transform ,ex hpSlider,mpSlider,name label
    public virtual void Init(Ientity go)
    {
        _mainCamera = Camera.main;
        _ui2dCamera = GameMethod.GetUiCamera;
        uiDebugInfo = this.transform.Find("Name").GetComponent<UILabel>();
        uiDebugInfo.gameObject.SetActive(true);
		mOwner = go;
        barHeight = GetHeight();
    }
    #endregion

    #region 初始化血条
    public virtual void ResetBloodBarValue()
    {
        hpSprite.fillAmount = 0f;
    }
    #endregion

    #region 创建血条之后，接收到服务器发送的血条信息，初始化血条
    public virtual void SetXueTiaoInfo()
    {
        UpdateHP();
    }
    #endregion

    //更新血条
    public void UpdateHP()
    {
        hpSprite.fillAmount = mOwner.Hp / mOwner.HpMax;
    }

    public virtual void IsBloodBarCpVib(bool isVis)
    {
        if (labelCost == null)
            return;
        if (isVis)
        {
            NpcConfigInfo info = ConfigReader.GetNpcInfo(mOwner.NpcGUIDType);

            int cp = (int)info.NpcConsumeCp;
            if (info.NpcCanControl == CanControl)
            {
                if (!mOwner.IsSameCamp(PlayerManager.Instance.LocalPlayer.EntityCamp)
                    && (mOwner.entityType == EntityType.AltarSoldier || mOwner.entityType == EntityType.Soldier))
                {
                    cp *= 2;
                }
            }
        }
        labelCost.gameObject.SetActive(isVis);
    }

    protected virtual float GetHeight()
    {
        NpcConfigInfo info = ConfigReader.GetNpcInfo(mOwner.NpcGUIDType);
        return info.NpcXueTiaoHeight;
    }

    protected virtual void Update()
    {
        if (mNeedUpdate && mOwner != null && mOwner.realObject != null)
        {
            UpdatePosition(mOwner.realObject.transform);
        }
    }

    public void SetVisible(bool val)
    {
        if (!val)
        {
            transform.localPosition = new Vector3(5000, 0, 0);
        }
        mNeedUpdate = val;
    }

    public void UpdatePosition(Transform target)
    {
        if (_mainCamera == null || _ui2dCamera == null)
            return;
        // 血条对应在3d场景的位置
        Vector3 pos_3d = target.position + new Vector3(0f, barHeight, 0f);
        // 血条对应在屏幕的位置
        Vector2 pos_screen = _mainCamera.WorldToScreenPoint(pos_3d);
        // 血条对应在ui中的位置
        Vector3 pos_ui = _ui2dCamera.ScreenToWorldPoint(pos_screen);
        transform.position = Vector3.Slerp(transform.position, pos_ui, Time.time);
	}
}
