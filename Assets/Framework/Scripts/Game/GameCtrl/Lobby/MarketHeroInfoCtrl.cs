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
    public class MarketHeroInfoCtrl : Singleton<MarketHeroInfoCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketHeroInfoEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketHeroInfoExit);
        }

        public class HeroGoodsInfo
        {
            public enum CostType
            {
                GoldType = 0,
                DiamondType,
            }
           public int mGoodType;
           public int mGoodId;
           public CostType mCostType;
           public int mCost;
           public bool mIsDiscount;
        }
    }
}
