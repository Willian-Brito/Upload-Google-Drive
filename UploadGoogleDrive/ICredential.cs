using Google.Apis.Auth.OAuth2;

namespace UploadGoogleDrive
{
    public interface ICredential
    {
        UserCredential GetCredentials();
    }
}