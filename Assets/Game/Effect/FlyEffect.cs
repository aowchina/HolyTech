﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using GameDefine;
using Common.Tools;
using HolyTech.GameData;
using HolyTech.GameEntity;
using HolyTech;
using HolyTech.Skill;
using HolyTech.Resource;

//技能飞行效果
namespace HolyTech.Effect
{
    public class FlyEffect : IEffect
    {
        public bool isAbsorb = false;//是否是吸附技能。0: 不是      1:是
        public bool isTurn = false;      
        public int emitType;            //保存飞行类型

        public FlyEffect()
        {
            mType = IEffect.ESkillEffectType.eET_FlyEffect;
        }
     

        //对丝带拖拽类型，收回
        public void DragRibbonBack()
        {
            fyDragRibbon[] mDragRibbon = obj.GetComponentsInChildren<fyDragRibbon>();
            foreach (fyDragRibbon ribbon in mDragRibbon)
            {                
                ribbon.ForeceBack();
            }   
        }

        public override void OnLoadComplete()
        {
            SkillEmitConfig skillInfo = ConfigReader.GetSkillEmitCfg(skillID);
            Transform point;
            Player enOwner, enTarget;

            PlayersManager.Instance.PlayerDic.TryGetValue(enOwnerKey, out enOwner);
            PlayersManager.Instance.PlayerDic.TryGetValue(enTargetKey, out enTarget);

            point = enOwner.objPoint.transform;
                       
            //拖拽效果
            if (skillInfo.emitType == 8)
            {
                fyDragRibbon[] mDragRibbon = obj.GetComponentsInChildren<fyDragRibbon>();
                foreach (fyDragRibbon ribbon in mDragRibbon)
                {
                    ribbon.mDir = dir;
                    ribbon.Link(point);
                }   
            }
            else
            {              
                if (point != null)
                {
                    GetTransform().position = point.transform.position;
                }
                if (skillInfo.emitType == 1 || skillInfo.emitType == 4 || skillInfo.emitType == 5)
                {
                    if (enTarget == null)
                    {
                        Quaternion rt = Quaternion.LookRotation(fixPosition - GetTransform().position);
                        GetTransform().rotation = rt;
                    }
                    else
                    {
                        Quaternion rt = Quaternion.LookRotation(enTarget.RealEntity.transform.position - GetTransform().position);
                        GetTransform().rotation = rt;
                    }
                }
                else if (skillInfo.emitType == 2 || skillInfo.emitType == 6)
                {
                    Quaternion rt = Quaternion.LookRotation(dir);
                    GetTransform().rotation = rt;
                }
            }
         
            //播放声音
            string soundPath = GameConstDefine.LoadGameSoundPath + skillInfo.sound;
            ResourceItem objUnit = ResourcesManager.Instance.loadImmediate(soundPath, ResourceType.ASSET);

            if (objUnit.Asset != null)
            {
                AudioClip clip = objUnit.Asset as AudioClip;
                if (clip != null)
                {
                    AudioSource audio = AudioManager.Instance.PlayEffectAudio(clip);
                    SceneSoundManager.Instance.addSound(audio, obj);
                }
            }         
        }
        private void UpdateNoTargetType()
        {
            SkillEmitConfig skillinfo = ConfigReader.GetSkillEmitCfg(skillID);
            Vector3 flyDir = Vector3.zero;
            float speed = skillinfo.flySpeed;
            Vector3 Pos = GetTransform().position;
            flyDir = dir;
            flyDir.Normalize();
            Quaternion rt = Quaternion.LookRotation(flyDir);
            GetTransform().rotation = rt;
            GetTransform().rotation = rt;
            Pos += flyDir * Time.deltaTime * speed;
            GetTransform().position = Pos;
            
       }
        private void UpdateTargetType()
        {
            //Ientity enTarget;            
            //EntityManager.AllEntitys.TryGetValue(enTargetKey, out enTarget);
            Player enTarget;
            PlayersManager.Instance.PlayerDic.TryGetValue(enTargetKey,out enTarget);

            SkillEmitConfig skillinfo = ConfigReader.GetSkillEmitCfg(skillID);
            Vector3 flyDir = Vector3.zero;
            float speed = skillinfo.flySpeed;
            Vector3 Pos = GetTransform().position;
            float distance;// = Vector3.Distance(root.transform.position, enTarget.RealEntity.objAttackPoint.position);
           // if (enTarget == null || enTarget.RealEntity == null || enTarget.RealEntity.objAttackPoint == null)
            if (enTarget == null || enTarget.RealEntity == null || enTarget.RealEntity.transform.position == null)
            {
                flyDir = fixPosition - Pos;
                distance = Vector3.Distance(Pos, fixPosition);
            }
            else
            {
               // flyDir = enTarget.RealEntity.objAttackPoint.transform.position - Pos;
                // distance = Vector3.Distance(GetTransform().position, enTarget.RealEntity.objAttackPoint.position);
                flyDir = enTarget.RealEntity.transform.position - Pos;
                distance = Vector3.Distance(GetTransform().position, enTarget.RealEntity.transform.position);
            }
            
            if (flyDir == Vector3.zero)
            {
                flyDir = Vector3.one;
            }
            flyDir.Normalize();
            Quaternion rt = Quaternion.LookRotation(flyDir);
            GetTransform().rotation = rt;
            Pos += flyDir * Time.deltaTime * speed;
            GetTransform().position = Pos;
            
            if (speed * Time.deltaTime > distance)
            {
                //Debug.LogError("fly effect endtime:" + Time.time);
                isDead = true;
            }
            //else
            //{
            //    Pos += flyDir * Time.deltaTime * speed;
            //    root.transform.position = Pos;
            //}
        }
        private void UpdateTurnType()
        {
            Ientity enOwner;
            EntityManager.AllEntitys.TryGetValue(enOwnerKey, out enOwner);

            if (isTurn == false)
            {
                SkillEmitConfig skillinfo = ConfigReader.GetSkillEmitCfg(skillID);
                Vector3 flyDir = Vector3.zero;
                float speed = skillinfo.flySpeed;
                Vector3 Pos = GetTransform().position;
                flyDir = dir;
                flyDir.Normalize();
                Quaternion rt = Quaternion.LookRotation(flyDir);
                GetTransform().rotation = rt;
                GetTransform().rotation = rt;
                Pos += flyDir * Time.deltaTime * speed;
                GetTransform().position = Pos;
            }
            else
            {
                
                SkillEmitConfig skillinfo = ConfigReader.GetSkillEmitCfg(skillID);
                Vector3 flyDir = Vector3.zero;
                float speed = skillinfo.flySpeed;
                Vector3 Pos = GetTransform().position;
                float distance;// = Vector3.Distance(root.transform.position, enTarget.RealEntity.objAttackPoint.position);
                if (enOwner == null || enOwner.RealEntity == null || enOwner.RealEntity.objAttackPoint == null)
                {
                    flyDir = fixPosition - Pos;
                    distance = Vector3.Distance(Pos, fixPosition);
                }
                else
                {
                    flyDir = enOwner.RealEntity.objAttackPoint.transform.position - Pos;
                    distance = Vector3.Distance(GetTransform().position, enOwner.RealEntity.objAttackPoint.position);
                }

                if (flyDir == Vector3.zero)
                {
                    flyDir = Vector3.one;
                }
                flyDir.Normalize();
                Quaternion rt = Quaternion.LookRotation(flyDir);
                GetTransform().rotation = rt;
                Pos += flyDir * Time.deltaTime * speed;
                GetTransform().position = Pos;

                if (speed * Time.deltaTime > distance)
                {
                    //Debug.LogError("fly effect endtime:" + Time.time);
                    isDead = true;
                }
            }
        }
        public override void Update()
        {
            if (isDead)
                return;

            base.Update();
           
            SkillEmitConfig skillinfo = ConfigReader.GetSkillEmitCfg(skillID);      //外部已经判断
            //拖拽效果，不需要更新
            if (skillinfo.emitType == 8)
                return;

            //Ientity enTarget;
            //EntityManager.AllEntitys.TryGetValue(enTargetKey, out enTarget);
            Player enTarget;
            PlayersManager.Instance.PlayerDic.TryGetValue(enOwnerKey,out enTarget);
            
            //目标角色消失，特效消失
            if (skillinfo.emitType == 1 && enTarget == null)
            {                      
                isDead = true;
                return;
            }

            switch (skillinfo.emitType)
            {
                case 1://有目标类型
                case 4:
                case 5:
                case 7:
                    {
                        UpdateTargetType();
                    }
                    break;
                case 2://无目标类型
                case 6:
                    {
                        UpdateNoTargetType();
                    }
                    break;
                case 3:
                    {
                        UpdateTurnType();
                    }
                    break;
            }
        }
    }
}

