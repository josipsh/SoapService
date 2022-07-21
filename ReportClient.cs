using SoapService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace SoapService
{
    public class ReportClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public ReportClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        public Stream FetchReportForUser(int userId)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{_baseUrl}/api/report/fullreport/{userId}");
            //request.Method = "GET";
            //request.Accept = "application/xml";

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    return response.GetResponseStream();
            //}

            //throw new Exception("Unable to fetch report");

            XmlDocument xmlDoc = new XmlDocument();
            MemoryStream stream = new MemoryStream();

            xmlDoc.LoadXml(xml);
            xmlDoc.Save(stream);
            stream.Position = 0;
            return stream;
        }

        private string xml = $"<ArrayOfReportReadDto xmlns:i={@"""http://www.w3.org/2001/XMLSchema-instance"""} >    <ReportReadDto>        <Asset>            <Id>1</Id>            <Name>Bitcoin</Name>            <Symbol>BTC</Symbol>            <AssetType>Crypto</AssetType>        </Asset>        <CurrentPrice>            <Id>0</Id>            <Currency>                <Id>1</Id>                <Name>Dolar</Name>                <Symbol>USD</Symbol>            </Currency>            <PriceRate>23153.41</PriceRate>            <PriceDate>2022-07-21T00:00:00</PriceDate>        </CurrentPrice>        <BoughtAssets>            <BoughtAssetReadDto>                <Id>3</Id>                <BuyDate>2022-07-20T03:07:54.852</BuyDate>                <Amount>60</Amount>                <PriceId>1</PriceId>                <AssetId>1</AssetId>                <Price>                    <Id>1</Id>                    <Currency>                        <Id>1</Id>                        <Name>Dolar</Name>                        <Symbol>USD</Symbol>                    </Currency>                    <PriceRate>20000</PriceRate>                    <PriceDate>2022-07-20T03:07:54.852</PriceDate>                </Price>                <Asset>                    <Id>1</Id>                    <Name>Bitcoin</Name>                    <Symbol>BTC</Symbol>                    <AssetType>Crypto</AssetType>                </Asset>            </BoughtAssetReadDto>            <BoughtAssetReadDto>                <Id>4</Id>                <BuyDate>2022-07-20T03:07:54.852</BuyDate>                <Amount>30</Amount>                <PriceId>2</PriceId>                <AssetId>1</AssetId>                <Price>                    <Id>2</Id>                    <Currency>                        <Id>1</Id>                        <Name>Dolar</Name>                        <Symbol>USD</Symbol>                    </Currency>                    <PriceRate>20000</PriceRate>                    <PriceDate>2022-07-20T03:07:54.852</PriceDate>                </Price>                <Asset>                    <Id>1</Id>                    <Name>Bitcoin</Name>                    <Symbol>BTC</Symbol>                    <AssetType>Crypto</AssetType>                </Asset>            </BoughtAssetReadDto>        </BoughtAssets>        <SoldAsstes>            <SoldAssetReadDto>                <Id>2</Id>                <SellDate>2022-07-20T03:09:16.706</SellDate>                <Amount>50</Amount>                <PriceId>7</PriceId>                <AssetId>1</AssetId>                <Price>                    <Id>7</Id>                    <Currency>                        <Id>1</Id>                        <Name>Dolar</Name>                        <Symbol>USD</Symbol>                    </Currency>                    <PriceRate>25000</PriceRate>                    <PriceDate>2022-07-20T03:09:16.706</PriceDate>                </Price>                <Asset>                    <Id>1</Id>                    <Name>Bitcoin</Name>                    <Symbol>BTC</Symbol>                    <AssetType>Crypto</AssetType>                </Asset>            </SoldAssetReadDto>            <SoldAssetReadDto>                <Id>3</Id>                <SellDate>2022-07-20T05:10:29.565</SellDate>                <Amount>10</Amount>                <PriceId>14</PriceId>                <AssetId>1</AssetId>                <Price>                    <Id>14</Id>                    <Currency>                        <Id>1</Id>                        <Name>Dolar</Name>                        <Symbol>USD</Symbol>                    </Currency>                    <PriceRate>1500</PriceRate>                    <PriceDate>2022-07-20T05:10:29.565</PriceDate>                </Price>                <Asset>                    <Id>1</Id>                    <Name>Bitcoin</Name>                    <Symbol>BTC</Symbol>                    <AssetType>Crypto</AssetType>                </Asset>            </SoldAssetReadDto>        </SoldAsstes>    </ReportReadDto></ArrayOfReportReadDto>";
    }
}