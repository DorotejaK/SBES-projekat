using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Manager
{
	public class CertManager
	{
		/// <summary>
		/// Get a certificate with the specified subject name from the predefined certificate storage
		/// Only valid certificates should be considered
		/// </summary>
		/// <param name="storeName"></param>
		/// <param name="storeLocation"></param>
		/// <param name="subjectName"></param>
		/// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
		public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
		{
            //4.1 - 1
            //X509Certificate2 certificate = null;
            X509Store store = new X509Store(storeName, storeLocation); ///kreiramo stor
            //otvaramo store
            store.Open(OpenFlags.ReadOnly); //hocemo samo da ga citamo

            //kreiramo novu kolekciju sertifikata
            X509Certificate2Collection certCollection =
                store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true); //vratiti samo validne sertifikate u spisak

            foreach(X509Certificate2 c in certCollection)
            {
                if (c.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
                {
                    return c; //kad kliknemo na cer u diteails subject videcemo da je cn=... po tome znamo da je to bas taj sertifiakt
                }
            }
            return null;

		}


		/// <summary>
		/// Get a certificate from the specified .pfx file		
		/// </summary>
		/// <param name="fileName"> .pfx file name </param>
		/// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
		public static X509Certificate2 GetCertificateFromFile(string fileName)
		{
			X509Certificate2 certificate = null;

			return certificate;
		}

        //CEMU SLUZI OVA GRUPA 
    /*    public static X509Certificate2 GetCertificateFromFile(string fileName, SecureString pwd)
        {
            X509Certificate2 certificate = null;


            return certificate;
        }
        public static string GetUserGroup(X509Certificate2 cert)
        {
            string[] parts = cert.SubjectName.Name.Split('=');
            return parts[2];

        }
        */
    }
}
