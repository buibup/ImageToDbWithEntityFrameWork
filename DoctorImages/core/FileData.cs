using DoctorImages.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorImages.core
{
    public static class FileData
    {
        
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension));
        }

        public static List<ImageModel> CompareDiffList(List<ImageModel> listDirectory, List<ImageModel> listDb)
        {
            List<ImageModel> listResult = new List<ImageModel>();
            
            // Get diff name image between directory and database to list sort by modifydate
            var diffNameList = listDirectory.Where(di => !listDb.Any(db => di.Name == db.Name )).OrderByDescending(d => d.ModifyDate).ToList();
            // Get duplicates name select first to List
            var duplicatesNameList = diffNameList.GroupBy(i => i.Name).Select(g => g.First()).ToList();

            listResult = duplicatesNameList;
         
            // Add new image to database
            foreach (var lstDi in listDirectory)
            {
                ImageModel image = new ImageModel();
                foreach(var lstDb in listDb)
                {
                    if ((lstDi.Name == lstDb.Name) && (lstDi.ModifyDate.ToString("yyyyMMdd HH:mm:ss") != lstDb.ModifyDate.ToString("yyyyMMdd HH:mm:ss")) )
                    {
                        try
                        {
                            using (var db = new ImageContext())
                            {
                                var result = db.Images.SingleOrDefault(i => i.Name == lstDi.Name);
                                if (result != null)
                                {
                                    result.Data = lstDi.Data;
                                    result.ModifyDate = lstDi.ModifyDate;
                                    db.SaveChanges();
                                }
                            }
                        }
                        catch(Exception e)
                        {

                        }

                        
                    }
                }
            }

            
            return listResult;
        }

    }
}
