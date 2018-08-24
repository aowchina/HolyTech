namespace HolyTech.FSM
{
    //英雄状态
	public enum FsmState 
	{
		FSM_STATE_FREE,//自由 0
		FSM_STATE_RUN,//跑 1
        FSM_STATE_SING,//唱歌 2
		FSM_STATE_RELEASE,//释放3
        FSM_STATE_LEADING,//离开 4
        FSM_STATE_LASTING,//
		FSM_STATE_DEAD,//死亡 6
        FSM_STATE_ADMOVE,//
        FSM_STATE_FORCEMOVE,//被迫移动  8
        FSM_STATE_RELIVE,//重生  9
        FSM_STATE_IDLE,//默认状态  10
	}
}