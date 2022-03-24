using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Microsoft.Extensions.Configuration;
using DipGitApiLib;
using System.Text.Json;


namespace DipGitApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private RestClient _client;
        private string _accessKey;

        public ProductsController(IConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config.GetConnectionString("RestDB_Url"));
            _accessKey = _config.GetConnectionString("key");
        }
        /// <summary>
        /// Searches Products for the value of a specific field
        /// </summary>
        /// <param name="field">Name of field to search on</param>
        /// <param name="value">value of field</param>
        /// <returns>Object if found</returns>
        [HttpGet("{field}/{value}")]
        public async Task<IActionResult> SearchProduct(string field, string value)
        {
            string search = $"{{\"{field}\":\"{value}\"}}";

            var request = new RestRequest();
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", _accessKey);
            request.AddHeader("content-type", "application/json");
            request.AddQueryParameter("q", search);
            var response = _client.Get(request);

            if (response.Content.Contains("_id"))
            {
                return Ok(response.Content);
            }

            return NotFound();
        }

        /// <summary>
        /// Gets all Products and returns them
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = new RestClient("https://diplomagit-e6cc.restdb.io/rest/products");
            var request = new RestRequest()
            .AddHeader("cache-control", "no-cache")
            .AddHeader("x-apikey", "35ef07b4da07e33f8da131df3ef7b29b87d9e")
            .AddHeader("content-type", "application/json");
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.Content.Contains("_id"))
            {
                return Ok(response.Content);
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Add a new Product
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Product newProduct)
        {

            var body = JsonSerializer.Serialize(newProduct);

            var client = new RestClient("https://diplomagit-e6cc.restdb.io/rest/products");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "35ef07b4da07e33f8da131df3ef7b29b87d9e");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return Ok(response.Content);
        }

        /// <summary>
        /// Deletes a product based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = new RestClient($"https://diplomagit-e6cc.restdb.io/rest/products/{id}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "35ef07b4da07e33f8da131df3ef7b29b87d9e");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = await client.ExecuteAsync(request);

            return Ok(response.Content);
        }


        /// <summary>
        /// Returns the total qty of item from all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalQty")]
        public async Task<IActionResult> GetTotalQty()
        {
            // Read all products and create a Products object.  Use the products object to determine the total qty
            var client = new RestClient("https://diplomagit-e6cc.restdb.io/rest/products");
            var request = new RestRequest()
            .AddHeader("cache-control", "no-cache")
            .AddHeader("x-apikey", "35ef07b4da07e33f8da131df3ef7b29b87d9e")
            .AddHeader("content-type", "application/json");
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.Content.Contains("_id"))
            {
                var products = new Products();
                var responseContent = JsonSerializer.Deserialize<List<Product>>(response.Content);
                products.ProductList = responseContent;
                return Ok("Total Qty of All Products: " + products.GetTotalQtyProducts());
            }

            return NotFound();
        }

        /// <summary>
        /// Returns the total value of all item prices summed.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalValue")]
        public async Task<IActionResult> GetTotalValue()
        {
            // Read all products and create a Products object.  Use the products object to determine the total value
            var client = new RestClient("https://diplomagit-e6cc.restdb.io/rest/products");
            var request = new RestRequest()
            .AddHeader("cache-control", "no-cache")
            .AddHeader("x-apikey", "35ef07b4da07e33f8da131df3ef7b29b87d9e")
            .AddHeader("content-type", "application/json");
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.Content.Contains("_id"))
            {
                var products = new Products();
                var responseContent = JsonSerializer.Deserialize<List<Product>>(response.Content);
                products.ProductList = responseContent;
                return Ok("Total Value of All Products: " + products.GetTotalValueProducts());
            }

            return NotFound();
        }
    }
}
