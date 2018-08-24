using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using GameDefine;
using Common.Tools;
using HolyTech.GameData;
using HolyTech.Model;
using GameDefine;
using System;

namespace HolyTech.GameEntity
{
	public class PlayerManager : EntityManager
	{
		public static new PlayerManager Instance {
			private set;
			get;
		}
   
        public Dictionary<UInt64, Iplayer> AccountDic = new Dictionary<UInt64, Iplayer>();//UInt64 masterId

		public PlayerManager(){
			Instance = this;
		}
         

		public Iselfplayer LocalPlayer { set; get; }		 

		public Iplayer LocalAccount{ set; get; }
        public SITUATION StateSituationOb{ set; get; } //战况  平手，失败，胜利
        
        
        //创建实例（英雄）  英雄ID   英雄阵营
        public override Ientity HandleCreateEntity (UInt64 sGUID , EntityCampType campType){
            //entity id		
            Iplayer player;
            if (GameUserModel.Instance.IsLocalPlayer(sGUID))
            {
                player = new Iselfplayer(sGUID, campType);                
            }
            else
            {
                player =  new Iplayer(sGUID, campType);
            }

            player.GameUserId = sGUID;
            return player;
		}

        //将Player添加到AccountDic中 
        public void AddAccount(UInt64 sGUID, Iplayer entity)
		{
			if (AccountDic.ContainsKey (sGUID)) {
				Debug.LogError("Has the same Guid: " + sGUID)	;		
				return;
			}
			AccountDic.Add (sGUID , entity);
		}

        //设置公共属性   模型名称  id  碰撞器大小  昵称
        public override void SetCommonProperty(Ientity entity, int id)
        {
            base.SetCommonProperty(entity, id);
            //获取英雄配置文件
            HeroConfigInfo info = ConfigReader.GetHeroInfo(id);
            entity.ColliderRadius = info.HeroCollideRadious / 100;
            Iplayer mpl = (Iplayer)entity;
            if (mpl.GameUserNick == "" || mpl.GameUserNick == null)
            {
                //随机昵称
                mpl.GameUserNick = RandomNameData.Instance.GetRandName();
            }
        }

        //获取模型名称
		protected override string GetModeName (int id)
		{
			return ConfigReader.GetHeroInfo(id).HeroName;
		} 

		public bool IsLocalSameType(Ientity entity){
			if(PlayerManager.Instance.LocalPlayer.EntityCamp != entity.EntityCamp)
				return false;
			return true;
		}

        //清除AccountDic
		public void CleanAccount(){
			for (int i = AccountDic.Count - 1; i >= 0; i--) {
				if (GameUserModel.Instance.IsLocalPlayer (AccountDic.ElementAt(i).Value.GameObjGUID))
					continue;	
				AccountDic.Remove (AccountDic.ElementAt(i).Key);
			}					 
		}

		public void RemoveAccountBySeat(uint seat){
			for (int i = AccountDic.Count - 1; i >= 0; i--) {
				if (AccountDic.ElementAt(i).Value.GameUserSeat != seat)
					continue;	
				AccountDic.Remove (AccountDic.ElementAt(i).Key);
				break;
			}					 
		}

        //游戏结束时清除 AccountDic
        public void CleanPlayerWhenGameOver() {
            foreach (var item in AccountDic.Values) 
            { 
                item.CleanWhenGameOver();
            }
        }
        
	}
}