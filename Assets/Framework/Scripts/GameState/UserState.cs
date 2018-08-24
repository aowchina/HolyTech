﻿using UnityEngine;
using System.Collections;
using GameDefine;
using HolyTech.Resource;
using HolyTech.Ctrl;

namespace HolyTech.GameState
{
    class UserState : IGameState
    {
        GameStateTypeEnum stateTo;

        GameObject mScenesRoot;

        public UserState()
        {
        }

        public GameStateTypeEnum GetStateType()
        {
            return GameStateTypeEnum.GS_User;
        }

        public void SetStateTo(GameStateTypeEnum gs)
        {
            stateTo = gs;
        }

        //进入Lobby场景，在这里广播进入消息，并添加监听器。
        public void Enter()
        {
            SetStateTo(GameStateTypeEnum.GS_Continue);

            EventCenter.Broadcast(GameEventEnum.GameEvent_UserEnter);    

            ResourceItem clipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_UIBGSOUND, ResourceType.ASSET);
            AudioClip clip = clipUnit.Asset as AudioClip;

            AudioManager.Instance.PlayBgAudio(clip);

            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.AddListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
        }

        private void SdkLogOff()
        {
            GameMethod.LogOutToLogin();//清除所有账户数据

            SetStateTo(GameStateTypeEnum.GS_Login);
        }

        public void Exit()
        {
            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.RemoveListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
            EventCenter.Broadcast(GameEventEnum.GameEvent_UserExit);
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
                case GameEventEnum.GameEvent_IntoLobby:
                    GameStateManager.Instance.ChangeGameStateTo(GameStateTypeEnum.GS_Lobby);//改变游戏状态，改为Lobby状态
                    break;            
            }
        }
    }
}


