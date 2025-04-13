using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.services.Attachments
{
    public class AttachmentService : IAttachmetService
    {

        private readonly List<string> AllowedExtentions = new List<string>() { ".jpg", ".png", ".jpeg" };

        private const int FileMaximumSize = 2_097_152;

       

        public string UploadImage(IFormFile File, string FolderName)
        {

            var fileExtention = Path.GetExtension(File.FileName);

            if (!AllowedExtentions.Contains(fileExtention))
                throw new Exception("File Extention is not allowed");
            if (File.Length > FileMaximumSize)
                throw new Exception("File Size is too large");


            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", FolderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}_{File.FileName}";
            var filePath = Path.Combine(folderPath, fileName);



            using var fs = new FileStream(filePath, FileMode.Create);
            File.CopyTo(fs);


            return fileName;
        }

        public bool Delete(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }
    }
}
