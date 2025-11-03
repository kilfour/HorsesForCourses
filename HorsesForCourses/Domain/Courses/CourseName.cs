using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Courses.InvalidationReasons;

namespace HorsesForCourses.Domain.Courses;

public record CourseName : DefaultString<CourseNameCanNotBeEmpty, CourseNameCanNotBeTooLong>
{
    public CourseName(string value) : base(value) { }
    protected CourseName() { }
    public static CourseName Empty => new();
}
