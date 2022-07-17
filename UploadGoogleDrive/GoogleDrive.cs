using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;


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
        public void Upload(string ArquivoZip)
        {

            if(System.IO.File.Exists(ArquivoZip))
            {
                Console.Clear();
                Console.WriteLine("[+] Iniciando Upload no Google Drive");

                var fileMetadata = new Google.Apis.Drive.v3.Data.File();

                fileMetadata.Name = Path.GetFileName(ArquivoZip);
                fileMetadata.MimeType = "application/zip";
                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(ArquivoZip, FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream,"application/zip");
                    request.Fields = "id";
                    
                    request.Upload();
                }

                var file = request.ResponseBody;                
                var Url = GeneratePublicUrl(file.Id);
                
                Console.WriteLine("");
                Console.WriteLine($"Link para Download: {Url}");
                Console.WriteLine("Upload Efetuado com sucesso!");
            }
        }
        #endregion

        #region GeneratePublicUrl
        private string GeneratePublicUrl(string fileId)
        {
            try
            {
                Permission userPermission = new Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };

                var request = this.service.Permissions.Create(userPermission, fileId);
                request.Execute();

                var Url = $"https://drive.google.com/uc?export=download&confirm=t&id={request.FileId}";

                return Url;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion
    }
}