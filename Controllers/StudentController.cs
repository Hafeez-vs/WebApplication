
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;


    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/students
    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _context.Studentsapi.ToList();
        return Ok(data);
    }

    // GET: api/students/5
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var student = _context.Studentsapi.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // POST
    [Authorize]
    [HttpPost]
    public IActionResult Create(Student student)
    {
        _context.Studentsapi.Add(student);
        _context.SaveChanges();

        return Ok(student);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, Student student)
    {
        if (id != student.Admno) return BadRequest();

        _context.Update(student);
        _context.SaveChanges();

        return Ok(student);
    }

    // DELETE
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _context.Studentsapi.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Studentsapi.Remove(student);
        _context.SaveChanges();

        return Ok();
    }
}