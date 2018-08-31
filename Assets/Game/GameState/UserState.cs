using System;
using UnityEngine;
using GameDefine;
using HolyTech.Resource;

namespace HolyTech.GameState
{
    class UserState : IGameState
    {
        GameStateTypeEnum stateTo;

        GameObject mScenesRoot;

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

            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_UserEnter);    

            ResourceItem clipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_UIBGSOUND, ResourceType.ASSET);
            AudioClip clip = clipUnit.Asset as AudioClip;

            AudioManager.Instance.PlayBgAudio(clip);

            EventCenter.AddListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.AddListener((Int32)GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
        }

        private void SdkLogOff()
        {
            GameMethod.LogOutToLogin();//清除所有账户数据

            SetStateTo(GameStateTypeEnum.GS_Login);
        }

        public void Exit()
        {
            EventCenter.RemoveListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.RemoveListener((Int32)GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
            EventCenter.Broadcast((Int32)GameEventEnum.GameEvent_UserExit);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {

        }
        public GameStateTypeEnum Update(float fDeltaTime)
        {
            return stateTo;
        }

        public void OnEvent(FEvent evt)
        {
            switch ((GameEventEnum)evt.GetEventId())
            {
                case GameEventEnum.GameEvent_IntoLobby:
                    GameStateManager.Instance.ChangeGameStateTo(GameStateTypeEnum.GS_Lobby);//改变游戏状态，改为Lobby状态
                    break;            
            }
        }
    }
}


