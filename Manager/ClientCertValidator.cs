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

		public override void Validate(X509Certificate2 certificate)
		{
           // bool test = true;


            if (!certificate.Subject.Equals(certificate.Issuer))
            {


              //  test = false;
                throw new Exception("Certificate is not self-signed");

            }

            
         //  if (test)
         //  {
         //      try
         //      {
         //          Audit.AuthenticationSuccess(Formatter1.ParseName(certificate.SubjectName.Name));
         //      }
         //      catch (Exception e)
         //      {
         //          Console.WriteLine(e.Message);
         //      }
         //  }
         //  else
         //  {
         //      try
         //      {
         //          Audit.AuthenticationFailed(Formatter1.ParseName(certificate.SubjectName.Name));
         //      }
         //      catch (Exception e)
         //      {
         //          Console.WriteLine(e.Message);
         //      }
         //  }
             
             

        }
    }
}
