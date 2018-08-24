using UnityEngine;
using System.Collections;
using System;

namespace HolyTech.Ctrl
{
    public class ChatTaskCtrl : Singleton<ChatTaskCtrl>
    {
        public bool isOpen = false;
        public void Enter(UInt64 sGUID)
        {
            if (!isOpen){
                isOpen = true;
                EventCenter.Broadcast(GameEventEnum.GameEvent_ChatTaskEnter, sGUID);
            }
            else
            {
                EventCenter.Broadcast(GameEventEnum.GameEvent_ReadyChatTaskEnter, sGUID);
            }

        }

        public void Exit()
        {
            isOpen = false;
            EventCenter.Broadcast(GameEventEnum.GameEvent_ChatTaskExit);
        }
        public void SendMsg(UInt64 sGUID, uint length, byte[] talkMsg)
        {
            CGLCtrl_GameLogic.Instance.EmsgTocs_AskSendMsgToUser(sGUID, length, talkMsg);
        }
        public void ShowChatTask()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_ShowChatTaskFriend);
        }
        public void SetNewChat()
        {
            EventCenter.Broadcast<bool>(GameEventEnum.GameEvent_ReceiveLobbyMsg,false);
        }

        internal void SetDestoy(UInt64 sGUID)
        {
            EventCenter.Broadcast<UInt64>(GameEventEnum.GameEvent_RemoveChatTask, sGUID);
        }
    }
}
