using HolyTech.GameEntity;
using UnityEngine;

namespace HolyTech.FSM
{
    public class EntityDeadFSM : EntityFSM
    {
        public static readonly EntityFSM Instance = new EntityDeadFSM();

        public EntityDeadFSM()
        {
        }

        public FsmState State
        {
            get
            {
                return FsmState.FSM_STATE_DEAD;
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
            entity.OnEnterDead();
        }

        public void Execute(Ientity entity)
        {
            entity.OnExecuteDead();
        }

        public void Exit(Ientity entity)
        {
            if (entity.FSM != null && entity.FSM.State == FsmState.FSM_STATE_DEAD)
            {
                entity.objTransform.position = entity.EntityFSMPosition;
                entity.objTransform.rotation = Quaternion.LookRotation(entity.EntityFSMDirection);
                entity.OnReborn();
            }
        }
    }
}

