using System;
using HolyTech.Model;

namespace HolyTech.Ctrl
{
    public class RuneEquipCtrl : Singleton<RuneEquipCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_RuneEquipWindowEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_RuneEquipWindowExit);
        }

        public RuneEquipCtrl()
        {
        }

        public void UnloadRune(int page, int pos)
        {
            RuneEquipModel.Instance.RemoveRune(page, pos);
        }
    }
}
