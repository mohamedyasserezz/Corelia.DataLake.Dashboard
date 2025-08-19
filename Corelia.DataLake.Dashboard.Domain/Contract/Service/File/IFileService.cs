using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Microsoft.AspNetCore.Http;
namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.File
{
    public interface IFileService
    {
        public Task<string> SaveFileAsync(IFormFile imageFile, string subfolder);

        public string GetProfileUrl(ApplicationUser user);
        public string GetImageUrl(string subFolder, string Image);
        public void DeleteFile(string file, string subfolder);
       public Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
