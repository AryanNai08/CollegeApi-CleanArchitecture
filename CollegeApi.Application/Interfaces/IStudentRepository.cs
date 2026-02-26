using CollegeApi.Domain.Entities;

namespace CollegeApi.Application.Interfaces
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeesStatus(int feeStatus);

    }
}
