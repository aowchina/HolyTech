﻿using GameDefine;
using System.Text;
using System;
using HolyTech.GuideDate;
using HolyTech.Model;
using HolyTech.GameEntity;

namespace HolyTech.Ctrl
{
    public class RoomCtrl : Singleton<RoomCtrl>
    {
        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RoomEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RoomExit);
        }

        //改变座位
        public void ChangeSeat(int i)
        {
            CGLCtrl_GameLogic.Instance.ChangeRoomSeat(i);
        }

        //开始战场
        public void BeginBattle()
        {
            CGLCtrl_GameLogic.Instance.StartRoom();
        }

        //准备
        public void ReadyRoom()
        {
            CGLCtrl_GameLogic.Instance.ReadyRoom();
        }

        //取消准备
        public void CancelReadyRoom()
        {
            CGLCtrl_GameLogic.Instance.CancelRoom();
        }

        //发送聊天信息
        public void SendTalkMessage(string msg)
        {
            string currStr = GameMethod.GetSplitStr(msg);//去掉敏感词汇
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(currStr);
           
            CGLCtrl_GameLogic.Instance.EmsgToss_AskSendRoomTalk((UInt32)utf8Bytes.Length * sizeof(byte), utf8Bytes);
        }

        //接收聊天消息
        public void RecvTalkMessage(uint index, string msg)
        {
            EventCenter.Broadcast<uint, string>(GameEventEnum.GameEvent_RecvChatMsg, index, msg);
        }

        public void UpdateRoomBaseInfo(UInt64 roomID, UInt32 mapID)
        {
            GameUserModel.Instance.GameRoomID = roomID;
            GameUserModel.Instance.GameMapID = mapID;

            if (SceneGuideTaskManager.Instance().IsNewsGuide((int)mapID) == SceneGuideTaskManager.SceneGuideType.NoGuide)
            {
                //断线重连等待过程中游戏结束退回房间特殊处理,关闭重连窗口和等待窗口.
                EventCenter.Broadcast(GameEventEnum.GameEvent_ReConnectSuccess);
                EventCenter.Broadcast(GameEventEnum.GameEvent_EndWaiting);

                EventCenter.SendEvent(new CEvent(GameEventEnum.GameEvent_IntoRoom));
            }
        }

        public void ExitInviteList()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_RoomInviteExit);
        }
        public void OpenInviteList()
        {
            if (FriendManager.Instance.RoomInvite.Count > 0)
                EventCenter.Broadcast(GameEventEnum.GameEvent_RoomInviteEnter);
        }

        public void SendInviteList(int roomID,UInt64 sGUID)
        {
            CGLCtrl_GameLogic.Instance.EmsgToss_AskInviteFriendsToBattle(roomID, sGUID);
        }

        public void AskCanInviteFriends()
        {
            FriendManager.Instance.RoomInvite.Clear();
            CGLCtrl_GameLogic.Instance.EmsgToss_AskCanInviteFriends();
        }
    }
}
