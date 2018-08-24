using UnityEngine;
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
    public class MarketCtrl : Singleton<MarketCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketExit);
        }

    }
}
