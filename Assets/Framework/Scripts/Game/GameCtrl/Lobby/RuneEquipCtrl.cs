using UnityEngine;
using System.Collections.Generic;
using HolyTech;
using Common.GameData;
using HolyTech.GameData;
using HolyTech.Network;
using LSToGC;
using System.IO;
using System.Linq;

using HolyTech.Model;

namespace HolyTech.Ctrl
{
    public class RuneEquipCtrl : Singleton<RuneEquipCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneEquipWindowEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneEquipWindowExit);
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
