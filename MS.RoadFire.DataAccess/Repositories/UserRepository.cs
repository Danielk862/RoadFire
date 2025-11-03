using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MS.RoadFire.Common.External;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbRoadFireContext _context;

        public UserRepository(DbRoadFireContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<User>> GetPaginationAsync(PaginationDTO paginationDTO)
        {
            var queryable = _context.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
            {
                var filter = paginationDTO.Filter;
                var search = $"%{filter}%";

                // Usamos EF.Functions.Like con Collate para ignorar mayúsculas, minúsculas y tildes
                queryable = queryable.Where(u =>
                    EF.Functions.Like(EF.Functions.Collate(u.Username, "Latin1_General_CI_AI"), search) ||
                    (u.Employee != null && (
                        EF.Functions.Like(EF.Functions.Collate(u.Employee.FirtsName, "Latin1_General_CI_AI"), search) ||
                        EF.Functions.Like(EF.Functions.Collate(u.Employee.Surname, "Latin1_General_CI_AI"), search)
                    )) ||
                    (u.Role != null && EF.Functions.Like(EF.Functions.Collate(u.Role.Name, "Latin1_General_CI_AI"), search))
                );
            }

            return await queryable
                .OrderBy(u => u.Username)
                .Paginate(paginationDTO)
                .ToListAsync();
        }

        public new async Task<int> GetTotalRecordsAsync(PaginationDTO paginationDTO)
        {
            var queryable = _context.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
            {
                var filter = paginationDTO.Filter;
                var search = $"%{filter}%";

                // Usamos EF.Functions.Like con Collate para ignorar mayúsculas, minúsculas y tildes
                queryable = queryable.Where(u =>
                    EF.Functions.Like(EF.Functions.Collate(u.Username, "Latin1_General_CI_AI"), search) ||
                    (u.Employee != null && (
                        EF.Functions.Like(EF.Functions.Collate(u.Employee.FirtsName, "Latin1_General_CI_AI"), search) ||
                        EF.Functions.Like(EF.Functions.Collate(u.Employee.Surname, "Latin1_General_CI_AI"), search)
                    )) ||
                    (u.Role != null && EF.Functions.Like(EF.Functions.Collate(u.Role.Name, "Latin1_General_CI_AI"), search))
                );
            }

            return await queryable.CountAsync();
        }
    }
}