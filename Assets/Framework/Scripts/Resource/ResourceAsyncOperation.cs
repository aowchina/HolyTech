﻿using UnityEngine;

namespace HolyTech.Resource
{
    public class ResourceAsyncOperation
    {
        internal ResourceRequestType mRequestType;
        internal int mAllDependencesAssetSize;
        internal int mLoadDependencesAssetSize;
        internal bool mComplete;
        public AsyncOperation asyncOperation;

        internal ResourceItem mResource;

        internal ResourceAsyncOperation(ResourceRequestType requestType)
        {
            mRequestType = requestType;
            mAllDependencesAssetSize = 0;
            mLoadDependencesAssetSize = 0;
            mComplete = false;
            asyncOperation = null;
            mResource = null;
        }

        public bool Complete
        {
            get
            {
                return mComplete;
            }
        }

        public int Prograss
        {
            get
            {
                if (mComplete)
                    return 100;
                else if (0 == mLoadDependencesAssetSize)
                    return 0;
                else
                {
                    //使用assetbundle
                    if (ResourcesManager.Instance.UsedAssetBundle)
                    {
                        if (ResourceRequestType.LOADLEVEL == mRequestType)
                        {
                            int depsPrograss = (int)(((float)mLoadDependencesAssetSize / mAllDependencesAssetSize) * 100);
                            int levelPrograss = asyncOperation != null ? (int)((float)asyncOperation.progress * 100.0f) : 0;
                            return (int)(depsPrograss * 0.8) + (int)(levelPrograss * 0.2);
                        }
                        else
                        {
                            return (int)(((float)mLoadDependencesAssetSize / mAllDependencesAssetSize) * 100);
                        }
                    }
                    //不使用
                    else
                    {
                        if (ResourceRequestType.LOADLEVEL == mRequestType)
                        {
                            int levelPrograss = asyncOperation != null ? (int)((float)asyncOperation.progress * 100.0f) : 0;
                            return levelPrograss;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
    }

}