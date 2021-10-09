using EmployeeManagementAPI.Methods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private IEmployeeMethods employeeMethods;

        public EmployeeAPIController(IEmployeeMethods employeeMethods)
        {
            this.employeeMethods = employeeMethods;
        }

        [HttpGet]
        public ActionResult<IList<Employee>> GetAllData()
        {
            try
            {
                return (ActionResult<IList<Employee>>)Ok(employeeMethods.GetAllData());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }


        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetDataByID(int id)
        {
            try
            {
                Employee em = employeeMethods.GetDataByID(id);

                if(em.ID==0)
                {
                    return NotFound();
                }
                return Ok(em);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            try 
            {
                employee.ID = employeeMethods.Save(employee);
                return CreatedAtAction(nameof(GetDataByID), new { id = employee.ID }, employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Employee");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id,Employee employee)
        {
            int rowsAffected = 0;
            try
            {
                if(id!=employee.ID)
                {
                    return BadRequest("ID is not matched");
                }

                rowsAffected = employeeMethods.Update(employee);

                if (rowsAffected <= 0)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Employee");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = employeeMethods.Delete(id);
                if (rowsAffected <= 0)
                {
                    return NotFound();
                }
                return Ok(string.Format("Employee ID : {0} is deleted successfully",id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Employee");
            }
        }
    }    
}

