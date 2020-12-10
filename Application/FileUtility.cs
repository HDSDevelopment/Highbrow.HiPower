using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Highbrow.HiPower.Application
{
    public class FileUtility
    {

        public async Task<FileProcessResult> Create(string directory,
                                    string createFileName,
                                    IFormFile formFile)
        {
            FileProcessResult result = null;

            if (formFile.Length > 0)
                result = await CreateFile(directory, createFileName, formFile, result);

            return result;
        }

        public async Task<FileProcessResult> Update(string directory,
                                            string previousFileName,
                                            string updateFileName,
                                            IFormFile formFile)
        {
            FileProcessResult result = null;

            if (formFile.Length > 0)
            {
                string directoryPath = directory;

                if (string.IsNullOrEmpty(previousFileName))
                    DeleteFile(directoryPath, previousFileName);

                result = await CreateFile(directory, updateFileName, formFile, result);
            }
            return result;
        }

        async Task<FileProcessResult> CreateFile(string directory,
                                                string fileName,
                                                IFormFile formFile,
                                                FileProcessResult result)
        {
            string fileExtension = Path.GetExtension(formFile.FileName);
            var newFileName = fileName + fileExtension;

            string directoryPath = directory;

            CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, newFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            result.FileName = newFileName;
            result.LengthOfFile = formFile.Length;
            result.ContentType = formFile.ContentType;            

            return result;
        }

        void DeleteFile(string directory, string fileWithExtension)
        {
            string deleteFileWithPath = Path.Combine(directory, fileWithExtension);

            if (System.IO.File.Exists(deleteFileWithPath))
                System.IO.File.Delete(deleteFileWithPath);
        }

        void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}