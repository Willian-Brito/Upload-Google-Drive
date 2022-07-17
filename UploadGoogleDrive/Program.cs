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
                
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}