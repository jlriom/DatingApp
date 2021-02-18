using System;
using System.IO;
using API.Helpers;
using API.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace API.Services
{
    public class PhotoService: IPhotoService
    {
        private const string containerName = "images";

        readonly BlobContainerClient _containerClient; 

        public PhotoService(IOptions<ImageStorageSettings> imageStorageSettingsOptions)
        {
            var blobServiceClient = new BlobServiceClient(imageStorageSettingsOptions.Value.ConnectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var url = $"{_containerClient.Uri}/{filename}";
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var response = await _containerClient.UploadBlobAsync(filename, stream);
            }
            return new ImageUploadResult
            {
                Url = url,
                PublicId = filename
            };
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {

            await _containerClient.DeleteBlobAsync(publicId);
            return new DeletionResult();
        }
    }
}