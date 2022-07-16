using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace UploadGoogleDrive
{
    public class GoogleDrive : IDriveService
    {
        #region Propriedades da Classe
        public string ApplicationName { get; set; }
        public UserCredential credential { get; set; }
        public DriveService service { get; set; }        
        #endregion
        
        #region Construtor
        public GoogleDrive(string applicationName, UserCredential credential)
        {
            this.ApplicationName = applicationName;
            this.credential = credential;
            this.service = StartService();
        }
        #endregion

        #region Metodos

        #region StartService
        private DriveService StartService()
        {
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this.credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }
        #endregion

        #region Upload
        public void Upload(string path)
        {

            if(File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("[+] Iniciando Upload no Google Drive");

                var fileMetadata = new Google.Apis.Drive.v3.Data.File();

                fileMetadata.Name = Path.GetFileName(path);
                fileMetadata.MimeType = "application/zip";
                FilesResource.CreateMediaUpload request;

                using (var stream = new System.IO.FileStream(path,System.IO.FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream,"application/zip");
                    request.Fields = "id";
                    request.Upload();
                }

                var file = request.ResponseBody;

                Console.WriteLine($"Link para Download: https://drive.google.com/uc?export=download&confirm=t&id={file.Id}");
                System.Console.WriteLine("Upload Efetuado com sucesso!");
            }
        }
        #endregion

        #endregion
    }
}