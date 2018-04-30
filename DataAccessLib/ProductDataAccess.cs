using System;

using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client; 
using System.Collections.Generic;
using Model;
using System.Linq;


namespace DataAccessLib
{

    public class ProductDataAccess
    {
        Cluster cluster = new Cluster(new ClientConfiguration 
        { 
        Servers = new List<Uri> { new Uri("http://88.88.88.1") }
        });

        public ProductDataAccess()
        {
            var authenticator = new PasswordAuthenticator("Administrator", "Tesco123");
            cluster.Authenticate(authenticator); 
        }


        public bool AddProduct( Product product)
        {
            using (var bucket = cluster.OpenBucket("Product"))
            {
                var document = new Document<dynamic>
                {
                   
                    Id = Convert.ToString(product.ProductNo),
                    Content = new
                    {
                        ProductNo=product.ProductNo,
                        Name = product.Name,
                        LongDescription = product.LongDescription,
                        ShortDescription = product.ShortDescription,
                        Image = product.Image,


                    }
                };

                var upsert = bucket.Upsert(document);
                return upsert.Success;
                
            }

        } 


        public List<dynamic> GetAllProduct()
        {
            List<dynamic> productList = null;
            using (var bucket = cluster.OpenBucket("Product"))
            {
                var query = "SELECT * FROM Product";
                var result = bucket.Query<dynamic>(query);
                if(result.Success)
                {
                    productList = result.Rows;
                }
            }

            return productList;
        }


        public Product GetProductById(string id)
        {
            Product product = null;
            using(var bucket = cluster.OpenBucket("Product"))
            {
                var result = bucket.GetDocument<Product>(id);
                product = result.Content;
            }

            return product;
        }
        
    }
}
