using System;
using System.Collections.Generic;
using NAppUpdate.Framework.Common;
using System.IO;

namespace NAppUpdate.Framework.Sources
{
    public class MemorySource : IUpdateSource, IDisposable
    {
        private readonly Dictionary<Uri, string> tempFiles;

        public MemorySource(Stream feed)
        {
            this.Feed = feed;
            this.tempFiles = new Dictionary<Uri, string>();
        }

        public Stream Feed { get; set; }

        public void AddTempFile(Uri uri, string path)
        {
            tempFiles.Add(uri, path);
        }

        #region IUpdateSource Members

        public Stream GetUpdatesFeed()
        {
            return Feed;
        }

        public bool GetData(string filePath, string basePath, Action<UpdateProgressInfo> onProgress, ref string tempFile)
        {
            Uri uriKey = null;

            if (Uri.IsWellFormedUriString(filePath, UriKind.Absolute))
                uriKey = new Uri(filePath);
            else if (Uri.IsWellFormedUriString(basePath, UriKind.Absolute))
                uriKey = new Uri(new Uri(basePath, UriKind.Absolute), filePath);

            if (uriKey == null || !tempFiles.ContainsKey(uriKey))
                return false;

            tempFile = tempFiles[uriKey];

            return true;
        }

        #endregion


        public void CloseStream()
        {
            if (Feed != null) Feed.Close();
        }

        public void Dispose()
        {
            if (Feed != null) Feed.Dispose();
        }
    }
}
