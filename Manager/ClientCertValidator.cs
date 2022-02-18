using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace Manager
{
	public class ClientCertValidator : X509CertificateValidator
	{
		/// <summary>
		/// Implementation of a custom certificate validation on the client side.
		/// Client should consider certificate valid if the given certifiate is not self-signed.
		/// If validation fails, throw an exception with an adequate message.
		/// </summary>
		/// <param name="certificate"> certificate to be validate </param>
		public override void Validate(X509Certificate2 certificate)
		{
           // bool test = true;


            if (!certificate.Subject.Equals(certificate.Issuer))
            {//ukoliko je on sam sebi izdao sertifikat baciti gresku (zadatak sa vezbi)
                //sevisni sertifikat je validan ukoliko nije self-signed
                //klijent validira serverski sertifikat


                //test = false;
                throw new Exception("Certificate is not self-signed");

            }

            /*
            if (test)
            {
                try
                {
                    Audit.AuthenticationSuccess(Formatter1.ParseName(certificate.SubjectName.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthenticationFailed(Formatter1.ParseName(certificate.SubjectName.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
             
             */

        }
    }
}
