using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameDefine;

using HolyTech.Ctrl;
using HolyTech.GameEntity;

namespace HolyTech.GuideDate
{
    public class AdGuidePlayerRebornTask : GuideTaskBase
    {
        private AdvancedGuideInfo mGuideInfo;

        public AdGuidePlayerRebornTask(int task, GuideTaskType type, GameObject mParent)
            : base(task, type, mParent)
        {

        }

        public override void EnterTask()
        {
            if (!ConfigReader.AdvancedGuideInfoDict.TryGetValue(this.mTaskId, out mGuideInfo))
            {
                return;
            }
            EventCenter.AddListener(GameEventEnum.GameEvent_Reborn, OnGuideTaskEvents);
            mTaskCDtime = mGuideInfo.CDTime / 1000f;
        }

        public override void ExcuseTask()
        {
            base.ExcuseTask();
        }

        public override void ClearTask()
        {
            EventCenter.RemoveListener(GameEventEnum.GameEvent_Reborn, OnGuideTaskEvents);
            base.ClearTask();
        }

        /// <summary>
        /// 引导任务事件触发
        /// </summary>
        private void OnGuideTaskEvents()
        {
            if (mIsTaskCoolDown)
            {
                return;
            }
            mIsTaskCoolDown = true;
            mTaskTime = Time.realtimeSinceStartup;
            EventCenter.Broadcast<int>(GameEventEnum.GameEvent_AdvancedGuideShowTip, this.mTaskId);
        }

    }


}
