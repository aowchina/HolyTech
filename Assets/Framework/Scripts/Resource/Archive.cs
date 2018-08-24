using System.Collections.Generic;

namespace HolyTech.Resource
{
    public class Archive
    {
        private Dictionary<string, string> mAllFiles; //["name-type", "name1-type1", "name2-type2"]

        public Archive()
        {
            mAllFiles = new Dictionary<string, string>();
        }

        public Dictionary<string, string> AllFiles
        {
            get
            {
                return mAllFiles;
            }
        }

        public void add(string filename, string type)
        {
            if (!mAllFiles.ContainsKey(filename))
            {
                mAllFiles.Add(filename, type);
            }
        }

        public string getPath(string fileName)
        {
            if (mAllFiles.ContainsKey(fileName))
                return fileName + "." + mAllFiles[fileName];           //name+type
            else
                DebugEx.LogError("can not find " + fileName, ResourceCommon.DEBUGTYPENAME);
            return null;
        }
    }
}