// ------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Business Management System Ltd.">
//      Copyright "2020" (c), Business Management System Ltd.
//      All rights reserved.
//  </copyright>
//  <author>Nikolay.Kostadinov</author>
// ------------------------------------------------------------------------------------------------

namespace LukoilExpedition.ConsoleClient
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    #endregion

    internal class Program
    {
        private static async Task Main()
        {
            var baseUrl = "https://192.168.213.112:15001";

            // Bypass the certificate for test server remove this code in production
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            // Bypass the certificate for test server 

            var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Change UserName and Password !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var userModel = new AuthenticateModel {UserName = "Administrator", Password = "P@ssw0rd"};

            var stringData = JsonConvert.SerializeObject(userModel);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Users/authenticate", contentData);
            var responsePayload = await response.Content.ReadAsStringAsync();

            // Here we have JWT token
            var jwtObject = JsonConvert.DeserializeObject<JwtModel>(responsePayload);

            // start data query
            // Adding authorization header
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtObject.Jwt}");

            // Make a new http request
            contentData = new StringContent(Message, Encoding.UTF8, "application/json");
            response = await client.PostAsync("/ExternalApi/LoadResults", contentData);
            responsePayload = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"The Id of the created result record is: {responsePayload}");
        }

        private static string Message => "{\"orderId\":8,\"rows\":[{\"transactionNumber\":253,\"measuringPoint\":\"001104\",\"productCode\":\"049\",\"grossObservableVolume\":1000,\"grossStandardVolume\":1020,\"mass\":900,\"averageObservableDensity\":785.2,\"averageReferenceDensity\":745.8,\"averageTemperature\":18.3,\"transactionBeginTime\":\"2020-05-20T07:47:52.084Z\",\"transactionEndTime\":\"2020-05-20T07:47:52.084Z\",\"additiveGrossObservableVolume\":0,\"additiveMass\":0,\"additiveAverageObservableDensity\":0,\"additiveAverageReferenceDensity\":0,\"additiveAverageTemperature\":0,\"markerGrossStandardVolume\":0,\"markerGrossObservableVolume\":0,\"markerMass\":0,\"markerAverageObservableDensity\":0,\"markerAverageReferenceDensity\":0,\"markerAverageTemperature\":0,\"totalizerBeginGrossObservableVolume\":123456,\"totalizerEndGrossObservableVolume\":1234567,\"totalizerBeginGrossStandartVolume\":45697,\"totalizerEndGrossStandartVolume\":45689,\"totalizerBeginMass\":58963,\"totalizerEndMass\":123568,\"id\":115},{\"transactionNumber\":254,\"measuringPoint\":\"001104\",\"productCode\":\"049\",\"grossObservableVolume\":50,\"grossStandardVolume\":20,\"mass\":10,\"averageObservableDensity\":785.2,\"averageReferenceDensity\":745.8,\"averageTemperature\":18.3,\"transactionBeginTime\":\"2020-05-20T07:47:52.084Z\",\"transactionEndTime\":\"2020-05-20T07:47:52.084Z\",\"additiveGrossObservableVolume\":0,\"additiveMass\":0,\"additiveAverageObservableDensity\":0,\"additiveAverageReferenceDensity\":0,\"additiveAverageTemperature\":0,\"markerGrossStandardVolume\":0,\"markerGrossObservableVolume\":0,\"markerMass\":0,\"markerAverageObservableDensity\":0,\"markerAverageReferenceDensity\":0,\"markerAverageTemperature\":0,\"totalizerBeginGrossObservableVolume\":123456,\"totalizerEndGrossObservableVolume\":1234567,\"totalizerBeginGrossStandartVolume\":45697,\"totalizerEndGrossStandartVolume\":45689,\"totalizerBeginMass\":58963,\"totalizerEndMass\":123568,\"id\":116}],\"grossObservableVolume\":1050,\"grossStandardVolume\":1040,\"mass\":910,\"averageObservableDensity\":785.2,\"averageReferenceDensity\":745.8,\"averageTemperature\":18.3,\"additiveGrossObservableVolume\":0,\"additiveGrossStandardVolume\":0,\"additiveMass\":0,\"additiveAverageObservableDensity\":0,\"additiveAverageReferenceDensity\":0,\"additiveAverageTemperature\":0,\"markerGrossObservableVolume\":0,\"markerGrossStandardVolume\":0,\"markerMass\":0,\"markerAverageObservableDensity\":0,\"markerAverageReferenceDensity\":0,\"markerAverageTemperature\":0,\"id\":8,\"isValid\":true,\"modifiedOn\":\"2020-05-20T07:47:52.084Z\"}";
    }
}