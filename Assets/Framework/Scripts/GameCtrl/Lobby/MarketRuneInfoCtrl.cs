using UnityEngine;
using System.Collections;
using HolyTech;
using Common.GameData;
using HolyTech.GameData;
using HolyTech.Network;
using LSToGC;
using System.IO;
using System.Linq;
using System;
using HolyTech.Model;

namespace HolyTech.Ctrl
{
    public class MarketRuneInfoCtrl : Singleton<MarketRuneInfoCtrl>
    {
        public void Enter(GameObject go)
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneBuyWindowEnter, go);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneBuyWindowExit);
        }

        public void BuyRune(int runeid, GameDefine.ConsumeType type, int num)
        {
            CGLCtrl_GameLogic.Instance.EMsgToGSToCSFromGC_AskBuyGoods(runeid, (int)type, num);
        }
    }
}
