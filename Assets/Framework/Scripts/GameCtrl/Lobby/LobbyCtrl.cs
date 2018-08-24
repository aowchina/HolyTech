﻿using UnityEngine;
using System.Collections;
using HolyTech;
using Common.GameData;
using HolyTech.GameData;
using HolyTech.Network;
using LSToGC;
using System.IO;
using System.Linq;

namespace HolyTech.Ctrl
{
    public class LobbyCtrl : Singleton<LobbyCtrl>
    {
        public void Enter()
        {
            //广播进入游戏大厅  显示UI
            EventCenter.Broadcast(GameEventEnum.GameEvent_LobbyEnter);
            //向服务器请求队伍信息 返回的消息分别为
            /* notifyGoodsBuy
             * notifyUserCLDays
             * notifyCSHeroList
             * notifyRunesList
             * notifyOtherItemInfo
             * notifyUserSNSList
             */
            CGLCtrl_GameLogic.Instance.EmsgToss_RequestMatchTeamList();
            //初始化引导的信息 小谷注释 此方法空
            //InitLobbyGuideUIInfo();
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_LobbyExit);
        }

        public void AskNewGMCmd(string cmd)
        {
            CGLCtrl_GameLogic.Instance.EmsgTocs_AddNewGMCmd(cmd);
        }

        /// <summary>
        /// 进入大厅的时候初始化引导的信息
        /// </summary>
        private void InitLobbyGuideUIInfo()
        {
            UIGuideCtrl.Instance.InitLobbyGuideInfo();
        }

        internal void AskPersonInfo()
        {
            CGLCtrl_GameLogic.Instance.PersonInfo();
        }


        internal void InviteInfo(ulong sGUID, string nickName)
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_InviteCreate, sGUID, nickName);
        }
    }
}
