using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.Entities
{
    public class File
    {
        public File(string fileName,long fileSize,string fileType)
        {
            FileGUID = Guid.NewGuid().ToString();
            FileName = fileName;
            FileSize = fileSize;
            FileType = fileType;
            UploadedOn = DateTime.UtcNow;
        }
        public File()
        {

        }
        public string FileGUID { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
