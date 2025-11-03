using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Accounts.InvalidationReasons;

namespace HorsesForCourses.Domain.Accounts;

public record ApplicationUserName : DefaultString<ApplicationUserNameCanNotBeEmpty, ApplicationUserNameCanNotBeTooLong>
{
    public ApplicationUserName(string value) : base(value) { }
    protected ApplicationUserName() { }
    public static ApplicationUserName Empty => new();
}
