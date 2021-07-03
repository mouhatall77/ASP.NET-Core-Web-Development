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
            int newIdNumber = -1;


            //Prepared Sql statement where @name stand for any other item
            string sqlStatement = "DELETE FROM dbo.Products WHERE Id = @Id";

            //Section where we use connection to our sql database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a command object. Sql command takes 2 params
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", product.Id);


                try
                {

                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return newIdNumber;
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
            //
            ProductModel foundProduct = null;

            //Prepared Sql statement where @name stand for any other item
            string sqlStatement = "SELECT * FROM dbo.Products WHERE Id = @Id";

            //Section where we use connection to our sql database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a command object. Sql command takes 2 params
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Search term with wildcard before and after the term
                command.Parameters.AddWithValue("@Id", id);


                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProduct = new ProductModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        };
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return foundProduct;
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
            int newIdNumber = -1;


            //Prepared Sql statement where @name stand for any other item
            string sqlStatement = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";

            //Section where we use connection to our sql database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a command object. Sql command takes 2 params
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Search term with wildcard before and after the term
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Id", product.Id);


                try
                {

                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return newIdNumber;
        }
    }
}
