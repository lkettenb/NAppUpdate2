using System;
using NAppUpdate.Framework.Common;
using System.IO;

namespace NAppUpdate.Framework.Sources
{
    public interface IUpdateSource
    {
        Stream GetUpdatesFeed();
        void CloseStream();
		bool GetData(string filePath, string basePath, Action<UpdateProgressInfo> onProgress, ref string tempLocation);
    }
}
