using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Courses;
using HorsesForCourses.Domain.Coaches;

namespace HorsesForCourses.Domain;

public class UnavailableFor(Id<Coach> CoachId, Id<Course> CourseId) : DomainEntity<UnavailableFor>
{
    public Id<Coach> CoachId { get; } = CoachId;
    public Id<Course> CourseId { get; } = CourseId;
}
