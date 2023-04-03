using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace sas_project.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
          
            string accountName = "Account Name";
            string accountKey = "Account Key";
            string containername = "containername";
            //adding expire time after 1 minutes from now
            var offset = TimeSpan.FromMinutes(1);
            BlobSasBuilder sasbuilder = new BlobSasBuilder()
            {
                BlobContainerName = "BlobContainerName",
                BlobName = "BlobName",
                StartsOn = DateTime.Now.Subtract(offset),
                ExpiresOn = DateTime.Now.Add(offset)
                //Let SAS token expire after 5 minutes.
            };
            sasbuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);
            //User will only be able to read the blob and it's properties
            var sasToken = sasbuilder.ToSasQueryParameters(new StorageSharedKeyCredential(accountName, accountKey)).ToString();
           

            return sasToken;



            //Alternate Way Of generating Sas Token without azure storage nuget package

            container reource uri
            string resourceUri = "your container base url";
            string keyName = "keyName";
            string key = "key";
            TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var week = 60 * 60 * 24 * 7;
            var expiry = DateTime.Now.AddMinutes(5);
            string stringToSign = HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = String.Format(CultureInfo.InvariantCulture, "sig={0}&se={1}&skn={2}", HttpUtility.UrlEncode(signature), expiry, keyName);

            return sasToken;





        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
