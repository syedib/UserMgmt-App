using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserMgmtApi.Dtos;

namespace UserMgmtApi.Services
{
    public class FileUploadService
    {
        public async Task<string> UploadPassportAsync(IFormFile file, string passportNumber) 
        {
            string uploadFolderPath = Directory.GetCurrentDirectory() + "\\Uploads\\" + passportNumber;
            string fileName = file.FileName;
            string filePath = Path.Combine(uploadFolderPath, fileName);

            try
            {
                // Create the upload folder if it doesn't exist
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Check if the file already exists in the directory
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Remove the existing file
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch 
            {
                filePath = "";
            }

            return filePath;
        }

        public async Task<byte[]> GetPhotoAsync(IFormFile photo) 
        {
            byte[] photoData = default;

            try 
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    photoData = memoryStream.ToArray();
                }
            }
            catch(Exception ex) 
            {
            
            }

            return photoData;
        }

        public FileDetail? GetFileDetail(string filePath)
        {
            if (File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Convert the byte array to an IFormFile
                IFormFile formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", Path.GetFileName(filePath));

                return new FileDetail(formFile.FileName, Path.GetExtension(filePath), formFile.Length);

            }

            return null;
        }
    }
}
