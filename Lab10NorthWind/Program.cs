using Lab10NorthWind.Data;
using Lab10NorthWind.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10NorthWind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthWindContext())
            {
                var query1 = context.Customers
                    .OrderBy(c1 => c1.CompanyName)
                    .Select(c1 => new
                    {
                        c1.CompanyName,
                        c1.Country,
                        c1.Region,
                        c1.Phone,
                        OrderCount = c1.Orders.Count
                    });

                foreach (var c1 in query1)
                {
                    Console.WriteLine($"Företagsnamn: {c1.CompanyName}");
                    Console.WriteLine($"Land: {c1.Country}");
                    Console.WriteLine($"Region: {c1.Region}");
                    Console.WriteLine($"Telefonnummer: {c1.Phone}");
                    Console.WriteLine($"Antal ordrar: {c1.OrderCount}");
                    Console.WriteLine();
                }


                var query2 = context.Customers
                    .Select(c2 => new
                    {
                        c2.CompanyName,
                        c2.Country,
                        c2.Region,
                        c2.Phone,
                        OrderCount = c2.Orders.Count // Räkna antal ordrar
                    });

                // Sortera efter företagsnamn i stigande ordning
                query2 = query2.OrderBy(c2 => c2.CompanyName);

                // Eller om användaren vill sortera i fallande ordning
                // query = query.OrderByDescending(c => c.CompanyName);

                foreach (var c2 in query2)
                {
                    Console.WriteLine($"Företagsnamn: {c2.CompanyName}");
                    Console.WriteLine($"Land: {c2.Country}");
                    Console.WriteLine($"Region: {c2.Region}");
                    Console.WriteLine($"Telefon: {c2.Phone}");
                    Console.WriteLine($"Antal ordrar: {c2.OrderCount}");
                    Console.WriteLine();
                }
                
                var existingCustomerIds = context.Customers.Select(c => c.CustomerId).ToList();

                Console.WriteLine("Befintliga kund-ID:");
                foreach (var customersId in existingCustomerIds)
                {
                    Console.WriteLine(customersId);
                }


                Console.WriteLine("Ange kundens ID: ");
                string customerId = Console.ReadLine();

                var customer = context.Customers
                    .Include(c => c.Orders) // Ladda ordrarna för kunden
                    .FirstOrDefault(c => c.CustomerId == customerId);

                if (customer != null)
                {
                    Console.WriteLine($"Företagsnamn: {customer.CompanyName}");
                    Console.WriteLine($"Land: {customer.Country}");
                    Console.WriteLine($"Region: {customer.Region}");
                    Console.WriteLine($"Telefon: {customer.Phone}");
                    Console.WriteLine($"Antal ordrar: {customer.Orders.Count}");

                    // Visa alla ordrar
                    Console.WriteLine("Kundens ordrar:");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"Order ID: {order.OrderId}");
                        // Visa övriga orderfält
                    }
                }
                else
                {
                    Console.WriteLine("Kunden hittades inte.");
                }



                var newCustomer = new Customer();

                Console.Write("Ange företagsnamn: ");
                newCustomer.CompanyName = Console.ReadLine();

                Console.Write("Ange land: ");
                newCustomer.Country = Console.ReadLine();

                Console.Write("Ange region: ");
                newCustomer.Region = Console.ReadLine();

                Console.Write("Ange telefonnummer: ");
                newCustomer.Phone = Console.ReadLine();

                // Generera en slumpad ID (5 tecken lång)
                var random = new Random();
                newCustomer.CustomerId = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                context.Customers.Add(newCustomer);
                context.SaveChanges();

            }
        }
    }
}