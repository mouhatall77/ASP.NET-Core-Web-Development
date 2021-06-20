using RegisterAndLoginApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterAndLoginApp.Services
{
    public class UsersDAO
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username AND password = @password";

            using(SqlConnection connection = new SqlConnection(connectionString))   //Generate block code and autoatically close it when done
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);  //sql statement to send and connection that we want to connect to

                //The parameters defined with @, specify what kind of variable (Varchar), length, 
                //and set the value to what came in in the user object == user.Username/Password
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;   
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;   


                try
                {
                    connection.Open(); //Initiate connection to db
                    SqlDataReader reader = command.ExecuteReader(); //Define new obj

                    if (reader.HasRows) // == db is not empty
                    {
                        success = true;
                    }

                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return success;
            // return true if found in the db.
        }
    }
}
