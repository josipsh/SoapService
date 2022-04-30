using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SoapService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {

            //napravi rest api koji dohvaca sve podatke iz baze u xml
            //sortiraj u logickom smislu (Asset class i u svakom asset klasi svi prodai i kupljeni resordi)
            //izvuci potrebne podatke iz xml-a
            //napravi logiku
            return "Hello World";
        }
    }
}
