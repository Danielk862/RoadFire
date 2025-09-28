using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Contracts.Interfaces
{
    public interface ISecurityRepository
    {
        Task<User> Login(string username, string password);
    }
}
