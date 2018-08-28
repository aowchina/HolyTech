using UnityEngine;
using System.Collections;


namespace HolyTech.Ctrl
{
    /// /////////////////////////// ////////////////////////////   进阶引导  /// /////////////////////////// ////////////////////////
    /// /// /////////////////////////// ////////////////////////   进阶引导  /// /////////////////////////// //////////////////////// 
    /// /// /////////////////////////// ////////////////////////   进阶引导  /// /////////////////////////// //////////////////////// 

    public class AdvancedGuideCtrl : Singleton<AdvancedGuideCtrl>
    {
        public AdvancedGuideCtrl()
        { 

        }

        public void Enter()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_AdvancedGuideEnter);
        }

        public void Exit()
        {
            EventCenter.Broadcast(GameEventEnum.GameEvent_AdvancedGuideExit);
        }
    }
}

