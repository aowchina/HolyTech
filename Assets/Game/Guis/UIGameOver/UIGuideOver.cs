﻿using UnityEngine;
using GameDefine;
using HolyTech.GuideDate;
using HolyTech.Resource;

public class UIGuideOver : MonoBehaviour
{


    private float timeLimit = 0f;
    private float showTime = 10f;
    private float timeStart = 0f;

    static GameObject objUI = null;

    ButtonOnPress btnPress;

    string path = "effect/ui_effect/guide_congratulation";

    void Awake() {
       // objUI.gameObject.SetActive(false);
        BaseDaBomb.StartEffectEvent += StartGuideOver;
        btnPress = transform.Find("Position").GetComponent<ButtonOnPress>();
    }

    void PressOver(int ie, bool Press) { 
        NotifyGameOver();
    }

    void NotifyGameOver() {
        timeLimit = 0f;
        SceneGuideTaskManager.Instance().SetHasFinishedAllGuide(true);
        HolyGameLogic.Instance.EmsgToss_FinishAllGuideToLobby();
        if (UINewsGuide.Instance != null) {
            LoadUiResource.DestroyLoad(SceneGuideTaskManager.guideUiPath);
        }
    }

    void OnDisable() {
        BaseDaBomb.StartEffectEvent -= StartGuideOver;
        btnPress.RemoveListener(PressOver);
    }

    void StartGuideOver(float time) {
        timeLimit = time;
        timeStart = Time.time;
        hasShowEffect = false;
    }

    bool hasShowEffect = false;

    void Update() {
        if (timeLimit > 0f) {
            float timeBetween = Time.time - timeStart;
            if ((timeBetween > timeLimit) && !hasShowEffect)
            {
                LoadUiResource.LoadRes(transform, path);
                hasShowEffect = true;
            }
            else if ((timeBetween > timeLimit +0.5f) && timeBetween < (timeLimit + showTime+0.5f) && (!objUI.activeInHierarchy))
            {
                objUI.SetActive(true);
            }
            else if (timeBetween > (timeLimit + showTime))
            { 
                timeLimit = 0f;
                NotifyGameOver();
            }
        }
    }

    void OnEnable() {
        btnPress.AddListener(PressOver);
    }

    public static void LoadGuideOver()
    {       
        //GameObject obj = GameObject.Instantiate(Resources.Load(GameConstDefine.GuideOverUi)) as GameObject;

        ResourceItem objUnit = ResourcesManager.Instance.loadImmediate(GameConstDefine.GuideOverUi, ResourceType.PREFAB);
        GameObject obj = GameObject.Instantiate(objUnit.Asset) as GameObject;
           
        obj.transform.parent = UINewsGuide.Instance.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        objUI = obj.transform.Find("Position").gameObject;
        objUI.SetActive(false);

        if (SceneGuideTaskManager.Instance().sceneGuideTask != null && SceneGuideTaskManager.Instance().sceneGuideTask.currentTask != null)
        {
            SceneGuideTaskManager.Instance().sceneGuideTask.currentTask.CleanAllTask();
        }
    }
}