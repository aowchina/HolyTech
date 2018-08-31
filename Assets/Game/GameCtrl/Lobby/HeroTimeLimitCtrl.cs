using System;

namespace HolyTech.Ctrl
{
    public class HeroTimeLimitCtrl : Singleton<HeroTimeLimitCtrl>
    {

        public void Enter()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_HeroTimeLimitEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_HeroTimeLimitExit);
        }
    }
}