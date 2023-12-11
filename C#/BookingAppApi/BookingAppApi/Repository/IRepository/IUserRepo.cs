using BookingAppApi.Data;
using BookingAppApi.DTO;

namespace BookingAppApi.Repository.IRepository
{
    public interface IUserRepo : IGenericRepo<User>
    {
        /// <summary>
        /// For the login request
        /// </summary>
        /// <param name="request">The username and the password</param>
        /// <returns>The hashed password</returns>
        Task<User> GetUserAsync(UserLoginDTO request);
    }
}
