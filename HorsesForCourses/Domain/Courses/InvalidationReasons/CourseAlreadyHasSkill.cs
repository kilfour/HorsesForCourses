using HorsesForCourses.Abstractions;

namespace HorsesForCourses.Domain.Courses.InvalidationReasons;

public class CourseAlreadyHasSkill(string skill) : DomainException(skill) { }
