using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;

namespace GarPuller.ServiceLayer
{
/*     public class MyKey : IEquatable<MyKey>
    {
        public bool Equals(MyKey? other)
        {
            throw new NotImplementedException();
        }
    } */
    public static class GarFileMapper
    {

        public static IQueryable<GarFile> 
            MapDownloadFileInfoToGarFile(this IQueryable<DownloadFileInfo> downloadFilesInfo) 
        {    
            return downloadFilesInfo.Select(fileInfo => new GarFile
            {
                Date = ParseToDate(fileInfo.Date),
                SubmittedAt = DateTime.Now,
                DeltaUrl = fileInfo.GarXMLDeltaURL,
                FullUrl = fileInfo.GarXMLFullURL,
                CorrelationId = Guid.NewGuid()
            });  
        }
        public static DateTime ParseToDate(string date) 
        {
            DateTime dateResult;
            DateTime.TryParse(date, out dateResult);
            return dateResult;
        }
    }
}