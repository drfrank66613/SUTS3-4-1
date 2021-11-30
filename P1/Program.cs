using System;
using System.Collections.Generic;

namespace P1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("This is the program for L1.");

            // using (var dbConn = Database.CreateConnection())
            // {
            //     dbConn.Open();

            //     dbConn.CreateTable();

            //     List<Product> products = new List<Product>();

            //     products.Add(new Product("Laptop", "Fastest Gaming Laptop", "Black", 1000.00m));
            //     products.Add(new Product("Drawing Book", "Express Your Imagination", "Rainbow", 10.00m));
            //     products.Add(new Product("Bag", "Carry Your Stuff Anywhere", "Navy BLue", 150.20m));
            //     products.Add(new Product("Mouse", "RGB Wireless Mouse", "White", 80.50m));


            //     using (var transaction = dbConn.BeginTransaction())
            //     {
            //         dbConn.InsertProducts(products);

            //         transaction.Commit();
            //         products.Clear();
            //     }

            //     IEnumerable<Product> readProducts = dbConn.ReadProducts();

            //     int index = 0;
            //     foreach (var p in readProducts)
            //     {
            //         index++;
            //         Console.WriteLine("Product " + index);
            //         Console.WriteLine(p.ToString());
            //         Console.WriteLine("\n=======================\n");
            //     }
            //     Console.ReadLine();
            // }

            var done = false;
            while (!done)
            {
                Console.WriteLine("These are options:");
                Console.WriteLine("1. Add new product\n2. Print all product\n3. Get a product\n4. Delete a product\n5. Quit Program");
                var input = Console.ReadLine();
                Console.WriteLine("");

                using (var dbConn = Database.CreateConnection())
                {
                    dbConn.Open();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Fill in the product attributes below:");
                            Console.Write("Product Name: ");
                            var productName = Console.ReadLine();
                            Console.Write("Product Description: ");
                            var productDescription = Console.ReadLine();
                            Console.Write("Product Colour: ");
                            var productColour = Console.ReadLine();
                            Console.Write("Product Price: ");
                            var productPrice = Convert.ToDecimal(Console.ReadLine());

                            List<Product> products = new List<Product>();
                            products.Add(new Product(productName, productDescription, productColour, productPrice));

                            using (var transaction = dbConn.BeginTransaction())
                            {
                                dbConn.InsertProducts(products);

                                transaction.Commit();
                                products.Clear();
                            }

                            Console.WriteLine("Your product has been added successfully, select option 2 to see your products\n\n");
                            break;
                        case "2":
                            IEnumerable<Product> readProducts = dbConn.ReadProducts();

                            int index = 0;
                            foreach (var p in readProducts)
                            {
                                index++;
                                Console.WriteLine("Product " + index);
                                Console.WriteLine(p.ToString());
                                Console.WriteLine("\n=======================\n");
                            }
                            break;
                        case "3":
                            Console.Write("Key in the product id to get the specific product: ");
                            var getId = Convert.ToInt32(Console.ReadLine());
                            Product product = dbConn.GetProduct(getId);
                            Console.WriteLine(product.ToString());
                            Console.WriteLine("\n=======================\n");
                            break;
                        case "4":
                            Console.Write("Key in the product id to delete the specific product: ");
                            var deleteId = Convert.ToInt32(Console.ReadLine());
                            dbConn.DeleteProduct(deleteId);
                            Console.WriteLine("Your product has been deleted, select option 2 to see your remaining products\n\n");
                            break;
                        case "5":
                            done = true;
                            break;
                    }
                }

            }




            /**
             * Section to be modified
             */


            // Add a new products here
            /*
            products.Add(new Product( ... ));
            products.Add(new Product(... ));
            products.Add(new Product(... ));
            products.Add(new Product(... ));
            */


            /**
             * Add User interaction function here LT4.4
             */
            // while(!done)
            //{
            //  var input = Console...
            //  switch(input)
            //  {
            //  }
            //}
            //
            //
        }
    }
}
