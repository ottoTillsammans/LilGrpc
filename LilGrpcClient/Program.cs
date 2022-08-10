using Grpc.Net.Client;
using LilGrpcServer;
using System;
using System.Threading.Tasks;

namespace LilGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region String resources.

            const string create = "create";
            const string read = "read";
            const string update = "update";
            const string delete = "delete";
            const string exit = "exit";

            #endregion

            using var defaultChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(defaultChannel);
            string type = string.Empty;

            while (true)
            {
                Console.WriteLine("Type the command: \"create\", \"read\", \"update\", \"delete\", \"exit\"");

                type = Console.ReadLine();

                switch (type)
                {
                    case create:
                        var createReply = await client.SayHelloAsync(Create(type));
                        Console.WriteLine("\nServer reply:\n\n" + createReply.Message);
                        break;
                    case read:
                        var readReply = await client.SayHelloAsync(Read(type));
                        Console.WriteLine("\nServer reply:\n\n" + readReply.Message);
                        break;
                    case update:
                        var updateReply = await client.SayHelloAsync(Update(type));
                        Console.WriteLine("\nServer reply:\n\n" + updateReply.Message);
                        break;
                    case delete:
                        var deleteReply = await client.SayHelloAsync(Delete(type));
                        Console.WriteLine("\nServer reply:\n\n" + deleteReply.Message);
                        break;
                    case exit:
                        return;
                }
            }
        }

        #region Methods to create request.

        /// <summary>
        /// Create request entity to create employee.
        /// </summary>
        /// <param name="type">Type of operation.</param>
        /// <returns>Request entity.</returns>
        static HelloRequest Create(string type)
        {
            Console.WriteLine("Type Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Type Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Type Patronym:");
            string patronym = Console.ReadLine();

            Console.WriteLine("Type Gender (male or female):");
            bool gender = Console.ReadLine().ToLower() == "female" ? true : false;

            Console.WriteLine("Type Birthdate year:");
            int year = 0;
            Int32.TryParse(Console.ReadLine(), out year);

            Console.WriteLine("Type Birthdate month:");
            int month = 0;
            Int32.TryParse(Console.ReadLine(), out month);

            Console.WriteLine("Type Birthdate day:");
            int day = 0;
            Int32.TryParse(Console.ReadLine(), out day);

            Console.WriteLine("Type \"yes\" if has children, otherwise \"no\":");
            bool hasChildren = Console.ReadLine() == "yes" ? true : false;

            HelloRequest request = new HelloRequest
            {
                Type = type,
                Name = name,
                Surname = surname,
                Patronym = patronym,
                Gender = gender,
                Year = year,
                Month = month,
                Day = day,
                HasChildren = hasChildren
            };
            return request;
        }

        /// <summary>
        /// Create request entity to update employee.
        /// </summary>
        /// <param name="type">Type of operation.</param>
        /// <returns>Request entity.</returns>
        static HelloRequest Update(string type)
        {
            Console.WriteLine("< Press Enter to skip the parameter >\n");

            Console.WriteLine("Type Id: ");
            int id = 0;
            Int32.TryParse(Console.ReadLine(), out id);

            Console.WriteLine("Type Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Type Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Type Patronym:");
            string patronym = Console.ReadLine();

            Console.WriteLine("Type Gender (male or female):");
            bool gender = Console.ReadLine().ToLower() == "female" ? true : false;

            Console.WriteLine("Type Birthdate year:");
            int year = 0;
            Int32.TryParse(Console.ReadLine(), out year);

            Console.WriteLine("Type Birthdate month:");
            int month = 0;
            Int32.TryParse(Console.ReadLine(), out month);

            Console.WriteLine("Type Birthdate day:");
            int day = 0;
            Int32.TryParse(Console.ReadLine(), out day);

            Console.WriteLine("Type \"yes\" if has children, otherwise \"no\":");
            bool hasChildren = Console.ReadLine() == "yes" ? true : false;

            HelloRequest request = new HelloRequest
            {
                Id = id,
                Type = type,
                Name = name,
                Surname = surname,
                Patronym = patronym,
                Gender = gender,
                Year = year,
                Month = month,
                Day = day,
                HasChildren = hasChildren
            };
            return request;
        }

        /// <summary>
        /// Create request entity to get employees list.
        /// </summary>
        /// <param name="type">Type of operation.</param>
        /// <returns>Request entity.</returns>
        static HelloRequest Read(string type)
        {
            return new HelloRequest { Type = type };
        }

        /// <summary>
        /// Create request entity to delete employee.
        /// </summary>
        /// <param name="type">Type of operation.</param>
        /// <returns>Request entity.</returns>
        static HelloRequest Delete(string type)
        {
            int id = 0;

            Console.WriteLine("Type the employee's id you need to delete:");
            Int32.TryParse(Console.ReadLine(), out id);

            return new HelloRequest { Type = type, Id = id };
        }
    }

    #endregion
}