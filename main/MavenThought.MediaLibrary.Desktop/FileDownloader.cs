using System.Net;

namespace MavenThought.MediaLibrary.Desktop
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string fileName);
    }

    public class FileDownloader : IFileDownloader
    {
        private readonly WebClient _webClient;

        public FileDownloader()
        {
            _webClient = new WebClient();
        }

        public void DownloadFile(string url, string fileName)
        {
            _webClient.DownloadFile(url, fileName);
        }
    }
}