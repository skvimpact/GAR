using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GarPublicClient
{
    public class CertifiedHttpClient : HttpClient
    {      
        public CertifiedHttpClient(string? certificateName = null) :
            base(HttpClientHandlerFactory(certificateName)){}
        private static HttpClientHandler HttpClientHandlerFactory(string? certificateName)
        {
            HttpClientHandler handler = new HttpClientHandler();
            if (!String.IsNullOrWhiteSpace(certificateName))
            {
                X509Certificate2? certificate2 = GetCertificateFromStore(certificateName);
                if (certificate2 != null)
                    handler.ClientCertificates.Add(certificate2);
            }
            return handler;
        }
        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            request.Headers
                .Add("X-messageID", Guid.NewGuid().ToString().Replace("-", String.Empty));
            request.Headers
                .Add("X-messageDT", DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss.ffffffZ"));
            return await base.SendAsync(request);
        }   
        private static X509Certificate2? GetCertificateFromStore(string certName)
        {
            // https://learn.microsoft.com/ru-ru/dotnet/api/system.security.cryptography.x509certificates.x509certificate2?view=netcore-3.0
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }
        }     
    }
}