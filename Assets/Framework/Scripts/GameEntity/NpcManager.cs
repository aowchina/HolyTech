using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using GameDefine;
using Common.Tools;
using HolyTech.GameData;


namespace HolyTech.GameEntity
{
	public class NpcManager : EntityManager
	{
		public static new NpcManager Instance {
			private set;
			get;
		}

		public NpcManager(){
            Instance = this;
		}

        //设置公共属性
        public override void SetCommonProperty(Ientity entity, int id)
        {
            base.SetCommonProperty(entity, id);
            NpcConfigInfo info = ConfigReader.GetNpcInfo(id);
            entity.ColliderRadius = info.NpcCollideRadius / 100;
            entity.NPCCateChild = (ENPCCateChild)info.ENPCCateChild;
            entity.mCanBeLocked = info.CanLock;
        }

        //获取模型名称
		protected override string GetModeName (int id){
			if (ConfigReader.GetNpcInfo (id) == null) {
				return null;
			}
			return ConfigReader.GetNpcInfo(id).NpcName;
		}
	}
}