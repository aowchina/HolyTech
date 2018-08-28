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
    public class RuneCombineCtrl : Singleton<RuneCombineCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneCombineWindowEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RuneCombineWindowExit);
        }

        public RuneCombineCtrl()
        {
        }
    }
}
