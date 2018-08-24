using UnityEngine;
using System.Collections;

namespace HolyTech.Ctrl
{
    public class InviteRoomCtrl : Singleton<InviteRoomCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_InviteRoomEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_InviteRoomExit);
        }

        public void AcceptAddFriend(string roomID, string password)
        {
            CGLCtrl_GameLogic.Instance.AddRoom(roomID, password);
        }

    }
}

