using UnityEngine;
using System.Collections;
using GameDefine;
using HolyTech.GameData;
using HolyTech.Resource;
using HolyTech.Ctrl;
using System;
using HolyTech.View;

namespace HolyTech.GameState
{
    class RoomState : IGameState
    {
        GameStateTypeEnum stateTo;

        GameObject mScenesRoot;

        GameObject mUIRoot;

        public RoomState()
        {
        }

        public GameStateTypeEnum GetStateType()
        {
            return GameStateTypeEnum.GS_Room;
        }

        public void SetStateTo(GameStateTypeEnum gs)
        {
            stateTo = gs;
        }

        public void Enter()
        {
            SetStateTo(GameStateTypeEnum.GS_Continue);

            RoomCtrl.Instance.Enter();

            ResourceItem clipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_UIBGSOUND, ResourceType.ASSET);
            AudioClip clip = clipUnit.Asset as AudioClip;

            AudioManager.Instance.PlayBgAudio(clip);
            
            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_RoomBack, OnEvent);
            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_IntoHero, OnEvent);
            EventCenter.AddListener<UInt64,string>(GameEventEnum.GameEvent_InviteCreate, InviteAddFriend);
            EventCenter.AddListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
            EventCenter.AddListener(GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);

        }

        private void InviteAddFriend(UInt64 sGUID,string temp)
        {
            if (InviteCtrl.Instance.AddDic(sGUID, temp) && InviteCtrl.Instance.InvatiDic.Count > 1)
            {
                InviteCtrl.Instance.ChangeInvite(sGUID, temp);
            }
            else
                InviteCtrl.Instance.Enter(sGUID, temp);
        }

        public void Exit()
        {

            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_RoomBack, OnEvent);
            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_IntoHero, OnEvent);
            EventCenter.RemoveListener<UInt64, string>(GameEventEnum.GameEvent_InviteCreate, InviteAddFriend);
            EventCenter.RemoveListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
            EventCenter.RemoveListener(GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);
            RoomCtrl.Instance.Exit();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {

        }

        public GameStateTypeEnum Update(float fDeltaTime)
        {
            return stateTo;
        }

        public void OnEvent(CEvent evt)
        {
            switch (evt.GetEventId())
            {
                case GameEventEnum.GameEvent_RoomBack:
                    SetStateTo(GameStateTypeEnum.GS_Lobby);
                    break;
                case GameEventEnum.GameEvent_IntoHero:
                    SetStateTo(GameStateTypeEnum.GS_Hero);
                    break;
            }
        }

        private void SdkLogOff()
        {
            GameMethod.LogOutToLogin();

            SetStateTo(GameStateTypeEnum.GS_Login);
        }

        private void OnConnectServerFail()
        {
            EventCenter.Broadcast<EMessageType>(GameEventEnum.GameEvent_ShowMessage, EMessageType.EMT_Disconnect);
        }
    }
}


