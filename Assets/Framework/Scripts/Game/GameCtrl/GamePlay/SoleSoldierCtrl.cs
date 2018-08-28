using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class SoleSoldierCtrl : Singleton<SoleSoldierCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_SoleSoldierEnter);
        }
        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_SoleSoldierExit);
        }
    }
}
