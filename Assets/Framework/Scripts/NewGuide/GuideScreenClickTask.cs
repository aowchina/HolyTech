using System;
using System.Collections.Generic;
using UnityEngine;
using GameDefine;
using HolyTech.GameEntity;
using Common.Tools;
namespace HolyTech.GuideDate
{
    public class GuideScreenClickTask : GuideTaskBase
    {
      
        public GuideScreenClickTask(int task, GuideTaskType type, GameObject mParent)
            : base(task, type, mParent)
        {
           
        }

        /// <summary>
        /// 到时广播消息
        /// </summary>
        public override void EnterTask()
        {
            EventCenter.AddListener<Ientity>(GameEventEnum.GameEvent_LockTarget, OnLockTarget);
        }


        public override void ExcuseTask()
        {

        }

        private void OnLockTarget(Ientity mEntity)
        {
            this.FinishTask();
        }

        public override void ClearTask()
        {
            EventCenter.RemoveListener<Ientity>(GameEventEnum.GameEvent_LockTarget, OnLockTarget);
           
        }
 
    }


}
