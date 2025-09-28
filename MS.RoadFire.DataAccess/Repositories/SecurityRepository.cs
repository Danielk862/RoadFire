using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        #region Internals
        private readonly DbRoadFireContext _context;
        #endregion

        #region Constructor
        public SecurityRepository(DbRoadFireContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.AsNoTracking().Where(x => x.Username == username && x.Password == password && x.State).FirstOrDefaultAsync();
            return user!;
        }
        #endregion
    }
}
