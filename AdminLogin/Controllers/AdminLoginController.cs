using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminLogin.Models;

namespace AdminLogin.Controllers
{
    [RoutePrefix("api/employee")]
    public class AdminLoginController : ApiController
    {
       
        ProjectEntities db = new ProjectEntities();
        [Route("Login")]
       [HttpPost]

        public Response Login(Login login)
        {
            var log = db.EmployeeLogins.Where(x => x.Email.Equals(login.Email) &&
            x.Password.Equals(login.Password)).FirstOrDefault();
            if (log == null)
            {
                return new Response { Status = "Invalid", Message = "Invalid User" };
            }
            else

                return new Response { Status = "Success", Message = "Login SuccessFull" };

        }

        [Route("empbyid")]
        [HttpGet]
        public EmployeeLogin getbyid(int Id)
        {
            return db.EmployeeLogins.Find(Id);
        }


        [Route("Add")]
        [HttpPost]
        public object addorupdatefood(Register fm)
        {
            try
            {
                if (fm.Id == 0)
                {
                    EmployeeLogin c = new EmployeeLogin();
                    c.EmployeeName = fm.EmployeeName;
                    c.Email = fm.Email;
                    c.City = fm.City;
                    c.Department = fm.Department;
                    c.Password = fm.Password;
                    db.EmployeeLogins.Add(c);
                    db.SaveChanges();

                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Add"
                    };
                }
                else
                {
                    var obj = db.EmployeeLogins.Where(x => x.Id == fm.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {
                        obj.EmployeeName = fm.EmployeeName;
                        obj.City = fm.City;
                        obj.Email = fm.Email;
                        obj.Department = fm.Department;
                        obj.Password = fm.Password;
                        db.SaveChanges();

                    }
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Add"
                    };
                }
            }
            catch
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Error"
                };
            }

        }

        [Route("list")]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<EmployeeLogin> Getfood()
        {


            return db.EmployeeLogins.ToList();

        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage deleteemployee(int Id)
        {
            EmployeeLogin r = db.EmployeeLogins.Find(Id);
            Console.WriteLine(r);
            if (r != null)
            {
                db.EmployeeLogins.Remove(r);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        [Route("InsertEmployee")]
        [HttpPost]
        public object InsertCustomer(Register rg)
        {
            try
            {
                EmployeeLogin ul = new EmployeeLogin();
                if (ul.Id == 0)
                {
                    ul.EmployeeName = rg.EmployeeName;
                    ul.City = rg.City;
                    ul.Email = rg.Email;
                    ul.Password = rg.Password;
                    ul.Department = rg.Department;

                    db.EmployeeLogins.Add(ul);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Record Successlly saved"
                    };
                }
            }
            catch (Exception)
            {
                return new Response
                {
                    Status = "AlreadyExists",
                    Message = "Invalid Data."
                };
            }
            return new Response
            {
                Status = "Error",
                Message = "Invalid Data."
            };
        }
    }
}