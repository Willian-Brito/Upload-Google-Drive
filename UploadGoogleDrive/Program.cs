using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace UploadGoogleDrive
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                string ApplicationName = "UploadDrive";
                var ArquivoZip = "/home/h1s0k4/Área de trabalho/App/AppWeb.zip";
                

                ICredential Credential  = new GoogleCredential();
                IDriveService DriveService = new GoogleDrive(ApplicationName, Credential.GetCredentials());

                DriveService.Upload(ArquivoZip);
                // UserCredential credential;

                // credential = GetCredentials();

                // // Create Drive API service.
                // var service = new DriveService(new BaseClientService.Initializer()
                // {
                //     HttpClientInitializer = credential,
                //     ApplicationName = ApplicationName,
                // });


                // UploadBasicImage(ArquivoZip, service);

                // Console.WriteLine("Done");
                // Console.Read();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private static void UploadBasicImage(string path, DriveService service)
        {
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

            Console.WriteLine("File ID: " + file.Id);

        }

        // private static UserCredential GetCredentials()
        // {
        //     UserCredential credential;

        //     using (var stream = new FileStream("drive.json", FileMode.Open, FileAccess.Read))
        //     {
        //         string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        //         credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

        //         credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //             GoogleClientSecrets.Load(stream).Secrets,
        //             Scopes,
        //             "user",
        //             CancellationToken.None,
        //             new FileDataStore(credPath, true)).Result;
        //        // Console.WriteLine("Credential file saved to: " + credPath);
        //     }

        //     return credential;
        // }
    }
}