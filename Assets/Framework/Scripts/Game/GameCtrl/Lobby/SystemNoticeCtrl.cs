using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class SystemNoticeCtrl : Singleton<SystemNoticeCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_SystemNoticeEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_SystemNoticeExit);
        }
    }
}