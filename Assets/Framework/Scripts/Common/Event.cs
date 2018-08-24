using System.Collections.Generic;

namespace HolyTech
{
    public class CEvent
    {
        private GameEventEnum eventId;
        private Dictionary<string, object> paramList;

        public CEvent()
        {
            paramList = new Dictionary<string, object>();
        }

        public CEvent(GameEventEnum id)
        {
            eventId = id;
            paramList = new Dictionary<string, object>();
        }

        //获取消息类型
        public GameEventEnum GetEventId()
        {
            return eventId;
        }

        public void AddParam(string name, object value)
        {
            paramList[name] = value;
        }

        public object GetParam(string name)
        {
            if (paramList.ContainsKey(name))
            {
                return paramList[name];
            }
            return null;
        }

        public bool HasParam(string name)
        {
            if (paramList.ContainsKey(name))
            {
                return true;
            }
            return false;
        }

        public int GetParamCount()
        {
            return paramList.Count;
        }

        public Dictionary<string, object> GetParamList()
        {
            return paramList;
        }
    }
}