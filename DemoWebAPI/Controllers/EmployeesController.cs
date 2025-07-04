using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDBContext _Context;
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public EmployeesController(AppDBContext Context)
        {
            _Context = Context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _Context.Employees.ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            var employee = await _Context.Employees.FindAsync(Id);
            if (employee == null)
                return NotFound();
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _Context.Employees.Add(employee);
            _Context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEmployee(int Id,Employee employee)
        {
            if (Id != employee.Id) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest();
            }
            _Context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!_Context.Employees.Any(e => e.Id == Id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task <IActionResult> DeleteEmployee(int Id)
        {
            var employee = await _Context.Employees.FindAsync(Id);
            if(employee == null)
                return NotFound();

            _Context.Employees.Remove(employee);
            _Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
