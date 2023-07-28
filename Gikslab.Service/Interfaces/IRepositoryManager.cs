namespace Gikslab.Service.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        ISkillRepository Skill { get; }
        IActivityRepository Activity { get; }
        Task SaveAsync();
    }
}
