using Microsoft.EntityFrameworkCore;
using MS.RoadFire.Common.External;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Entities;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region Internals
        private readonly DbRoadFireContext _context;
        #endregion

        #region Constructor
        public CustomerRepository(DbRoadFireContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public override async Task<IEnumerable<Customer>> GetPaginationAsync(PaginationDTO pagination)
        {
            await Task.CompletedTask;
            var queryable = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) || x.SecondName.ToLower().Contains(pagination.Filter.ToLower()) ||
                    x.Surname.ToLower().Contains(pagination.Filter.ToLower()) || x.SecondSurname.ToLower().Contains(pagination.Filter.ToLower()) ||
                    x.Email.ToLower().Contains(pagination.Filter.ToLower()) || x.Identification.ToLower().Contains(pagination.Filter.ToLower()));            

            return queryable;
        }

        public override async Task<int> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) || x.SecondName.ToLower().Contains(pagination.Filter.ToLower()) ||
                    x.Surname.ToLower().Contains(pagination.Filter.ToLower()) || x.SecondSurname.ToLower().Contains(pagination.Filter.ToLower()) ||
                    x.Email.ToLower().Contains(pagination.Filter.ToLower()) || x.Identification.ToLower().Contains(pagination.Filter.ToLower()));            

            double count = await queryable.CountAsync();

            return (int)count;
        }
        #endregion
    }
}
