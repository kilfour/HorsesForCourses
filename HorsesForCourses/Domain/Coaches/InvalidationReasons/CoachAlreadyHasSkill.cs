using HorsesForCourses.Abstractions;

namespace HorsesForCourses.Domain.Coaches.InvalidationReasons;

public class CoachAlreadyHasSkill(string skill) : DomainException(skill) { }