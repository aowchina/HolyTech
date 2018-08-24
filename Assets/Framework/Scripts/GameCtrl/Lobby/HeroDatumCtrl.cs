using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class HeroDatumCtrl : Singleton<HeroDatumCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HeroDatumEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HeroDatumExit);
        }
    }
}
