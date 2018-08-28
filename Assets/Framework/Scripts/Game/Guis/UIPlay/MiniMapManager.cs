using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine; 
using HolyTech.GameData;
using HolyTech.GameEntity; 
using System.Linq;
using HolyTech;
using HolyTech.GameState;

public class MiniMapManager : Singleton<MiniMapManager>
{
    const float updateTime = 1f;
    float timeStart = 0f;
    bool tagCheck = false;
    public MiniMapManager() { 
         EventCenter.AddListener<CEvent>(GameEventEnum.GameEvent_Loading, OnEvent);
         EventCenter.AddListener<UInt64>(GameEventEnum.GameEvent_GameOver, OnGameOver);         
    }

    public void Update() {
        if (!tagCheck)
            return;
        if (Time.time - timeStart >= updateTime) {
            EventCenter.Broadcast(GameEventEnum.GameEvent_UpdateMiniMap);
            timeStart = Time.time;
        }
    }

    private void OnGameOver(UInt64 BaseGuid)
    {
        tagCheck = false;
    }

    private void OnEvent(CEvent evt)
    {
        switch (evt.GetEventId())
        {
            case GameEventEnum.GameEvent_Loading:
                {
                    GameStateTypeEnum stateType = (GameStateTypeEnum)evt.GetParam("NextState");
                    if (stateType != GameStateTypeEnum.GS_Play)
                        return;
                    timeStart = Time.time;
                    tagCheck = true;
                }
                break;
        }
    }


    

}

