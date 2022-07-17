using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;


namespace UploadGoogleDrive
{
    public class GoogleCredential : ICredential
    {
        #region Construtor
        public GoogleCredential() { }
        #endregion      

        #region Metodos
        public UserCredential GetCredentials()
        {
            UserCredential credential;
            string[] Scopes = { DriveService.Scope.Drive };

            using (var stream = new FileStream("drive.json", FileMode.Open, FileAccess.Read))
            {
                var credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = System.IO.Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync
                (
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    // "user",
                    "anyone",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                ).Result;
            }

            return credential;
        }
        #endregion 
    }
}