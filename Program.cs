using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace NathansBookStore
{
    internal class Program
    {
        static string connectionString = "server=localhost;port=3306;user=console_app_user;password=wh8tB678dbJgt6HJ6!kG67;database=my_guitar_shop";

        static MySqlConnection connection = new MySqlConnection(connectionString);

        static void Main(string[] args)
        {
            DisplayMenu();
            //InsertProduct(1, "EXAMPLE1", "Example 1", "Description 1", 20m, 10m, DateTime.Now);


        }

        static void DisplayMenu()
        {
            // Clear the console
            Console.Clear();

            // Set the console text color
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Display the title of the application in a box
            Console.WriteLine("\t╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║                  Welcome to Nathan's Guitar Shop               ║");
            Console.WriteLine("\t╚════════════════════════════════════════════════════════════════╝\n");

            // Reset the console text color
            Console.ResetColor();

            // Display a brief description of the application
            Console.WriteLine("\tWelcome to the shop application that has been designed to help you manage all");
            Console.WriteLine("\taspects of the guitar shop. From managing products and categories to handling");
            Console.WriteLine("\tcustomer information and processing orders, this application provides a");
            Console.WriteLine("\tuser-friendly interface for all your shop management needs.\n\n");

            // Display a series of items for the user to choose from
            Console.WriteLine("\tPlease choose a section from the main menu below:\n");
            Console.WriteLine("\t\t1. Customers");
            Console.WriteLine("\t\t2. Products");
            Console.WriteLine("\t\t3. Categories");
            Console.WriteLine("\t\t4. Orders");
            Console.WriteLine("\t\t5. Exit\n");

            // Validate the user's choice
            int choice;
            do
            {
                Console.Write("\tEnter your choice: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5);

                if (choice == 1)
            {
                //Display Customers Menu.
            }
            else if (choice == 2)
            {
                //Display Products Menu.
                DisplayProductsMenu();
            }
            else if (choice == 3)
            {
                //Display Categories Menu.
            }
            else if (choice == 4)
            {
                //Display Orders Menu.
            }
            else if (choice == 5)
            {
                Environment.Exit(0);
            }

        }

        static void DisplayProductsMenu()
        {
            // Clear the console
            Console.Clear();

            // Set the console text color
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Display the title of the application in a box
            Console.WriteLine("\t╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║                  Nathan's Guitar Shop - Products               ║");
            Console.WriteLine("\t╚════════════════════════════════════════════════════════════════╝\n");

            // Reset the console text color
            Console.ResetColor();

            // Display a series of items for the user to choose from
            Console.WriteLine("\tPlease choose an option from the menu below:\n");
            Console.WriteLine("\t\t1. View");
            Console.WriteLine("\t\t2. Add");
            Console.WriteLine("\t\t3. Update");
            Console.WriteLine("\t\t4. Delete");
            Console.WriteLine("\t\t5. Back to Main Menu\n");

            // Prompt for the user where they can indicate their response
            Console.Write("\tEnter your choice: ");

            // Validate the user's choice
            int choice;
            do
            {
                Console.Write("\tEnter your choice: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5);

            if (choice == 1)
            {
                //View Products.
                DisplayProductasMenu();
            }
            else if (choice == 2)
            {
                //Add Product.
                AddProduct();
            }
            else if (choice == 3)
            {
                //Update Product.
            }
            else if (choice == 4)
            {
                //Delete Product.
            }
            else if (choice == 5)
            {
                //Back to Main Menu
                DisplayMenu();
            }
        }

        static void DisplayProductasMenu()
        {
            string selectProducts = @"SELECT
	                            product_id
                                , category_id
	                            , product_code
	                            , product_name
	                            , list_price
                            FROM
	                            products;";

            var products = Select(selectProducts);

            // Clear the console
            Console.Clear();

            // Set the console text color
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Display the title of the application in a box
            Console.WriteLine("\t╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t║              Nathan's Guitar Shop - Products - View            ║");
            Console.WriteLine("\t╚════════════════════════════════════════════════════════════════╝\n\n");

            Console.WriteLine("\tproduct_id\tcategory_id\tlist_price\tproduct_code\tproduct_name");

            // Reset the console text color
            Console.ResetColor();


            foreach (DataRow product in products.Rows)
            {
                Console.WriteLine($"\t{product["product_id"].ToString().PadRight(10)}\t{product["category_id"].ToString().PadRight(10)}\t{product["list_price"].ToString().PadRight(10)}\t{product["product_code"].ToString().PadRight(10)}\t{product["product_name"].ToString().PadRight(10)}");
            }

            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadLine();

            DisplayProductsMenu();
        }

        static void AddProduct()
        {
            Console.Write("\n\tEnter the category ID: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n\tEnter the product code: ");
            string productCode = Console.ReadLine();

            Console.Write("\n\tEnter the product name: ");
            string productName = Console.ReadLine();

            Console.Write("\n\tEnter the description: ");
            string description = Console.ReadLine();

            Console.Write("\n\tEnter the list price: ");
            decimal listPrice = Convert.ToDecimal(Console.ReadLine());

            Console.Write("\n\tEnter the discount percent: ");
            decimal discountPercent = Convert.ToDecimal(Console.ReadLine());

            Console.Write("\n\tEnter the date added (yyyy-MM-dd): ");
            DateTime dateAdded = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            int result = InsertProduct(categoryId, productCode, productName, description, listPrice, discountPercent, dateAdded);

            if (result > 0)
            {
                Console.WriteLine("\n\n\tProduct added successfully.");
            }
            else
            {
                Console.WriteLine("\n\n\tAn error occurred while adding the product.");
            }

            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadLine();

            DisplayProductasMenu();
        }

        static int InsertProduct(int categoryId, string productCode, string productName, string description, decimal listPrice, decimal discountPercent, DateTime dateAdded)
        {

            string insertQuery = @"INSERT INTO products
                        (category_id,
                        product_code,
                        product_name,
                        description,
                        list_price,
                        discount_percent,
                        date_added)
                        VALUES
                        (@category_id,
                        @product_code,
                        @product_name,
                        @description,
                        @list_price,
                        @discount_percent,
                        @date_added);";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@category_id", MySqlDbType.Int32) { Value = categoryId },
                new MySqlParameter("@product_code", MySqlDbType.VarChar, 10) { Value = productCode },
                new MySqlParameter("@product_name", MySqlDbType.VarChar, 255) { Value = productName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = description },
                new MySqlParameter("@list_price", MySqlDbType.Decimal) { Value = listPrice },
                new MySqlParameter("@discount_percent", MySqlDbType.Decimal) { Value = discountPercent },
                new MySqlParameter("@date_added", MySqlDbType.DateTime) { Value = dateAdded }
            };

            return Insert(insertQuery, parameters);
        }

        static DataTable Select(string query)
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            connection.Close();

            return dataTable;
        }

        static int Insert(string query, params MySqlParameter[] parameters)
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();

            int id = Convert.ToInt32(command.LastInsertedId);

            connection.Close();

            return id;
        }
    }
}
