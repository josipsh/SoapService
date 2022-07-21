using System;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Xml;
using System.Xml.XPath;

namespace SoapService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapService : System.Web.Services.WebService
    {
        private readonly ReportClient _client;

        public SoapService()
        {
            _client = new ReportClient("http://localhost:5243");
        }

        [WebMethod]
        public string CalculateCurrentPortfolioValue(int userId, string assetSymbol)
        {
            try
            {
                Stream reportStream = _client.FetchReportForUser(userId);

                XPathDocument xpDoc = new XPathDocument(reportStream);
                XPathNavigator nav = xpDoc.CreateNavigator();

                float boughtAssetsAmount = GetBoughtAssetAmount(nav, assetSymbol);
                float soldAssetsAmount = GetSoldAssetAmount(nav, assetSymbol);

                GetPriceData(nav, assetSymbol, out float priceRate, out string priceDate, out string currencySymbol);
                string xmlResponse = GenerateXmlResponse(boughtAssetsAmount, soldAssetsAmount, priceRate, priceDate, currencySymbol);
                SaveXmlResponse(xmlResponse);
                return xmlResponse;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private float GetBoughtAssetAmount(XPathNavigator nav, string assetSymbol)
        {
            float res = 0.0f;
            string getBoughtAssetExpression = $"//ReportReadDto[Asset/Symbol='{assetSymbol}']//BoughtAssetReadDto/Amount";
            XPathNodeIterator boughtAssetsXPath = nav.Select(getBoughtAssetExpression);

            while (boughtAssetsXPath.MoveNext())
            {
                res += float.Parse(boughtAssetsXPath.Current.InnerXml);
            }

            return res;
        }

        private float GetSoldAssetAmount(XPathNavigator nav, string assetSymbol)
        {
            float res = 0.0f;
            string getSoldAssetExpression = $"//ReportReadDto[Asset/Symbol='{assetSymbol}']//SoldAssetReadDto/Amount";
            XPathNodeIterator soldAssetsXPath = nav.Select(getSoldAssetExpression);

            while (soldAssetsXPath.MoveNext())
            {
                res += float.Parse(soldAssetsXPath.Current.InnerXml);
            }

            return res;
        }

        private void GetPriceData(
            XPathNavigator nav,
            string assetSymbol,
            out float priceRate,
            out string priceDate,
            out string currencySymbol)
        {
            string getCurrentPriceExpression = $"//ReportReadDto[Asset/Symbol='{assetSymbol}']//CurrentPrice";
            XPathNodeIterator currentPriceXPath = nav.Select(getCurrentPriceExpression);

            if (currentPriceXPath.MoveNext())
            {
                XPathNodeIterator priceRateXPath = currentPriceXPath.Current.Select("//PriceRate");
                if (priceRateXPath.MoveNext())
                {
                    priceRate = float.Parse(priceRateXPath.Current.InnerXml);
                }
                else
                {
                    priceRate = 0;
                    throw new Exception("Unable to get price rate");
                }

                XPathNodeIterator priceDateXPath = currentPriceXPath.Current.Select("//PriceDate");
                if (priceDateXPath.MoveNext())
                {
                    priceDate = priceDateXPath.Current.InnerXml;
                }
                else
                {
                    priceDate = "";
                    throw new Exception("Unable to get price date");
                }

                XPathNodeIterator currencySymbolXPath = currentPriceXPath.Current.Select("//Currency/Symbol");
                if (currencySymbolXPath.MoveNext())
                {
                    currencySymbol = currencySymbolXPath.Current.InnerXml;
                }
                else
                {
                    currencySymbol = "";
                    throw new Exception("Unable to get currency symbol");
                }

                return;
            }

            throw new Exception("Unable to get currenct price");

        }

        private static string GenerateXmlResponse(
            float boughtAssetsAmount,
            float soldAssetsAmount,
            float priceRate,
            string priceDate,
            string currencySymbol)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<Report>");

            sb.Append("<CurrentAmount>");
            sb.Append(boughtAssetsAmount - soldAssetsAmount);
            sb.Append("</CurrentAmount>");

            sb.Append("<CurrentAssetPortofioValue>");
            sb.Append((boughtAssetsAmount - soldAssetsAmount) * priceRate);
            sb.Append("</CurrentAssetPortofioValue>");

            sb.Append("<AssetSymbol>");
            sb.Append("BTC");
            sb.Append("</AssetSymbol>");

            sb.Append("<CurrentPriceRate>");
            sb.Append(priceRate);
            sb.Append("</CurrentPriceRate>");

            sb.Append("<CurrentPriceDate>");
            sb.Append(priceDate);
            sb.Append("</CurrentPriceDate>");

            sb.Append("<CurrentPriceSymbol>");
            sb.Append(currencySymbol);
            sb.Append("</CurrentPriceSymbol>");

            sb.Append("</Report>");

            return sb.ToString();
        }

        private void SaveXmlResponse(string xmlResponse)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlResponse);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            xmlDocument.Save($"{path}/XmlResponse.xml");
        }
    }
}
