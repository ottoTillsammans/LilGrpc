using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace AdvanticaGrpcServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        string server = @"(localdb)\mssqllocaldb";
        string dBName = "advantica";
        string creds = "Trusted_Connection = True";

        const string create = "create";
        const string read = "read";
        const string delete = "delete";

        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            string result = string.Empty;
            
            using (Context db = new Context(server, dBName, creds))
            {
                switch (request.Type)
                {
                    case create:
                        result = CreateOrUpdateEmployee(request, db);
                        break;
                    case read:
                        result = ReadEmployees(db);
                        break;
                    case delete:
                        result = DeleteEmployee(request, db);
                        break;
                    default:
                        result = string.Format("Input data \"type\" mismatch the template. Available values are: \"{0}\", \"{1}\", \"{2}\".", create, read, delete);
                        break;
                }
            }
            return Task.FromResult(new HelloReply
            {
                Message = result
            });
        }

        public string CreateOrUpdateEmployee(HelloRequest request, Context db)
        {
            Employee employee = db.Employees.Where(e => e.Id == request.Id).FirstOrDefault();
            string result = string.Empty;

            if(employee != null)
            {
                try
                {
                    employee.Name = request.Name;
                    employee.Surname = request.Surname;
                    employee.Patronym = request.Patronym;
                    employee.Birthdate = new DateTime(request.Year, request.Month, request.Day);
                    employee.Gender = request.Gender ? GenderType.Female : GenderType.Male;
                    employee.HasChildren = request.HasChildren;

                    db.SaveChanges();
                    result = string.Format("Employee's data with Id={0} was changed successfully.", request.Id);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                try
                {
                    DateTime date = new DateTime(request.Year, request.Month, request.Day);
                    GenderType gender = request.Gender ? GenderType.Female : GenderType.Male;
                    employee = new Employee(request.Name, request.Surname, request.Patronym, gender, date, request.HasChildren);

                    db.SaveChanges();
                    result = string.Format("Employee with Id={0} was created successfully.", request.Id);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return result;
        }

        public string ReadEmployees(Context db)
        {
            string result = string.Empty;
            
            try
            {
                List<Employee> employees = db.Employees.ToList();

                if (!employees.Any())
                    return result = "The employees list is empty.";

                StringBuilder sb = new StringBuilder();

                foreach(var e in employees)
                {
                    string patronym = string.IsNullOrWhiteSpace(e.Patronym) ? string.Empty : string.Format(" {0}", e.Patronym);
                    string hasChildren = e.HasChildren ? "Has children" : "Has no children";
                    sb.Append(string.Format("Employee: {0} {1}{2}, {3}, {4}, {5}\n", e.Name, e.Surname, patronym, e.Birthdate, e.Gender, hasChildren));
                }
                result = sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }

        public string DeleteEmployee(HelloRequest request, Context db)
        {
            string result = string.Empty;

            try
            {
                Employee employee = db.Employees.Where(e => e.Id == request.Id).FirstOrDefault();

                if(employee != null)
                {
                    db.Remove(employee);
                    db.SaveChanges();

                    result = string.Format("Employee with Id={0} was deleted successfully", request.Id);
                }
                else
                {
                    result = string.Format("Employee with Id={0} not found and not available to delete.", request.Id);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
    }
}
