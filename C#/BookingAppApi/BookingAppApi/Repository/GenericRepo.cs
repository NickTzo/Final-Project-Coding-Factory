using BookingAppApi.Data;
using BookingAppApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingAppApi.Repository
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {


        private readonly BookingAppApiDbContext _dbContext;

        public GenericRepo(BookingAppApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TEntity> GetByIdAsync(int Id)
        {
            try
            {
                var result = await _dbContext.Set<TEntity>().FindAsync(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                List<TEntity> entities = new();

                if (filter != null)
                {
                    return entities = await _dbContext.Set<TEntity>().Where(filter).ToListAsync();
                }
                else
                {
                    entities = await _dbContext.Set<TEntity>().ToListAsync();
                }

                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                var result = await _dbContext.Set<TEntity>().AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {

                var result = _dbContext.Set<TEntity>().Update(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> HardDeleteAsync(int Id)
        {
            try
            {
                var result = await _dbContext.Set<TEntity>().FindAsync(Id);
                if(result == null)
                {
                    return false;
                }
                var entityToDelete = await GetByIdAsync(Id);
                _dbContext.Set<TEntity>().Remove(entityToDelete);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
