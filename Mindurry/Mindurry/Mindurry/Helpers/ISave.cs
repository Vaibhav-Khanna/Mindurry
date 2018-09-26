using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mindurry.Helpers
{
    public interface ISave
    {
        Task<StorageFolder> Save(MemoryStream fileStream, string name);
        Task LaunchFolder(StorageFolder folder);
        void CopyToClipboard(string text);
    }
}
