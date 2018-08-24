using HolyTech.GameEntity;

namespace HolyTech.FSM
{
	public class EntityReleaseSkillFSM : EntityFSM
	{
		public static readonly EntityFSM Instance = new EntityReleaseSkillFSM();
		public FsmState State{
			get
			{
				return FsmState.FSM_STATE_RELEASE;
			}
		}
		public bool CanNotStateChange{
			set;get;
		}
		
		public bool StateChange(Ientity entity , EntityFSM fsm){
			return CanNotStateChange;
		}
		
		public void Enter(Ientity entity , float last){
			
			entity.OnEntityReleaseSkill();
		}
		
		public void Execute(Ientity entity){
            //entity.OnEntityPrepareAttack ();
		}

		public void Exit(Ientity entity){
		    
		}
	}
}

