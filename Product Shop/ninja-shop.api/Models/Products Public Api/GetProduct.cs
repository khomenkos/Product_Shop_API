using System;
using RestSharp;
namespace Product_Shop.api.Models.ProductsPublicApi
{

    public class ProductSearch
    {
        public string packagain { get; set; }

        public string superproduct_id { get; set; }

        public string brand { get; set; }

        public string name { get; set; }
    }

    public class GetProduct
    {
        public GetProduct()
        {
            var client = new RestClient("https://datagram-products-v1.p.rapidapi.com/storeproduct/search/?q=coca%20cola");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "datagram-products-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "a43a52ca40msha4bb5e26dde474bp187e28jsn3ce4594ab462");
            IRestResponse response = client.Execute(request);
        }
    }
}
