namespace LukoilExpedition.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    class Program
    {
        static async Task Main()
        {
            string baseUrl = "https://192.168.213.112:15001";

            // Bypass the certificate for test server remove this code in production
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            // Bypass the certificate for test server 

            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Change UserName and Password !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var userModel = new AuthenticateModel {UserName = "YourUserName", Password = "YourPassword"};

            string stringData = JsonConvert.SerializeObject(userModel);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("/Users/authenticate", contentData);
            var responsePayload = await response.Content.ReadAsStringAsync();

            // Here we have JWT token
            var jwtObject = JsonConvert.DeserializeObject<JwtModel>(responsePayload);

            // start data query
            // Adding authorization header
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtObject.Jwt}");

            // Make a new http request
            response = await client.GetAsync("/ExternalApi/Customers");
            responsePayload = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Customer>>(responsePayload);

            foreach (var customer in result)
            {
                Console.WriteLine(customer);
            }


        }
    }
}
