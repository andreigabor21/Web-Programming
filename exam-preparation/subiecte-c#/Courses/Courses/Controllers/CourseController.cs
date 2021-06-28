using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Courses.Models;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CoursesContext _context;

        public CourseController(CoursesContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Course/name
        [HttpGet("{name}")]
        public ActionResult<String> GetCourse([FromForm]string name)
        {
            var participants = _context.Courses
                .Where(course => course.CourseName == name)
                .First().Participants;
            Console.WriteLine(participants);
            return participants;
        }
        
        // GET: api/Course/courses/" + name
        [HttpGet("Courses/{name}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesOfStudent(string name)
        {
            Console.WriteLine(name);
            return await _context.Courses
                .Where(course => course.Participants.Contains(name))
                .ToListAsync();
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromForm]string student, [FromForm]string course, [FromForm]int grade)
        {
            var courseFound = _context.Courses.FirstOrDefault(c => c.CourseName.Equals(course));
            Console.WriteLine(courseFound);

            if (!courseFound.Participants.Contains(student))
            {
                courseFound.Participants += student + ";";
            }
            if (!courseFound.Grades.Contains(student))
            {
                courseFound.Grades += student + "-" + grade.ToString() + ";";
            }
            else
            {
                string pattern = $"(?<={student}-)(.*?)(?=;)";
                var newGrades = Regex.Replace(courseFound.Grades, pattern, grade.ToString());
                courseFound.Grades = newGrades;
            }

            Console.WriteLine(courseFound.Grades);
            await _context.SaveChangesAsync();
            return courseFound;
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
