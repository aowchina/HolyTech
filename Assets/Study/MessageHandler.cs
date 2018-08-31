using System.IO;
using Common.GameData;
using HolyTech;
using HolyTech.Ctrl;
using HolyTech.GameData;
using HolyTech.Model;
using UnityEngine;
using GSToGC;
using System;
using HolyTech.Effect;
using System.Collections;

public partial class MessageHandler: UnitySingleton<MessageHandler> {
    /////////////////////消息处理///////////////////////

    public int OnNotifySkillModelBufEffect(GSToGC.BuffEffect pMsg)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelBufCoroutine(pMsg));
        return (Int32)EErrorCode.eNormal;
    }
     IEnumerator OnNetMsg_NotifySkillModelBufCoroutine(GSToGC.BuffEffect pMsg)
    {     
        yield return 1;
        //创建特效
        UInt64 skillowner= pMsg.guid;
        UInt64 skilltarget= pMsg.targuid;
        float rTime = pMsg.time / 1000.0f;
        Player target = null;
        PlayersManager.Instance.PlayerDic.TryGetValue(skilltarget, out target);
        if (0 == pMsg.state)
        {
           // HolyTech.Skill.BuffManager.Instance.AddBuff(pMsg.uniqueid, pMsg.effectid, rTime, target);
            Player entity = null;
            PlayersManager.Instance.PlayerDic.TryGetValue(skilltarget, out entity);
            EffectManager.Instance.CreateBuffEffect(entity, pMsg.effectid, pMsg.uniqueid); 
        }
        else if (1 == pMsg.state)
        {
            HolyTech.Skill.BuffManager.Instance.RemoveBuff(pMsg.uniqueid);
            HolyTech.Effect.EffectManager.Instance.DestroyEffect(pMsg.uniqueid);
        }
    }



    public int OnNotifySkillModelHitTarget(HitTar pMsg)
    {
        StartCoroutine(OnNetMsg_NotifySkillModelHitTargetCoroutine(pMsg));
        return (Int32)EErrorCode.eNormal;
    }

    public IEnumerator OnNetMsg_NotifySkillModelHitTargetCoroutine(HitTar pMsg)
    {
        //创建特效
        UInt64 ownerID = pMsg.guid;
        UInt64 targetID = pMsg.targuid;
        yield return 1;
        EffectManager.Instance.CreateBeAttackEffect(ownerID, targetID, pMsg.effectid);//创建受击特效
    }

    public int OnNotifyGameObjectReliveState(NotifyGameObjectReliveState pMsg)
    {

        UInt64 sGUID= pMsg.guid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 ford = this.ConvertDirToVector3(pMsg.dir);
        Player entity;
        if (PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity))
        {
            pos.y = entity.RealEntity.transform.position.y;
            entity.GOSSI.sServerBeginPos = pos;
            entity.GOSSI.sServerSyncPos = pos;
            entity.GOSSI.sServerDir = ford;
            entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
            entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
            //在这里可以设置血条
            entity.OnReliveState();
        }

        return (Int32)EErrorCode.eNormal;  
    }
    public int OnNotifyGameObjectDeadState(DeadState pMsg)
    {

        UInt64 deadID=pMsg.objguid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);//位置
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);//方向
        Player entity;
        if (PlayersManager.Instance.PlayerDic.TryGetValue(deadID, out entity))
        {
            pos.y = entity.RealEntity.transform.position.y;//         
            entity.EntityFSMChangeDataOnDead(pos, dir);
            entity.OnDeadState();        
        }
        return (Int32)EErrorCode.eNormal;  
    }
    public int OnNotifyHPChange(HPChange pMsg)
    {
        UInt64 sGUID= pMsg.guid;
        int crticalHp = pMsg.hp;//当前血量
        Player entity;
        Vector3 posInWorld = Vector3.zero;
        if (PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity))
        {
            posInWorld = entity.RealEntity.transform.position + new Vector3(0.0f, 2.0f, 0.0f);
            //更新实体的血条
            entity.UpdateHpChange((byte)pMsg.reason, (float)crticalHp); 
            entity.UpdateHp(entity);
        }
        return (Int32)EErrorCode.eNormal;  
    
    }
    public int OnNotifyMPChange(MpChange pMsg)
    {
        UInt64 sGUID = pMsg.guid;
        Player entity;
        if (PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity))
        {       
            entity.SetMp((float)pMsg.mp);//设置Mp值

            BloodBarPlayer BloodBarPlayer = (BloodBarPlayer)entity.BloodBar;
            //更新蓝条   
            entity.UpdateMp(entity);
        }
        return (Int32)EErrorCode.eNormal;
    }
    public int OnNotifyHPInfo(NotifyHPInfo pMsg)
    {
        foreach (GSToGC.NotifyHPInfo.HPInfo info in pMsg.hpinfo)
        {
            UInt64 sGUID = info.guid;
            Player entity;
            if (PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity))
            {
                entity.SetHp((float)info.curhp);
                entity.SetHpMax((float)info.maxhp);
                //血条更新
                entity.UpdateHp(entity);
            }
        }
        //暂时不用
        //EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyHPInfo, pMsg);
        return (Int32)EErrorCode.eNormal;  
    }
    public int OnNotifyMPInfo(NotifyMPInfo pMsg)
    {
       foreach (GSToGC.NotifyMPInfo.MPInfo info in pMsg.mpinfo)
        {
            UInt64 sGUID= info.guid;
            Player entity;
            if (PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity))
            {
                entity.SetMp((float)info.curmp);        
                entity.SetMpMax((float)info.maxmp);   

                BloodBarPlayer playerXueTiao = (BloodBarPlayer)entity.BloodBar;
                //更新实体的蓝条
                //playerXueTiao.UpdateMp();
            }
        }
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyMPInfo, pMsg);
        return (Int32)EErrorCode.eNormal;  
    }
    public int OnNotifySkillModelEmitDestroy(DestroyEmitEffect pMsg)
    {
        //从特效字典中取出特效，在创建特效时添加的
        IEffect effect = EffectManager.Instance.GetEffect(pMsg.uniqueid);
        if (effect != null)
        {
            if (effect.mType == IEffect.ESkillEffectType.eET_FlyEffect)
            {
                FlyEffect flyEffect = effect as FlyEffect;

                //拖拽类型
                if (flyEffect.emitType == 8)
                {
                    flyEffect.DragRibbonBack();
                }
                else
                    EffectManager.Instance.DestroyEffect(flyEffect);
            }
            else
            {
                EffectManager.Instance.DestroyEffect(effect);
            }
        }
        return (int)EErrorCode.eNormal;
    }
    public int OnNotifySkillModelEmit(EmitSkill pMsg)
    {    
        StartCoroutine(OnNetMsg_NotifySkillModelEmitCoroutine(pMsg));     
        return (Int32)EErrorCode.eNormal;  
    }
    public IEnumerator OnNetMsg_NotifySkillModelEmitCoroutine(EmitSkill pMsg)
    {
        UInt64 skillPlayerID = pMsg.guid;
        UInt64 skillTargetID = pMsg.targuid;
        Vector3 pos = this.ConvertPosToVector3(pMsg.tarpos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);    
        //普通追踪特效
         yield return 1;
         FlyEffect effect = EffectManager.Instance.CreateFlyEffect(skillPlayerID, skillTargetID, pMsg.effectid, (uint) pMsg.uniqueid, pos, dir, pMsg.ifAbsorbSkill);
        // EventCenter.Broadcast(GameEventEnum.UserEvent_NotifySkillModelEmit, pMsg);//暂时没用上
    
    }

    public int OnNotifyGameObjectReleaseSkillState(ReleasingSkillState pMsg )
    {
        Vector3 pos = this.ConvertPosToVector3(pMsg.pos);
        Vector3 dir = this.ConvertDirToVector3(pMsg.dir);
        dir.y = 0.0f;
        UInt64 targetID = pMsg.targuid;//目标id；
        UInt64 sGUID = pMsg.objguid;//主动方id
        Player target,entity;
        PlayersManager.Instance.PlayerDic.TryGetValue(targetID, out target);
        PlayersManager.Instance.PlayerDic.TryGetValue(sGUID, out entity);
        if (!target) return (int)EErrorCode.eNormal;
        if (entity!=null)
        {
            pos.y = entity.RealEntity.transform.position.y;  
            //数据改变 位置 方向 技能id 目标
             entity.EntityChangeDataOnPrepareSkill(pos, dir, pMsg.skillid, target);
            //释放技能 
             entity.OnEntityReleaseSkill();  
        }   
   
        return (int)EErrorCode.eNormal;
    }

    public int  OnNotifySkillInfo(NotifySkillInfo pMsg)
    {
        UInt64 targetID = pMsg.guid;
        float timeInSecond = pMsg.time / 1000.0f;   //状态持续的时间毫秒数
        float timeMax = pMsg.cooldown / 1000.0f;
        Player entity;
        PlayersManager.Instance.PlayerDic.TryGetValue(targetID, out entity);
        if (entity == null) return (int)EErrorCode.eNormal;;
        if (entity.GameObjGUID == pMsg.guid )
        {    
            entity.mIsSkillCD = true;
            entity.SetSkillCD(timeInSecond);
            
        }          

       return (int)EErrorCode.eNormal;
   }
    public int OnNotifySGameObjectFreeState(FreeState pMsg  )
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyGameObjectFreeState, pMsg);

        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyGameObjectRunState(RunningState pMsg)
    {

        if (null == pMsg.dir || null == pMsg.pos)
            return 0;

        Player entity;
        PlayersManager.Instance.PlayerDic.TryGetValue(pMsg.objguid, out entity);

        entity.GOSSI.fBeginTime = Time.realtimeSinceStartup;
        entity.GOSSI.fLastSyncSecond = Time.realtimeSinceStartup;
        
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyGameObjectRunState,pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifySkillModelStartForceMoveTeleport(NotifySkillModelStartForceMoveTeleport pMsg)
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifySkillModelStartForceMoveTeleport, pMsg);
        return (int)EErrorCode.eNormal; 
    }

    public int OnNotifyBattleHeroInfo(GSToGC.HeroInfo pMsg)
    {
        //玩家确定英雄 显示加载界面 
        //HeroCtrl.Instance.AddRealSelectHero((uint)pMsg.heroposinfo.pos, pMsg.heroposinfo.heroid);
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyEnsureHero, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyHeroInfo(GSToGC.NotifyHeroInfo pMsg)
    {
        //包含英雄模型的id  保存创建的英雄模型
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyGameObjectAppear(GSToGC.GOAppear pMsg)
    {    
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyGameObjectAppear, pMsg);  
        return (int)EErrorCode.eNormal;
    }

    public int OnBroadcastBattleHeroInfo(GSToGC.BroadcastBattleHeroInfo pMsg)
    {
        //英雄Id
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyBattleHeroInfo, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyToChooseHero(GSToGC.TryToChooseHero pMsg)
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyTryChooseHero, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyBattleBaseInfo(GSToGC.BattleBaseInfo pMsg)
    {
        GameUserModel.Instance.GameBattleID = pMsg.battleid;
        GameUserModel.Instance.GameMapID = pMsg.mapid;
        GameUserModel.Instance.IsReconnect = pMsg.ifReconnect;

        if (pMsg.ifReconnect)
        {
            GameUserModel.Instance.GameBattleID = pMsg.battleid;
            GameUserModel.Instance.GameMapID = (uint)pMsg.mapid;
            EventCenter.Broadcast(GameEventEnum.GameEvent_ReconnectToBatttle);
        }
        else
        {
            //向服务器发送消息请求战斗
            HolyGameLogic.Instance.EmsgToss_AskEnterBattle(pMsg.battleid);
        }
        return (int)EErrorCode.eNormal;
    }
    public int OnNotifyBattleSeatPosInfo(GSToGC.BattleSeatPosInfo pMsg)
    {


        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyBattleSeatPosInfo, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyBattleMatherCount(GSToGC.BattleMatcherCount pMsg)
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyBattleMatherCount, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnRequestMatchTeamList()
    {
        //请求战斗匹配
        HolyGameLogic.Instance.AskMatchBattle(1001, EBattleMatchType.EBMT_Normal);
        //申请匹配
        HolyGameLogic.Instance.AskStartTeamMatch();
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyUserBaseInfo(GSToGC.UserBaseInfo pMsg)
    {
        ulong sGUID = pMsg.guid;
        //设置游戏基本信息
        GameUserModel.Instance.SetGameBaseInfo(pMsg);
        ////请求战斗匹配
        HolyGameLogic.Instance.EmsgToss_RequestMatchTeamList();
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyMatchTeamSwitch(GSToGC.NotifyMatchTeamSwitch pMsg)
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyMatchTeamSwitch, pMsg.startflag);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyMatchTeamBaseInfo(GSToGC.NotifyMatchTeamBaseInfo pMsg)
    {
        //初始化组队伍基本信息  设置队伍地图id 及匹配模式
        //TeamMatchCtrl.Instance.InitTeamBaseInfo(pMsg.mapid, pMsg.matchtype);
        EventCenter.Broadcast<bool>(GameEventEnum.UserEvent_NotifyMatchTeamBaseInfo, pMsg.teamid!=0);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyMatchTeamPlayerInfo(GSToGC.NotifyMatchTeamPlayerInfo pMsg)
    {
        //if (pMsg.isInsert)
        //{
        //    //新增队友
        //    TeamMatchCtrl.Instance.AddTeammate(pMsg.postion, pMsg.nickname, pMsg.headid.ToString(), pMsg.userlevel);
        //}
        //else
        //{
        //    //删除队友
        //    TeamMatchCtrl.Instance.DelTeammate(pMsg.nickname);
        //}
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyServerAddr(LSToGC.ServerBSAddr pMsg)
    {
        SelectServerData.Instance.Clean();
        for (int i = 0; i < pMsg.serverinfo.Count; i++)
        {
            string addr = pMsg.serverinfo[i].ServerAddr;
            int state = pMsg.serverinfo[i].ServerState;
            string serverName = pMsg.serverinfo[i].ServerName;
            string[] sArray = serverName.Split('/');
            string name = sArray[0];
            string area = sArray[1];
            int port = pMsg.serverinfo[i].ServerPort;

            //添加到服务器列表中
            SelectServerData.Instance.SetServerList(i, name, (SelectServerData.ServerState)state, addr, port, area);
        }

        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyServerAddr);
        return (int)EErrorCode.eNormal;
    }

    //连接Gate服务器
    public int OnNotifyGateServerInfo(BSToGC.AskGateAddressRet pMsg)
    {
        SelectServerData.Instance.GateServerAdress = pMsg.ip;
        SelectServerData.Instance.GateServerPort = pMsg.port;
        SelectServerData.Instance.GateServerToken = pMsg.token;
        SelectServerData.Instance.SetGateServerUin(pMsg.user_name);
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyGateServerInfo, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnOneClinetLoginCheckRet(BSToGC.ClinetLoginCheckRet pMsg)
    {
        uint loginSuccess = pMsg.login_success;
        if (loginSuccess != 1)//fail
        {
            LoginCtrl.Instance.LoginFail(); //广播登录失败消息  显示失败界面，重新添加监听器
        }
        return (int)EErrorCode.eNormal;
    }

    //转换到选择英雄状态 显示选择英雄界面
    public int OnNotifyBattleStateChange(Stream stream)
    {
        //这个消息用于显示选择选择英雄界面 所以在显示窗口的位置注册
      //  EventCenter.Broadcast(GameEventEnum.GameEvent_IntoHero);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyHeroList(GSToGC.HeroList pMsg)
    {
        //将服务器传过来的可以选择的英雄的ID添加到英雄列表中 还没有显示出来
        foreach (int heroId in pMsg.heroid)
        {
            GameUserModel.Instance.CanChooseHeroList.Add(heroId);
        }
        GameUserModel.Instance.STCTimeDiff = pMsg.timeDiff;
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyHeroList, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public int OnNotifyBattleStateChange(BattleStateChange pMsg)
    {
        EventCenter.Broadcast(GameEventEnum.UserEvent_NotifyBattleStateChange, pMsg);
        return (int)EErrorCode.eNormal;
    }

    public Vector3 ConvertPosToVector3(Pos pos)
    {
        if (pos != null)
            return new Vector3((float)pos.x / 100.0f, HolyGameLogic.Instance.GetGlobalHeight(), (float)pos.z / 100.0f);
        else
            return Vector3.zero;
    }

    public Vector3 ConvertDirToVector3(Dir dir)
    {
        float angle = (float)(dir.angle) / 10000;
        return new Vector3((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
    }
}
