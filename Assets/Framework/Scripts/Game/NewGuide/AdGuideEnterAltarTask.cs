﻿using UnityEngine;
namespace HolyTech.GuideDate
{
    public class AdGuideEnterAltarTask : GuideTaskBase
    {
        private AdvancedGuideInfo mGuideInfo;

        public AdGuideEnterAltarTask(int task, GuideTaskType type, GameObject mParent)
            : base(task, type, mParent)
        {

        }

        public override void EnterTask()
        {
            if (!ConfigReader.AdvancedGuideInfoDict.TryGetValue(this.mTaskId, out mGuideInfo))
            {
                return;
            }
            EventCenter.AddListener(GameEventEnum.GameEvent_PlayerEnterAltar, OnGuideTaskEvents);
            mTaskCDtime = mGuideInfo.CDTime / 1000f;
        }

        public override void ExcuseTask()
        {
            base.ExcuseTask();
        }

        public override void ClearTask()
        {
            EventCenter.RemoveListener(GameEventEnum.GameEvent_PlayerEnterAltar, OnGuideTaskEvents);
            base.ClearTask();
        }

        /// <summary>
        /// 引导任务事件触发  进入引导任务
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
