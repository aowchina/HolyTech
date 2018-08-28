using UnityEngine;
using System.Collections;
using HolyTech;
using Common.GameData;
using HolyTech.GameData;
using HolyTech.Network;
using LSToGC;
using System.IO;
using System.Linq;
using GameDefine;
using System;
using System.Collections.Generic;

namespace HolyTech.Ctrl
{
    public class HomePageCtrl : Singleton<HomePageCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HomePageEnter);
        }
        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HomePageExit);
        }

        //请求快速战斗
        public void AskQuickPlay(int id, EBattleMatchType type)
        {
            //请求战斗匹配
            HolyGameLogic.Instance.AskMatchBattle(id, type);
            //申请匹配
            HolyGameLogic.Instance.AskStartTeamMatch();
        }
    }
}
