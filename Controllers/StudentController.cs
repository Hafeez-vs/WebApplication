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
        var data = _context.Studentsapi.OrderBy(s => s.Admno).ToList();
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
    [Consumes("multipart/form-data")]
    public IActionResult Create([FromForm] Student student,IFormFile imagefile)
    {
        if (imagefile != null && imagefile.Length > 0)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagefile.FileName);

            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imagefile.CopyTo(stream);
            }

            student.ImageUrl = "/images/" + fileName;
        }
        _context.Studentsapi.Add(student);
        _context.SaveChanges();

        return Ok(student);
    }

    // PUT
    [Authorize]
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public IActionResult Update( int id, [FromForm] Student student,IFormFile imagefile)
    {
        var exist = _context.Studentsapi.Find(id);
        if(exist == null)
        {
            return NotFound();
        }

        exist.Admno=student.Admno;
        exist.name = student.name;
        exist.age = student.age;
        exist.course = student.course;
        exist.mark = student.mark;

        if (imagefile != null && imagefile.Length > 0)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!string.IsNullOrEmpty(exist.ImageUrl))
            {
                string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",exist.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }
            }
            string filename= Guid.NewGuid().ToString() + Path.GetExtension(imagefile.FileName);
            string filepath= Path.Combine(folder,filename);
            using (var stream = new FileStream(filepath,FileMode.Create))
            {
                imagefile.CopyTo(stream);
            }
            exist.ImageUrl = "/images/" + filename;
        }
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