using System;
using UnityEngine;
using GameDefine;
using HolyTech.Resource;
using HolyTech.Ctrl;
using HolyTech.View;
using HolyTech.GameEntity;
using HolyTech.Model;

namespace HolyTech.GameState
{
    class OverState : IGameState
    {
        GameStateTypeEnum stateTo;

        GameObject mScenesRoot;

        GameObject mUIRoot;

        float mTime;
        bool mNeedUpdate;
        bool mNeedScore;

        public GameStateTypeEnum GetStateType()
        {
            return GameStateTypeEnum.GS_Over;
        }

        public void SetStateTo(GameStateTypeEnum gs)
        {
            stateTo = gs;
        }

        public void Enter()
        {
            SetStateTo(GameStateTypeEnum.GS_Continue);

            mTime = 0;

            mNeedUpdate = true;
            mNeedScore = true;

            ResourceItem clipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.GetMapBgAudio((MAPTYPE)GameUserModel.Instance.GameMapID), ResourceType.ASSET);
            AudioClip clip = clipUnit.Asset as AudioClip;

            AudioManager.Instance.PlayBgAudio(clip);

            AdvancedGuideCtrl.Instance.Exit();

            EventCenter.AddListener<FEvent>((Int32)GameEventEnum.GameEvent_Loading, OnEvent);
            EventCenter.AddListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoRoom, OnEvent);
            EventCenter.AddListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.AddListener((Int32)GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);

        }

        public void Exit()
        {
            EventCenter.RemoveListener<FEvent>((Int32)GameEventEnum.GameEvent_Loading, OnEvent);
            EventCenter.RemoveListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoRoom, OnEvent);
            EventCenter.RemoveListener<FEvent>((Int32)GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.RemoveListener((Int32)GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);

            //GameMethod.DisableAllUI();

            GameMethod.RemoveUI("superwan(Clone)");
            PlayerManager.Instance.LocalPlayer.CleanWhenGameOver();
            HolyTechGameBase.Instance.OpenConnectUI();
            ObjectPool.Instance.Clear();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public GameStateTypeEnum Update(float fDeltaTime)
        {
            if (!mNeedUpdate)
                return stateTo;

            mTime += fDeltaTime;
            if (UIGuideModel.Instance.GuideType == GCToCS.AskCSCreateGuideBattle.guidetype.first)
            {
                if (mTime > 6)
                {
                    HolyGameLogic.Instance.EmsgToss_AskReEnterRoom();
                    mNeedUpdate = false;
                    UIGuideCtrl.Instance.GuideBattleType(GCToCS.AskCSCreateGuideBattle.guidetype.other);
                }
            }
            else
            {
                if (mTime > 18)
                {
                    ScoreCtrl.Instance.Exit();
                    HolyGameLogic.Instance.EmsgToss_AskReEnterRoom();
                    mNeedUpdate = false;
                }
                else if (mNeedScore && mTime > 6)
                {
                    ScoreCtrl.Instance.Enter();
                    mNeedScore = false;
                }
            }
            return stateTo;
        }

        public void OnEvent(FEvent evt)
        {
            switch ((GameEventEnum)evt.GetEventId())
            {
                case GameEventEnum.GameEvent_Loading:
                    {
                        GameStateTypeEnum stateType = (GameStateTypeEnum)evt.GetParam("NextState");
                        LoadingState lState = GameStateManager.Instance.getState(GameStateTypeEnum.GS_Loading) as LoadingState;
                        lState.SetNextState(stateType);
                        lState.SetFrontScenes(View.EScenesType.EST_Play);
                        SetStateTo(GameStateTypeEnum.GS_Loading);
                    }
                    break;
                case GameEventEnum.GameEvent_IntoRoom:
                    {
                        LoadingState lState = GameStateManager.Instance.getState(GameStateTypeEnum.GS_Loading) as LoadingState;
                        lState.SetNextState(GameStateTypeEnum.GS_Room);
                        lState.SetFrontScenes(View.EScenesType.EST_Play);
                        SetStateTo(GameStateTypeEnum.GS_Loading);
                    }
                    break;
                case GameEventEnum.GameEvent_IntoLobby:
                    {
                        LoadingState lState = GameStateManager.Instance.getState(GameStateTypeEnum.GS_Loading) as LoadingState;
                        lState.SetNextState(GameStateTypeEnum.GS_Lobby);
                        lState.SetFrontScenes(View.EScenesType.EST_Play);
                        SetStateTo(GameStateTypeEnum.GS_Loading);
                    }
                    break;
            }
        }

        private void OnConnectServerFail()
        {
            EventCenter.Broadcast<EMessageType>((Int32)GameEventEnum.GameEvent_ShowMessage, EMessageType.EMT_Disconnect);
        }
    }
}


