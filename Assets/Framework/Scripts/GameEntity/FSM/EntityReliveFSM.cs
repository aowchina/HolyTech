using HolyTech.GameEntity;

namespace HolyTech.FSM
{
    public class EntityReliveFSM : EntityFSM
    {
        public static readonly EntityFSM Instance = new EntityReliveFSM();
        public FsmState State
        {
            get
            {
                return FsmState.FSM_STATE_RELIVE;
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
            entity.OnEnterRelive();
        }

        public void Execute(Ientity entity)
        {
        }

        public void Exit(Ientity entity)
        {
            entity.OnExitRelive();
        }
    }
}

