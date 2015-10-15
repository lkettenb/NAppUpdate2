using System;
using System.IO;
using System.Text;
using NAppUpdate.Framework.Common;

namespace NAppUpdate.Framework.Sources
{
    /// <summary>
    /// An IUpdateSource implementation for retreiving the update feed and data 
    /// from a UNC path
    /// 
    /// Example
    ///		private const string FeedName = "Feed.xml";
    ///		UpdateManager manager = UpdateManager.Instance;
    ///		manager.UpdateFeedReader = new NAppUpdate.Framework.FeedReaders.NauXmlFeedReader();
    ///		manager.UpdateSource = new NAppUpdate.Framework.Sources.UncSource(string.Format("{0}\\{1}", UpdatePath, FeedName), UpdatePath);
    /// </summary>
    public class UncSource : IUpdateSource, IDisposable
    {
        public UncSource() { }

        public UncSource(string feedUncPath, string uncPath)
        {
            FeedUncPath = feedUncPath;
            UncPath = uncPath;
        }

        /// <summary>
        /// The feed path, e.g. \\remoteComputer\SharedFolder\MyAppUpdates\Feed.xml
        /// </summary>
        public string FeedUncPath { get; set; }

        /// <summary>
        /// The Unc path to get updates data from
        /// e.g. XML Feed folder: \\remoteComputer\SharedFolder\MyAppUpdates
        /// </summary>
        public string UncPath { get; set; }

        private StreamReader Stream { get; set; }

        public Stream GetUpdatesFeed()
        {
            if (Stream != null) Stream.Close();
            Stream = new StreamReader(FeedUncPath, Encoding.UTF8, true);
            return Stream.BaseStream;
        }

        public void CloseStream()
        {
            if (Stream != null) Stream.Close();
        }


        public bool GetData(string filePath, string basePath, Action<UpdateProgressInfo> onProgress, ref string tempLocation)
        {
            if (basePath == null)
            {
                basePath = UncPath;
            }
            if (!basePath.EndsWith("\\"))
            {
                basePath += "\\";
            }

            File.Copy(basePath + filePath, tempLocation);
            return true;
        }

        public void Dispose()
        {
            if (Stream != null) Stream.Dispose();
        }
    }
}
