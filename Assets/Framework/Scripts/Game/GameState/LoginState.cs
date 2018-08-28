using UnityEngine;
using System.Collections;
using GameDefine;
using HolyTech.Resource;
using HolyTech.Ctrl;

namespace HolyTech.GameState
{
    class LoginState : IGameState
    {
        GameStateTypeEnum stateTo;//获取下一个状态
        GameObject mScenesRoot;
		public LoginState()
		{
		}
        public GameStateTypeEnum GetStateType()
        {
            return GameStateTypeEnum.GS_Login;
        }
        //  设置状态
        public void SetStateTo(GameStateTypeEnum gs)
        {
            stateTo = gs;
        }
        //加载登录场景
        public void Enter()
        {
            //状态转换为 Continue
            SetStateTo(GameStateTypeEnum.GS_Continue);

            ////加载单个资源    Game/GameLogin  预制体  包含灯光特效 摄像机等等（属于登录界面背景
            //ResourceItem sceneRootUnit = ResourcesManager.Instance.loadImmediate(GameConstDefine.GameLogin, ResourceType.PREFAB);
            //mScenesRoot = GameObject.Instantiate(sceneRootUnit.Asset) as GameObject;

            //广播登录事件， 绑定的事件是 Show方法 创建窗体并初始化，激活创建的UI 
            //LoginCtrl逻辑控制 广播登录事件 显示登录窗口
            LoginCtrl.Instance.Enter();

            //Audio/EnvironAudio/mus_fb_login_lp  音频 资源
            ResourceItem audioClipUnit = ResourcesManager.Instance.loadImmediate(AudioDefine.PATH_UIBGSOUND, ResourceType.ASSET);
            AudioClip clip = audioClipUnit.Asset as AudioClip;                                  
            AudioManager.Instance.PlayBgAudio(clip);


            //添加监听器   转换状态到UserState
            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_InputUserData, OnEvent);
            //添加监听器   转换状态到LobbyState
            EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_IntoLobby, OnEvent);
            //添加监听器   重新加载当前的State，
            //此时由于不是初始化启动的时候进入本状态，重新加载登录场景
            EventCenter.AddListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);
            
        }

        private void SdkLogOff()
        {
            GameMethod.LogOutToLogin();//退出登录，清空数据
            SetStateTo(GameStateTypeEnum.GS_Login);//状态转换到GS_Login状态
        }

        //每种状态都有退出状态，一旦转换状态，都会执行退出的状态
        public void Exit()
        {
            //移除监听器
            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_InputUserData, OnEvent);
            EventCenter.RemoveListener<CEvent>(GameEventEnum.GameEvent_IntoLobby, OnEvent);
            EventCenter.RemoveListener(GameEventEnum.GameEvent_SdkLogOff, SdkLogOff);       

            //LoadUiResource.DestroyLoad(mUIRoot);
            LoginCtrl.Instance.Exit();//广播退出登录事件
            GameObject.DestroyImmediate(mScenesRoot);            
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
            UIPlayMovie.PlayMovie("cg.mp4", Color.black, 2/* FullScreenMovieControlMode.Hidden*/, 3/*FullScreenMovieScalingMode.Fill*/);

            switch (evt.GetEventId())
            {
                case GameEventEnum.GameEvent_InputUserData:     
                    SetStateTo(GameStateTypeEnum.GS_User);
                    break;
                case GameEventEnum.GameEvent_IntoLobby:
                    GameStateManager.Instance.ChangeGameStateTo(GameStateTypeEnum.GS_Lobby);
                    break;
            }
        }
    }
}


