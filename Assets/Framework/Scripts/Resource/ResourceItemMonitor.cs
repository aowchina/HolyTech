using System;
using UnityEngine;


namespace HolyTech.Resource
{
    class ResourceItemMonitor : MonoBehaviour
    {
        public delegate void Construct(int instanceID, string name);
        public delegate void Destruct(int instanceID, string name);

        public static Construct mCHandle;
        public static Destruct mDHandle;

        void Awake()
        {
            mCHandle(gameObject.GetInstanceID(), gameObject.name);
        }

        void OnDestroy()
        {
            mDHandle(gameObject.GetInstanceID(), gameObject.name);
        }

        ~ResourceItemMonitor()
        {
            //同步异步问题,不再在此处调用存在同步问题的操作
            //mDHandle();
        }
    }
}
