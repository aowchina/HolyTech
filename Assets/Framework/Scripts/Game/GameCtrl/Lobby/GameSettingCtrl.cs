using UnityEngine;
using System.Collections;
namespace HolyTech.Ctrl
{
    public class GameSettingCtrl : Singleton<GameSettingCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_GameSettingEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_GameSettingExit);
        }
    }
}