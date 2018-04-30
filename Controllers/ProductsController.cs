using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLib;
using Model;
using System.Web;
using System.Net.Http;
using System.Net;

namespace ProductServiceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private ProductDataAccess dataAccess;
      
        public ProductsController()
        {
            dataAccess=new ProductDataAccess();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
            //dataAccess.AddProduct();
             return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public Product GetProductById(int Id)
        {            
            return dataAccess.GetProductById(Convert.ToString(Id));            
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage CreateProduct([FromBody] Product product)
        {
            dataAccess.AddProduct(product);
            return new HttpResponseMessage(HttpStatusCode.OK);
            
           // Request.CreateResponse(HttpStatusCode.OK,product);
        }

        [HttpGet]
        public List<dynamic> GetAllProducts()
        {
            return dataAccess.GetAllProduct();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
