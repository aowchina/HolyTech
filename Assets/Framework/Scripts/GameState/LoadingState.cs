using UnityEngine;
using System.Collections;
using GameDefine;
using HolyTech.GameData;
using System.Threading;
using HolyTech.Resource;
using HolyTech.View;
using HolyTech.Ctrl;
using HolyTech.Model;

namespace HolyTech.GameState
{
    class LoadingState : IGameState
    {
        GameStateTypeEnum stateTo;

        GameObject mScenesRoot;

        GameObject mUIRoot;

        public GameStateTypeEnum mNextState;

        public EScenesType mFrontScenes;

        public LoadingState()
        {
            mNextState = GameStateTypeEnum.GS_Continue;
        }

        public GameStateTypeEnum GetStateType()
        {
            return GameStateTypeEnum.GS_Loading;
        }

        public void SetNextState(GameStateTypeEnum next)
        {
            mNextState = next;
        }

        public void SetFrontScenes(EScenesType front)
        {
            mFrontScenes = front;
        }

        public void SetStateTo(GameStateTypeEnum gs)
        {
            stateTo = gs;
        }

        private string GetLoadMapName()
        {
            MapInfo map = MapLoadConfig.Instance.GetMapInfo(GameUserModel.Instance.GameMapID);

            if (map == null)
            {
                Debug.LogError("GetLoadMapName map find fail !!!");
                return "";
            }

            return map.mLoadScene;
        }

        private void LoadFinish(GameStateTypeEnum state)
        {    
            SetStateTo(state);
        }

        public void Enter()
        {
            if (mNextState == GameStateTypeEnum.GS_Continue)
                return;

            SetStateTo(GameStateTypeEnum.GS_Continue);

            //加载UI
            mUIRoot = GameMethod.LoadProgress();
            LoadScene.Instance.GState = mNextState;  //狀態賦值
            LoadScene.Instance.OnLoadFinish += LoadFinish;//委託賦值   LoadFinish 封裝了設置狀態函數

            //加载场景之前需要进行清除操作
            HolyTech.Effect.EffectManager.Instance.DestroyAllEffect();
            //清除GameObjectPool数据
            GameObjectPool.Instance.Clear();

            //加载场景  如果下一个状态是战斗状态  加载战斗场景
            if (mNextState == HolyTech.GameState.GameStateTypeEnum.GS_Play)
            {
                LoadScene.Instance.isCloseBySelf = false;
                string name = GetLoadMapName();
                LoadScene.Instance.LoadAsignedSene("Scenes/"+name);
                //Play UI窗口加载
                WindowManager.Instance.ChangeScenseToPlay(mFrontScenes);
            }
            else
            {
                //返回Pvp_Login选人界面需要清除预加载信息                
                ReadPreLoadConfig.Instance.Clear();
                LoadScene.Instance.isCloseBySelf = true;
                LoadScene.Instance.LoadAsignedSene("Scenes/Pvp_Login");//加载指定场景
                //Login UI窗口加载
                WindowManager.Instance.ChangeScenseToMain(mFrontScenes);

            }
            //音频播放
            ResourceItem clipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_UIBGSOUND, ResourceType.ASSET);
            AudioClip clip = clipUnit.Asset as AudioClip;
            AudioManager.Instance.PlayBgAudio(clip);

            //事件添加
            EventCenter.AddListener(GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);
            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_IntoRoom, OnEvent);

        }

        public void Exit()
        {
            EventCenter.RemoveListener(GameEventEnum.GameEvent_ConnectServerFail, OnConnectServerFail);
            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_IntoRoom, OnEvent);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {

        }

        public GameStateTypeEnum Update(float fDeltaTime)
        {
            return stateTo;
        }

        public void OnConnectServerFail()
        {
            EventCenter.Broadcast<EMessageType>(GameEventEnum.GameEvent_ShowMessage, EMessageType.EMT_Reconnect);
        }

        public void OnEvent(CEvent evt)
        {
            switch (evt.GetEventId())
            {
                case GameEventEnum.GameEvent_IntoRoom:
                    {
                        //获取当前状态
                        LoadingState lState = GameStateManager.Instance.getState(GameStateTypeEnum.GS_Loading) as LoadingState;
                        lState.SetNextState(GameStateTypeEnum.GS_Room);
                        lState.SetFrontScenes(View.EScenesType.EST_Play);
                        SetStateTo(GameStateTypeEnum.GS_Loading);
                    }
                    break;
            }
        }
    }
}


