using System.Collections.Generic;
using NAppUpdate.Framework.Tasks;
using System.IO;

namespace NAppUpdate.Framework.FeedReaders
{
    public interface IUpdateFeedReader
    {
        IList<IUpdateTask> Read(Stream feed);
    }
}
