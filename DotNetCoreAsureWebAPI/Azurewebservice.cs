using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; 
namespace DotNetCoreAsureWebAPI
{
    public class Azurewebservice
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string azureConnectionstring = "DefaultEndpointsProtocol=https;AccountName=abcreatedstorage;AccountKey=CHAy0N1ouhd6ZpkveMzZiCjTXgsadS1vOz7nEesmhaDoo/r5oNCQeb0VyZAv+wK+vYcAWSvsQbpp+ASt4OL/pw==;EndpointSuffix=core.windows.net";

        public Azurewebservice()
        {
            _blobServiceClient = new BlobServiceClient(azureConnectionstring);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("abcontainer");

        }

        public async Task<List<BlobContentInfo>>UploadFiles(List<IFormFile>files)
        {
            var azureResponse = new List<BlobContentInfo>();
            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var memoryStream  = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                    azureResponse.Add(client);
                }
            };

            return azureResponse;
        }
        public async Task<List<BlobItem>>GetUploadedBlob()
        {
            var items = new List<BlobItem>();
            var UploadedFiles = _blobContainerClient.GetBlobsAsync();
        
           await foreach ( BlobItem file in UploadedFiles)
            {
                items.Add(file);
            }

            return items;
        }
    }
}
