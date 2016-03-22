using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace tkdxplWebCDN.Controllers
{
    public class BlobImageController : Controller
    {
        static List<string> _japanList = new List<string>();
        static List<string> _japanCdnList = new List<string>();
        static List<string> _europeList = new List<string>();

        // GET: BlobImage
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PublicEuropeWest()
        {
            if (_europeList.Count == 0)
            {
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureStorageEurope"].ToString());
                var blobClient = storageAccount.CreateCloudBlobClient();
                blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
                var imagesBlobContainer = blobClient.GetContainerReference("public");
                await imagesBlobContainer.CreateIfNotExistsAsync();
                var lst = imagesBlobContainer.ListBlobs(useFlatBlobListing: true);
                foreach (var item in lst)
                {
                    _europeList.Add(item.Uri.ToString());
                }
            }
            

            return View(_europeList);
        }
        public async Task<ActionResult> PublicJapanWest()
        {
            if (_japanList.Count == 0)
            {
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureStorageJapan"].ToString());
                var blobClient = storageAccount.CreateCloudBlobClient();
                blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
                var imagesBlobContainer = blobClient.GetContainerReference("public");
                await imagesBlobContainer.CreateIfNotExistsAsync();
                var lst = imagesBlobContainer.ListBlobs(useFlatBlobListing: true);
                foreach (var item in lst)
                {
                    _japanList.Add(item.Uri.ToString());
                }
            }

            return View(_japanList);
        }
        public async Task<ActionResult> PublicJapanWestCDN()
        {
            //http://az694211.vo.msecnd.net/
            if (_japanCdnList.Count == 0)
            {
                var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureStorageJapan"].ToString());
                var blobClient = storageAccount.CreateCloudBlobClient();
                blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
                var imagesBlobContainer = blobClient.GetContainerReference("public");
                await imagesBlobContainer.CreateIfNotExistsAsync();
                var lst = imagesBlobContainer.ListBlobs(useFlatBlobListing: true);
                foreach (var item in lst)
                {
                    string cdnuri = "http://az694211.vo.msecnd.net/public/" + item.Uri.Segments.Last();
                    _japanCdnList.Add(cdnuri);
                }
            }

            return View(_japanCdnList);
        }
    }
}