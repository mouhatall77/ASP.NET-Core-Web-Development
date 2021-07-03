using Activity2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Activity2.Services
{
    public class ProductsDAO : IProductDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int Delete(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetAllProducts()
        {
            //Create empty list of type product model
            List<ProductModel> foundProducts = new List<ProductModel>();

            //Sql stqtement to get all product
            string sqlStatement = "SELECT * FROM dbo.Products";

            //Section where we use connection to our sql database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a command object. Sql command takes 2 params
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0],
                            Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return foundProducts;

        }

        public ProductModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            //Create empty list of type product model
            List<ProductModel> foundProducts = new List<ProductModel>();

            //Prepared Sql statement where @name stand for any other item
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";

            //Section where we use connection to our sql database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a command object. Sql command takes 2 params
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Search term with wildcard before and after the term
                command.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');


                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        });
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return foundProducts;
        }

        public int Update(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
