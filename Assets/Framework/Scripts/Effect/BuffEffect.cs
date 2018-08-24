using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Common.Tools;
using HolyTech.GameData;
using HolyTech.GameEntity;


namespace HolyTech.Effect
{
    public class BuffEffect : IEffect
    {
        public BuffEffect()
        {
            mType = IEffect.ESkillEffectType.eET_Buff;
        }

        public Ientity entity;        
        public uint InstID;
        
        public override void OnLoadComplete()
        {
            if (entity == null)
            {
                return;
            }
            GetTransform().parent = entity.RealEntity.objBuffPoint.transform;
            GetTransform().position = entity.RealEntity.objBuffPoint.transform.position;
        }

        public override void Update()
        {
            if (isDead)
                return;                   

            base.Update();
        }
    }
}
