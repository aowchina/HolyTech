using HolyTech.GameEntity;

namespace HolyTech.FSM
{
	public class EntityRunFSM : EntityFSM
	{
		public static readonly EntityFSM Instance = new EntityRunFSM();
		
		public FsmState State{
			get
			{
				return FsmState.FSM_STATE_RUN;
			}
		}
		
		public bool CanNotStateChange{
			set;get;
		}
		
		public bool StateChange(Ientity entity , EntityFSM fsm){
			return CanNotStateChange;
		}
		
		public void Enter(Ientity entity , float last){
            FSMHelper.ExecuteDeviation(entity);
            entity.OnEnterMove();
		}
		
		public void Execute(Ientity entity){      
                entity.OnExecuteMove();
		}
		
		public void Exit(Ientity entity){
			
		}
	}
}

