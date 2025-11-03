using HorsesForCourses.Abstractions;
using HorsesForCourses.Domain.Courses.InvalidationReasons;
using HorsesForCourses.Domain.Courses.TimeSlots;
using HorsesForCourses.Domain.Skills;
using HorsesForCourses.ValidationHelpers;
using HorsesForCourses.Domain.Accounts;
using HorsesForCourses.Domain.Coaches;

namespace HorsesForCourses.Domain.Courses;

public class Course : DomainEntity<Course>
{
    public CourseName Name { get; init; } = CourseName.Empty;

    public Period Period { get; init; } = Period.Empty;

    public IReadOnlyList<TimeSlot> TimeSlots => timeSlots;
    private readonly List<TimeSlot> timeSlots = [];

    public IReadOnlySet<Skill> RequiredSkills => requiredSkills;
    private readonly HashSet<Skill> requiredSkills = [];

    public bool IsConfirmed { get; private set; }
    public Coach? AssignedCoach { get; private set; }

    private Course() { /*** EFC Was Here ****/ }
    protected Course(string name, DateOnly start, DateOnly end)
    {
        Name = new CourseName(name);
        Period = Period.From(start, end);
    }

    private static void OnlyActorsWithAdminRoleCanCreateOrEditCourses(Actor actor)
        => actor.CanEditCourses();

    public static Course Create(Actor actor, string name, DateOnly start, DateOnly end)
    {
        OnlyActorsWithAdminRoleCanCreateOrEditCourses(actor);
        return new Course(name, start, end);
    }

    bool NotAllowedIfAlreadyConfirmed()
        => IsConfirmed ? throw new CourseAlreadyConfirmed() : true;

    public virtual Course UpdateRequiredSkills(Actor actor, IEnumerable<string> newSkills)
    {
        OnlyActorsWithAdminRoleCanCreateOrEditCourses(actor);
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenThereAreDuplicateSkills();
        return OverwriteRequiredSkills();
        // ------------------------------------------------------------------------------------------------
        // --
        bool NotAllowedWhenThereAreDuplicateSkills()
            => newSkills.NoDuplicatesAllowed(a => new CourseAlreadyHasSkill(string.Join(",", a)));
        Course OverwriteRequiredSkills()
        {
            requiredSkills.Clear();
            foreach (var s in newSkills.Select(Skill.From)) requiredSkills.Add(s);
            return this;
        }
        // ------------------------------------------------------------------------------------------------
    }

    public virtual Course UpdateTimeSlots<T>(
        Actor actor,
        IEnumerable<T> timeSlotInfo,
        Func<T, (CourseDay Day, int Start, int End)> getTimeSlot)
    {
        OnlyActorsWithAdminRoleCanCreateOrEditCourses(actor);
        var newTimeSlots = TimeSlot.EnumerableFrom(timeSlotInfo, getTimeSlot);
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenTimeSlotsOverlap();
        return OverwriteTimeSlots();
        // ------------------------------------------------------------------------------------------------
        // --
        bool NotAllowedWhenTimeSlotsOverlap()
            => TimeSlot.HasOverlap(newTimeSlots) ? throw new OverlappingTimeSlots() : true;
        Course OverwriteTimeSlots()
        {
            timeSlots.Clear();
            timeSlots.AddRange(newTimeSlots);
            return this;
        }
        // ------------------------------------------------------------------------------------------------
    }

    public Course Confirm(Actor actor)
    {
        OnlyActorsWithAdminRoleCanCreateOrEditCourses(actor);
        NotAllowedIfAlreadyConfirmed();
        NotAllowedWhenThereAreNoTimeSlots();
        return ConfirmIt();
        // ------------------------------------------------------------------------------------------------
        // --
        bool NotAllowedWhenThereAreNoTimeSlots()
            => TimeSlots.Count == 0 ? throw new AtLeastOneTimeSlotRequired() : true;
        Course ConfirmIt() { IsConfirmed = true; return this; }
        // ------------------------------------------------------------------------------------------------
    }

    public virtual Course AssignCoach(Actor actor, Coach coach)
    {
        OnlyActorsWithAdminRoleCanCreateOrEditCourses(actor);
        NotAllowedIfNotYetConfirmed();
        NotAllowedIfCourseAlreadyHasCoach();
        NotAllowedIfCoachIsInsuitable(coach);
        NotAllowedIfCoachIsUnavailable(coach);
        return AssignTheCoachAlready(coach);

        // ------------------------------------------------------------------------------------------------
        // --
        bool NotAllowedIfNotYetConfirmed()
            => !IsConfirmed ? throw new CourseNotYetConfirmed() : true;
        bool NotAllowedIfCourseAlreadyHasCoach()
            => AssignedCoach != null ? throw new CourseAlreadyHasCoach() : true;
        bool NotAllowedIfCoachIsInsuitable(Coach coach)
            => !coach.IsSuitableFor(this) ? throw new CoachNotSuitableForCourse() : true;
        bool NotAllowedIfCoachIsUnavailable(Coach coach)
            => !coach.IsAvailableFor(this) ? throw new CoachNotAvailableForCourse() : true;
        Course AssignTheCoachAlready(Coach coach)
        {
            AssignedCoach = coach;
            coach.AssignCourse(this);
            return this;
        }
        // ------------------------------------------------------------------------------------------------
    }
}