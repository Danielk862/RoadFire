using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Context;
using MS.RoadFire.DataAccess.Contracts.Interfaces;

namespace MS.RoadFire.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Internals
        private readonly DbRoadFireContext _context;
        private readonly DbSet<T> _entity;
        #endregion

        #region Constructor
        public GenericRepository(DbRoadFireContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var data = await _entity.FindAsync(id);
            return data!;
        }

        public async Task<T> AddAsync(T model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<T> UpdateAsync(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _entity.FindAsync(id);

            if (data != null)
            {
                _entity.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 
        }
        #endregion
    }
}
