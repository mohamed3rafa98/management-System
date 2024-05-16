using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1. Get Folder Path

            //string folderPath = "C:\\Users\\maraf\\source\\repos\\Demo MVC First Project Solution\\Demo.pl\\wwwroot\\Files\\" + folderName;

            //string folderPath = Directory.GetCurrentDirectory() + "wwwroot\\Files\\" + folderName;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files" ,folderName);

            // 2. Get file Name must be unique 

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File Path --> folderPath + fileName

            string filePath = Path.Combine(folderPath, fileName);

            // 4. Save file as Stream 

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
