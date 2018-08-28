using UnityEngine;
using System.Collections.Generic;
using HolyTech;
using Common.GameData;
using HolyTech.GameData;
using System.IO;
using System.Linq;

using HolyTech.Model;

namespace HolyTech.Ctrl
{
    public class MarketRuneListCtrl : Singleton<MarketRuneListCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketRuneListEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_MarketRuneListExit);
        }

        public enum ConsumeType
        {
            TypeGold = 1,
            TypeDiamond,
        }


        public MarketRuneListCtrl()
        {
        }

        public void SetSelectGoods(int runeId) {
            mGoodsSelect = runeId;
        }

        public int GetGoodsSelect() {
            return mGoodsSelect;
        }

        public void MarketHeroAskBuyRunes(int goodsId , ConsumeType tp)
        {
            HolyGameLogic.Instance.EMsgToGSToCSFromGC_AskBuyGoods(goodsId, (int)tp);
        }

        public void UpdateRuneBagInfo(uint runeID, int num, long gottime)
        {
            MarketRuneListModel.Instance.UpdateBuyedRuneInfo(runeID, num, gottime);
        }
        private int mGoodsSelect;
    }
}
