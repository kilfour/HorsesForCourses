using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Coaches.InvalidationReasons;

namespace HorsesForCourses.Domain.Coaches;

public record CoachName : DefaultString<CoachNameCanNotBeEmpty, CoachNameCanNotBeTooLong>
{
    public CoachName(string value) : base(value) { }
    protected CoachName() { }
    public static CoachName Empty => new();
}
