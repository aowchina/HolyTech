using UnityEngine;
using System.Collections;
using Common.Tools;
using GameDefine;
using System.Collections.Generic;
using HolyTech.GameData;
using System;
namespace HolyTech.GameEntity
{
    public class IChat
    {
        public UInt64 SGUID
        {
            private set;
            get;
        }

        public string NickName
        {
            private set;
            get;
        }
        public int HeadID
        {
            private set;
            get;
        }

       public class IChatInfo
        {
           public string msg;
           public bool isLocalPlayer;
           public string nickName
           {
               private set;
               get;
           }
           public int head;
            public MsgTalkEnum TalkState;
           public void SetNickName(string nick) {
               nickName = nick;
           }
        }

        //public List<string> MsgInfo = new List<string>();
        public List<IChatInfo> MsgInfo = new List<IChatInfo>();
        public MsgTalkEnum TalkState
        {
            private set;
            get;
        }
        public void SetTalkState(MsgTalkEnum talkState)
        {
            TalkState = talkState;
        }
        public void SetMsgInfo(UInt64 sGUID ,string nickName, string msgInfo, MsgTalkEnum talkState,int headID,bool islocal)
        {
            SGUID = sGUID;
            NickName = nickName;
            TalkState = talkState;
            HeadID = headID;
            IChatInfo info = new IChatInfo();
            info.isLocalPlayer = islocal;
            info.msg = msgInfo;
            info.head = headID;
            info.SetNickName(nickName);
            info.TalkState = talkState;
            MsgInfo.Add(info);
        }
    }
}
