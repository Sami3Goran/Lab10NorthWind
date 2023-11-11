using Lab10NorthWind.Data;
using Lab10NorthWind.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10NorthWind
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("1. Hämta alla kunder");
                Console.WriteLine("2. Visa detaljer för en kund");
                Console.WriteLine("3. Lägg till kund");
                Console.WriteLine("4. Avsluta");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Clear(); // Rensa konsoloutput

                    switch (choice)
                    {
                        case 1:
                            GetAllCustomers();
                            break;
                        case 2:
                            ShowCustomerDetails();
                            break;
                        case 3:
                            AddCustomer();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            break;
                    }
                }
                else
                {
                    Console.Clear(); // Rensa konsoloutput
                    Console.WriteLine("Ogiltig inmatning. Försök igen.");
                }

                Console.WriteLine();
            }
        }

        static void GetAllCustomers()
        {
            using (var context = new NorthWindContext())
            {
                Console.WriteLine("1. Stigande ordning");
                Console.WriteLine("2. Fallande ordning");
                int orderChoice;
                if (int.TryParse(Console.ReadLine(), out orderChoice))
                {
                    Console.Clear(); // Rensa konsoloutput
                    var customersQuery = orderChoice == 1
                        ? context.Customers.OrderBy(c => c.CompanyName)
                        : context.Customers.OrderByDescending(c => c.CompanyName);

                    var customers = customersQuery
                        .Select(c => new
                        {
                            c.CustomerId,
                            c.CompanyName,
                            c.Country,
                            c.Region,
                            c.Phone,
                            OrderCount = c.Orders.Count
                        })
                        .ToList();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID: {customer.CustomerId}");
                        Console.WriteLine($"Företagsnamn: {customer.CompanyName}");
                        Console.WriteLine($"Land: {customer.Country}");
                        Console.WriteLine($"Region: {customer.Region}");
                        Console.WriteLine($"Telefonnummer: {customer.Phone}");
                        Console.WriteLine($"Antal ordrar: {customer.OrderCount}");
                        Console.WriteLine("------------------------");
                    }
                }
                else
                {
                    Console.Clear(); // Rensa konsoloutput
                    Console.WriteLine("Ogiltig inmatning för ordning. Försök igen.");
                }
            }
            Console.Clear(); // Rensa konsoloutput efter att ha visat alla kunder
        }

        static void ShowCustomerDetails()
        {
            using (var context = new NorthWindContext())
            {
                Console.Clear(); // Rensa konsoloutput
                ListAllCustomersWithIds();

                Console.WriteLine("Ange kundens ID:");
                string customerId = Console.ReadLine();

                var customer = context.Customers
                    .Include(c => c.Orders)
                    .FirstOrDefault(c => c.CustomerId == customerId);

                if (customer != null)
                {
                    Console.WriteLine($"Företagsnamn: {customer.CompanyName}");
                    Console.WriteLine($"Land: {customer.Country}");
                    Console.WriteLine($"Region: {customer.Region}");
                    Console.WriteLine($"Telefonnummer: {customer.Phone}");
                    Console.WriteLine("Ordrar:");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"  Order ID: {order.OrderId}, Datum: {order.OrderDate}");
                    }
                }
                else
                {
                    Console.Clear(); // Rensa konsoloutput
                    Console.WriteLine("Kund hittades inte.");
                }
            }
            Console.Clear(); // Rensa konsoloutput efter att ha visat detaljer för en kund
        }

        static void AddCustomer()
        {
            using (var context = new NorthWindContext())
            {
                Console.Clear(); // Rensa konsoloutput
                var newCustomer = new Customer();

                Console.WriteLine("Ange företagsnamn:");
                newCustomer.CompanyName = Console.ReadLine();

                Console.WriteLine("Ange land:");
                newCustomer.Country = Console.ReadLine();

                Console.WriteLine("Ange region:");
                newCustomer.Region = Console.ReadLine();

                Console.WriteLine("Ange telefonnummer:");
                newCustomer.Phone = Console.ReadLine();

                // Generera ett slumpmässigt ID (5 tecken)
                newCustomer.CustomerId = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();

                context.Customers.Add(newCustomer);
                context.SaveChanges();

                Console.Clear(); // Rensa konsoloutput
                Console.WriteLine("Kund tillagd.");
            }
        }

        static void ListAllCustomersWithIds()
        {
            using (var context = new NorthWindContext())
            {
                Console.Clear(); // Rensa konsoloutput
                var customers = context.Customers
                    .Select(c => new { c.CustomerId, c.CompanyName })
                    .ToList();

                Console.WriteLine("Tillgängliga kunder:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"ID: {customer.CustomerId}, Företagsnamn: {customer.CompanyName}");
                }
            }
        }
    }
}