using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public partial class Course
    {
        public int Id { get; set; }
        public int? ProfessorId { get; set; }
        public string CourseName { get; set; }
        public string Participants { get; set; }
        public string Grades { get; set; }
    }
}
