using Acr.UserDialogs;
using Mindurry.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Xamarin.Forms;

[assembly: Dependency(typeof(Mindurry.UWP.Renderers.SaveWindows))]
namespace Mindurry.UWP.Renderers
{ 
    public class SaveWindows : ISave
    {
        MemoryStream stream;
        // StorageFolder folder;
        StorageFile file;

        public async Task<StorageFolder> Save(MemoryStream documentStream, string name)
        {
            documentStream.Position = 0;
            stream = documentStream;
            var folder = await SaveAsync(name);
            return folder;

             //await LaunchFolder(folder);


        }

        public void CopyToClipboard(string text)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }

        public async Task LaunchFolder(StorageFolder folder)
        {
                var toto = await Launcher.LaunchFolderAsync(folder);
        }

        private async Task<StorageFolder> SaveAsync(string name)
        {
        
            FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.ComputerFolder };
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                using (UserDialogs.Instance.Loading("Téléchargement du document", null, null, true))
                {
                    file = await folder.CreateFileAsync(name, CreationCollisionOption.GenerateUniqueName);
                    if (file != null)
                    {
                        Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                        Stream st = fileStream.AsStreamForWrite();
                        st.SetLength(0);
                        st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                        st.Flush();
                        st.Dispose();
                        fileStream.Dispose();
                        return folder;
                    }
                    else { return null; }
                }
            }
            else { return null; }
        }

    }
}

