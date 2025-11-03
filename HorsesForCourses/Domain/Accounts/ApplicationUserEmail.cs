using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Accounts.InvalidationReasons;

namespace HorsesForCourses.Domain.Accounts;

public record ApplicationUserEmail : DefaultString<ApplicationUserEmailCanNotBeEmpty, ApplicationUserEmailCanNotBeTooLong>
{
    public ApplicationUserEmail(string value) : base(value) { }
    protected ApplicationUserEmail() { }
    public static ApplicationUserEmail Empty => new();
}
