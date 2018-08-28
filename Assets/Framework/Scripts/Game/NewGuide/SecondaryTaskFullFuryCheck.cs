using System;
using System.Collections.Generic;
using UnityEngine; 
using System.Linq;
using GameDefine;
namespace HolyTech.GuideDate
{
    public class SecondaryTaskFullFuryCheck : SecondaryTaskCheckBase
    {
        EFuryState curState = EFuryState.eFuryNullState;
        
        
        public override void OnEnter(SecondaryTaskInfo parent)
        {
            base.OnEnter(parent);
            AddCheckListener();
        } 

        void OnEvent(CEvent eve)
        {
            EFuryState state = (EFuryState)eve.GetParam("State");
            GuideHelpData data = ConfigReader.GetGuideHelpInfo(parentInfo.GetTaskId()); 
            if (state == EFuryState.eFuryFullState && curState != EFuryState.eFuryFullState)
            {              
                SecondaryGuideManager.Instance.SendTaskStartTag(data);
            }
            else
            { 
                SecondaryGuideManager.Instance.SendTaskEndTag(data); 
            }
            curState = state;
        }

        public override void AddCheckListener()
        {            
            //EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_NotifySelfPlayerFuryStateChange, OnEvent);             
        }

        public override void RemoveAddListener()
        {
            //if (EventCenter.mEventTable.ContainsKey(GameEventEnum.GameEvent_NotifySelfPlayerFuryStateChange))
            //{
            //    EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_NotifySelfPlayerFuryStateChange, OnEvent);
            //}
        }

        public override void OnEnd()
        { 
            base.OnEnd();
            RemoveAddListener();
        }

    }
}
