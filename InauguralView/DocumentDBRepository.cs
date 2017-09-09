using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace InauguralView
{
    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = "inauguralAddresses"; 
        private static readonly string CollectionId = "inauguralAddresses";
        private static DocumentClient client;
        private static IConfiguration Configuration;

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
            client = new DocumentClient(new System.Uri(Configuration.GetConnectionString("DOCUMENTDB_ENDPOINT")), Configuration.GetConnectionString("DOCUMENTDB_AUTHKEY"));
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                Console.WriteLine("DEBUG DOCUMENT: " + document.ToString());
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
