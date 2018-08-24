using HolyTech.GameEntity;

namespace HolyTech.FSM
{
    public class PlayerAdMoveFSM : EntityFSM
    {
        public static readonly EntityFSM Instance = new PlayerAdMoveFSM();

        public FsmState State
        {
            get
            {
                return FsmState.FSM_STATE_ADMOVE;
            }
        }

        public bool CanNotStateChange
        {
            set;
            get;
        }

        public bool StateChange(Ientity entity, EntityFSM fsm)
        {
            return CanNotStateChange;
        }

        public void Enter(Ientity entity, float last)
        {
            entity.OnEnterEntityAdMove();//无
        }

        public void Execute(Ientity entity)
        {
            entity.OnExecuteEntityAdMove(); //ientity中空  在Iselfplayer
        }

        public void Exit(Ientity entity)
        {

        }
    }
}

