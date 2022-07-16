using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadGoogleDrive
{
    public interface IDriveService
    {
        void Upload(string path);
    }
}