using UnityEngine;
using System.Collections;
namespace HolyTech.Ctrl
{
    public class ExtraBonusCtrl : Singleton<ExtraBonusCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_ExtraBonusEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_ExtraBonusExit);
        }

        internal void SendMsg(string mTemp)
        {
            HolyGameLogic.Instance.EmsgTocs_CDKReq(mTemp);
        }
    }
}