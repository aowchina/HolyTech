using System;

namespace HolyTech.Ctrl
{
    public class HeroDatumCtrl : Singleton<HeroDatumCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_HeroDatumEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_HeroDatumExit);
        }
    }
}
