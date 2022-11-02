using MongoDB.Driver;
using MongoDbEmployee.Interfaces;
using MongoDbEmployee.Models;
using System.Collections.Generic;

namespace MongoDbEmployee.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeService(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);

        }

        public List<Employee> Get()
        {
            List<Employee> employees;
            employees = _employees.Find(emp => true).ToList();
            return employees;
        }

        public Employee Get(string id) =>
            _employees.Find<Employee>(emp => emp.Id == id).FirstOrDefault();

        public void Create(Employee newEmployee) =>
            _employees.InsertOne(newEmployee);

        public void Update(string id, Employee updatedEmployee) =>
             _employees.ReplaceOne(x => x.Id == id, updatedEmployee);

        public void Remove(string id) => _employees.DeleteOne(x => x.Id == id);

    }
}
