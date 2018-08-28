using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class VIPPrerogativeCtrl : Singleton<VIPPrerogativeCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_VIPPrerogativeEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_VIPPrerogativeExit);
        }
    }
}
