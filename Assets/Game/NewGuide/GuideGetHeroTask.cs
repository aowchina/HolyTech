using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameDefine;
using HolyTech.Model;
using HolyTech.Ctrl;

namespace HolyTech.GuideDate
{
    public class GuideGetHeroTask : GuideTaskBase
    {
        public GuideGetHeroTask(int task, GuideTaskType type, GameObject mParent)
            : base(task, type, mParent)
        {

        }

        public override void EnterTask()
        {
            EventCenter.AddListener<int>((Int32)GameEventEnum.GameEvent_CsGetNewHero, GetHeroEvent);
        }

        public override void ExcuseTask()
        {

        }

        public override void ClearTask()
        {
            EventCenter.RemoveListener<int>((Int32)GameEventEnum.GameEvent_CsGetNewHero, GetHeroEvent);
            base.ClearTask();
        }

        private void GetHeroEvent(int mType)
        {
            CommodityInfos cmd;
            if (GameUserModel.Instance.OwnedHeroInfoDict.TryGetValue(mType, out cmd))
            {
                if (!cmd.If_free)
                {
                    this.FinishTask();
                }
            }
            
        }

    }


}
