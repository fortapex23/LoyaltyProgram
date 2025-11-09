using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.ExternalServices.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
