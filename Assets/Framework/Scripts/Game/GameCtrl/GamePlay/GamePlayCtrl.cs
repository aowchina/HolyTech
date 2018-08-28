﻿using UnityEngine;
using System.Collections;
using System;
using HolyTech.GameEntity;
using HolyTech.GameState;
namespace HolyTech.Ctrl
{
    public class GamePlayCtrl : Singleton<GamePlayCtrl>
    {
        public void Enter()
        {
            //注册此消息的事件有很多，因为战斗场景要显示的界面有很多，广播此消息时有5个界面显示，
            //小地图窗口，技能窗口，装备窗口，游戏Play窗口，敌方显示窗口
            EventCenter.Broadcast(GameEventEnum.GameEvent_GamePlayEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_GamePlayExit);
        }

        public void AskBattleInfo()
        {
            HolyGameLogic.Instance.EmsgToss_AskBattleInfo();
        }

        public DateTime showaudiotimeold = System.DateTime.Now;
        private int ShopID = 0;

        internal void HpChange(GameEntity.Ientity entity)
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_EntityHpChange, entity);
        }

        internal void MpChange(GameEntity.Ientity entity)
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_EntityMpChange, entity);
        }

        internal void AskUseSkill(uint p)
        {
            HolyGameLogic.Instance.EmsgToss_AskUseSkill((UInt32)p);
        }

        internal void UpdateSkillPriv(int skillId)
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_LocalPlayerPassiveSkills, skillId);
        }

        internal void OpenShop()
        {
            if (ShopID != 0)
                PlayerManager.Instance.LocalPlayer.OpenShop(ShopID);
        }

        internal void SetShopID(int p)
        {
            ShopID = p;
        }

        public void BattleDelayTimeBegin(long time)
        {
            EventCenter.Broadcast<long>(GameEventEnum.GameEvent_BattleTimeDownEnter, time);
        }
    }
}