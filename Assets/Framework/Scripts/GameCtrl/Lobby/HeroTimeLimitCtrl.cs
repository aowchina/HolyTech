using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class HeroTimeLimitCtrl : Singleton<HeroTimeLimitCtrl>
    {

        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HeroTimeLimitEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_HeroTimeLimitExit);
        }
    }
}