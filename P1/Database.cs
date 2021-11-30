using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace P1
{
    /**
     * This static class should be a provider for an SQLite db
     * Make sure the package is included in your project
     * 
     * 
     * Run the following to install the package in your project directory :
     *      
     *          dotnet add package Microsoft.Data.Sqlite
     * 
     * Read documentation for Microsoft.Data.Sqlite
     * and complete the following functions.
     * 
     * 
     * Note "this" within the function parameter is called a method extension
     * Read : https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
     */
    static class Database
    {
        public static SqliteConnection CreateConnection()
        {
            return new SqliteConnection("Data Source=product.db");
        }

        public static void CreateTable(this SqliteConnection conn)
        {
            var table = conn.CreateCommand();
            table.CommandText = "CREATE TABLE Product(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name varchar(50), description varchar(255), colour varchar(30), price decimal(20, 2));";
            table.ExecuteNonQuery();
        }

        public static void DeleteProduct(this SqliteConnection conn, int id)
        {
            var delete = conn.CreateCommand();
            delete.CommandText = "DELETE FROM Product WHERE id = @id;";
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
        }

        public static Product GetProduct(this SqliteConnection conn, int id)
        {
            var select = conn.CreateCommand();
            select.CommandText = "SELECT * FROM Product WHERE id = @id;";
            select.Parameters.AddWithValue("@id", id);

            using (var reader = select.ExecuteReader())
            {
                Product product = new Product();
                while (reader.Read())
                {
                    var tempId = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var description = reader.GetString(2);
                    var colour = reader.GetString(3);
                    var price = reader.GetDecimal(4);

                    product.Id = tempId;
                    product.Name = name;
                    product.Description = description;
                    product.Colour = colour;
                    product.Price = price;

                }
                return product;
            }
        }

        public static void InsertProducts(this SqliteConnection conn, IEnumerable<Product> products)
        {

            foreach (Product product in products)
            {
                var insert = conn.CreateCommand();
                insert.CommandText = "INSERT INTO Product ('name', 'description', 'colour', 'price') VALUES (@name, @description, @colour, @price)";

                insert.Parameters.AddWithValue("@name", product.Name);
                insert.Parameters.AddWithValue("@description", product.Description);
                insert.Parameters.AddWithValue("@colour", product.Colour);
                insert.Parameters.AddWithValue("@price", product.Price);

                insert.ExecuteNonQuery();

            }

        }

        public static IEnumerable<Product> ReadProducts(this SqliteConnection conn)
        {
            var select = conn.CreateCommand();
            select.CommandText = "SELECT * FROM Product;";
            using (var reader = select.ExecuteReader())
            {
                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var description = reader.GetString(2);
                    var colour = reader.GetString(3);
                    var price = reader.GetDecimal(4);

                    Product product = new Product(name, description, colour, price);
                    product.Id = id;
                    products.Add(product);
                }
                return products;
            }
        }
    }
}
