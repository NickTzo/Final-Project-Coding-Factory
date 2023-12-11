using BookingAppApi.Data;
using BookingAppApi.DTO;
using BookingAppApi.Repository;
using BookingAppApi.Repository.IRepository;
using BookingAppApi.Security;
using Microsoft.EntityFrameworkCore;

namespace BookingApiApp.Repository
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly BookingAppApiDbContext _dbContext;
        
        public UserRepo(BookingAppApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
         public async Task<User> GetUserAsync(UserLoginDTO request)
        {
            try
            {
                var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Username == request.Username);
                if (user is null) return null;
                if (!EncryptionUtil.IsValidPassword(request.Password, user.Password)) return null;

                return user;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
