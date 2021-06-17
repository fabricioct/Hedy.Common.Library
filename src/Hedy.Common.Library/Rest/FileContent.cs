using System.IO;
using System.Net.Http;

namespace Hedy.Core.Infrastructure.Rest
{
    public class FileContent : MultipartFormDataContent
    {
        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            this.Add(new StreamContent(filestream), apiParamName, filename);
        }
    }
}