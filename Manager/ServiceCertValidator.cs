using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Manager
{
	public class ServiceCertValidator : X509CertificateValidator
	{
	    public override void Validate(X509Certificate2 certificate)
		{

           // bool test = true;

            //throw new NotImplementedException();
            //da li su nam isti izdavaoci kod klijenta i serverw
            string srvCertCN = Formatter1.ParseName(WindowsIdentity.GetCurrent().Name); // vraca ime zorana/wcfclient ili servoce

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            //ukoliko sertifikat koji validiramo je jednak sa issuerom naseg serverskog sertifikata - provera - ako nisu jednaki baciti gresku

            if(!certificate.Issuer.Equals(srvCert.Issuer))
            {
                //test =false;
                throw new Exception("Certificate is not from valid issuer");
            }
            
           // if (test)
           // {
           //     Audit.AuthenticationSuccess(Formatter1.ParseName(certificate.SubjectName.Name));
           // }
           // else
           // {
           //     Audit.AuthenticationFailed(Formatter1.ParseName(certificate.SubjectName.Name));
           // }
             
             

        }
    }
}
