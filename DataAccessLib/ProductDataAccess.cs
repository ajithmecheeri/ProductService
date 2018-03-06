using System;

using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client; 
using System.Collections.Generic;
using Model;

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
        
    }
}
