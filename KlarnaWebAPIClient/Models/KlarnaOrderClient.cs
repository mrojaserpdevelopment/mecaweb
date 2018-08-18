using KlarnaWebAPIClient.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace KlarnaWebAPIClient.Models
{
    public class KlarnaOrderClient
    {
        private HttpClient client;

        public KlarnaOrderClient()
        {
            Props config = new Props("ConfigClient.txt");            
            client = new HttpClient();            
            client.BaseAddress = new Uri(config.get("api_url", ""));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<KlarnaOrder> FindAll()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("klarnaorder").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<KlarnaOrder>>().Result;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public KlarnaOrder Find(string id)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("klarnaorder/" + id).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<KlarnaOrder>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(KlarnaOrder order)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("klarnaorder", order).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(KlarnaOrder order)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("klarnaorder/" + order.KlarnaID, order).Result;                
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("customers/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }  
    }
}