using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.db.DbOperations
{
    public class EmployeeRepository
    {
        public int AddEmployee(EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                Employee emp = new Employee()
                { FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    code = model.code
                };

                if(model.Address!= null)
                {
                   emp.Address = new Address()
                    {
                        Details=model.Address.Details,
                        State= model.Address.State,
                        Country=model.Address.Country
                    };
                }
                context.Employee.Add(emp);
                context.SaveChanges();

                return emp.Id;
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context= new EmployeeDBEntities())
            {
                var result = context.Employee.
                    Select(x => new EmployeeModel()
                    { 
                        FirstName = x.FirstName,
                        LastName =x.LastName,
                        Id=x.Id,
                        code=x.code,
                        AddressId=x.AddressId,

                        Address= new AddressModel()
                        {
                            Details = x.Address.Details,
                            State = x.Address.State,
                            Country = x.Address.Country,

                        }
             }).ToList();

                return result;
            }
        }

        public EmployeeModel GetEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                var result = context.Employee.
                    Where(x=>x.Id==id).
                    Select(x => new EmployeeModel()
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Id = x.Id,
                        code = x.code,
                        AddressId = x.AddressId,

                        Address = new AddressModel()
                        {
                            Details = x.Address.Details,
                            State = x.Address.State,
                            Country = x.Address.Country,

                        }
                    }).FirstOrDefault();

                return result;
            }
        }

        public bool UpdateEmployee(int id, EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities())
            {
                //var employee = context.Employee.FirstOrDefault(x => x.Id == id);

                //if (employee != null)
                //{
                //    employee.FirstName = model.FirstName;
                //    employee.LastName = model.LastName;
                //    employee.code = model.code;
                //    employee.Email = model.Email;
                //    //employee.Address.Details = model.Address.Details;
                //    //employee.Address.State = model.Address.State;
                //    //employee.Address.Country = model.Address.Country;

                //}
                //context.SaveChanges();
                //return true;


                //USING ENTITYSTATE
                var employee = new Employee();

                employee.Id = model.Id;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.code = model.code;
                employee.Email = model.Email;

                context.Entry(employee).State = System.Data.Entity.EntityState.Modified; 

                context.SaveChanges();
                return true;
                

            }
        }

        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeDBEntities())
            {
                //var employee = context.Employee.FirstOrDefault(x => x.Id == id);
                //if (employee != null)
                //{
                //    context.Employee.Remove(employee);
                //    context.SaveChanges();
                //    return true;
                //}



                //USING ENTITYSTATE
                var emp = new Employee()
                {
                    Id = id
                };

                context.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return false;
            }
        }
    }
}
