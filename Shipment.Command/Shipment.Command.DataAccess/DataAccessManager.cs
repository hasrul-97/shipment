using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Shipment.Command.Abstraction.DataAccess;
using Shipment.Command.DataAccess.Core;
using Shipment.Command.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.DataAccess
{
    public class DataAccessManager : IDataAccessManager
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        public DataAccessManager(IRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is used to upload the list of data to the database.
        /// </summary>
        /// <param name="data">List of Shipment data</param>
        /// <returns></returns>
        public async Task<string> StoreShipmentData(List<ShipmentData> data)
        {
            string message = string.Empty;
            try
            {
                int numberOfRowsAffected = await _repository.AddToDatabaseBulkWithParameter(SQLQueries.InsertData, data);
                if (numberOfRowsAffected > 0)
                {
                    message = "The file is successfully imported";
                }
                else
                {
                    throw new Exception("File not imported");
                }
                return message;
            }
            catch (SqlException exception)
            {
                throw new Exception("There has been an error with the database connection: " + exception.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// this method is used to upload file directly to the Azure Blob storage
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                //  CONTAINER NAME
                var containerName = "shipment";

                //  CONNECTION STRING TO AZURE STORAGE
                string connectionString = _configuration.GetSection("Blob").Value;

                // STORAGE ACCOUNT CONNECTION STRING
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                // CLIENT OF STORAGE ACCOUNT
                CloudBlobClient client = storageAccount.CreateCloudBlobClient();

                // REFERENCE TO THE CONTAINER
                CloudBlobContainer container = client.GetContainerReference(containerName);

                // TO CHECK IF THE CONTAINER IS PRESENT OR CREATE IT
                var isCreated = await container.CreateIfNotExistsAsync();

                using (var fileStream = file.OpenReadStream())
                {

                    CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);
                    blob.Properties.ContentType = file.ContentType;
                    await blob.UploadFromStreamAsync(fileStream);
                    File fileDatum = new File(file.FileName,file.Length,file.ContentType);
                    int numberOfRowsAffected = await _repository.AddToDatabaseWithParameter(SQLQueries.InsertFileUploadData,fileDatum);
                    if (numberOfRowsAffected > 0)
                    {
                    return "The file is uploaded successfully";
                    }
                    else
                    {
                        throw new Exception("File has not been uploaded.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
