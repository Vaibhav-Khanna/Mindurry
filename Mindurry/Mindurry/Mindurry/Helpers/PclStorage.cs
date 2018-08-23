using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PCLStorage;
using System.Linq;
using Mindurry.Models.DataObjects;

namespace Mindurry.Helpers
{
    public class PclStorage
    {

      //  private static string folderName = "Documents";

        public static async Task<byte[]> LoadFileLocal(string fileName, string firstFolder, string secondFolder)
        {
            // get hold of the file system  
            try
            {
                
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(firstFolder);

                if (folder != null)
                {
                    // open second folder
                    IFolder folder2 = await folder.GetFolderAsync(secondFolder);
                    if (folder2 != null)
                    {
                        //open file if exists  
                        IFile file = await folder2.GetFileAsync(fileName);

                        if (file != null)
                        {
                            //load stream to buffer  
                            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.Read))
                            {
                                long length = stream.Length;
                                byte[] streamBuffer = new byte[length];
                                stream.Read(streamBuffer, 0, (int)length);
                                return streamBuffer;
                            }
                        }
                        else { return null; }
                    }
                    else { return null; }
               
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<bool> IsFileExistAsync(string fileName, string firstFolder, string secondFolder)
        {
            // get hold of the file system 
            try
            {
               
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(firstFolder);
                if (folder != null)
                {
                    IFolder folder2 = await folder.GetFolderAsync(secondFolder);
                    if (folder2 != null)
                    {
                        ExistenceCheckResult folderexist = await folder2.CheckExistsAsync(fileName);

                        // already run at least once, don't overwrite what's there  
                        if (folderexist == ExistenceCheckResult.FileExists)
                        {
                            return true;

                        }
                        return false;
                    }
                    else { return false; }
                   
                }
                else { return false; }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<List<string>> GetAllLocalFileNames(string folderName)
        {
            try
            {
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(folderName);

                var files = await folder.GetFilesAsync();

                // already run at least once, don't overwrite what's there  
                if (files != null && files.Any())
                {
                    return files.Select((arg) => arg.Name).ToList();
                }
                return new List<string>();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }


        public static async Task<string> ReturnFolderPath(string folderName)
        {
            // get hold of the file system  
            try
            {
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(folderName);


                if (folder != null)
                {
                    return folder.Path;

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /*
        public static async Task<bool> DeleteFile(string fileName, string folderName)
        {
            // get hold of the file system  
            try
            {
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(folderName);

                bool exist = await IsFileExistAsync(fileName, folderName);

                if (exist == true)
                {
                    IFile file = await folder.GetFileAsync(fileName);
                    await file.DeleteAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } */

        public static async Task<bool> SaveFileLocal(byte[] arrayBytes, string fileName, string firstFolder, string secondFolder)
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;

                    IFolder folder = await rootFolder.CreateFolderAsync(firstFolder, CreationCollisionOption.OpenIfExists);
                    IFolder folder2 = await folder.CreateFolderAsync(secondFolder, CreationCollisionOption.OpenIfExists);
                
                        
               
                IFile file = await folder2.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                // populate the file with image data  
                System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite);
                if (stream != null)
                {
                    stream.Write(arrayBytes, 0, arrayBytes.Length);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static async void Init()
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;

                await rootFolder.CreateFolderAsync(ReferenceKind.Apartment.ToString().ToLower(), CreationCollisionOption.OpenIfExists);
                await rootFolder.CreateFolderAsync(ReferenceKind.Cellar.ToString().ToLower(), CreationCollisionOption.OpenIfExists);
                await rootFolder.CreateFolderAsync(ReferenceKind.Customer.ToString().ToLower(), CreationCollisionOption.OpenIfExists);
                await rootFolder.CreateFolderAsync(ReferenceKind.Garage.ToString().ToLower(), CreationCollisionOption.OpenIfExists);
                await rootFolder.CreateFolderAsync(ReferenceKind.Residence.ToString().ToLower(), CreationCollisionOption.OpenIfExists);

                //await rootFolder.CreateFolderAsync(quotations, CreationCollisionOption.OpenIfExists);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
