
using BookingAppApi.DTO;

namespace BookingAppApi.Services.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// Bring all the users
        /// </summary>
        /// <returns>A list of the user that exist in the database</returns>
        Task<List<UserReadOnlyDTO>> GetAll();

        /// <summary>
        /// A service that create a user
        /// </summary>
        /// <param name="request">The details that the new user need to fill in the form</param>
        /// <returns>The ok if the account created or not</returns>
        Task<UserReadOnlyDTO> SingUpUserAsync(UserCreateDTO request);

        /// <summary>
        /// A service for login in the app each user
        /// </summary>
        /// <param name="request">The details for the username and the password</param>
        /// <returns>The logged in user and a token</returns>
        Task<UserReadOnlyDTO> SingInUserAsync(UserLoginDTO request);

        /// <summary>
        /// A service that bring the data of a user base on his id
        /// </summary>
        /// <param name="id">the users id</param>
        /// <returns>A data form a user</returns>
        Task<UserReadOnlyDTO> GetUserById(int id);

        /// <summary>
        /// A service that bring the data of the user base on his username
        /// </summary>
        /// <param name="username">the username that the user have</param>
        /// <returns>The data from the user</returns>
        Task<UserReadOnlyDTO?> GetUserByUsername(string username);

        /// <summary>
        /// A service that update the data of the user 
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="request">the new details that the user want to update</param>
        /// <returns>the updated user</returns>
        Task<UserReadOnlyDTO?> UpdateUserAsync(int id,UserUpdateDTO request);

        /// <summary>
        /// A service to delete a user
        /// </summary>
        /// <param name="id">the users id</param>
        /// <returns>Logout and delete the data from the specific user from the database</returns>
        Task<bool> DeleteUserAsync(int id);

        /// <summary>
        /// Create a token for the logged in user
        /// </summary>
        /// <param name="userId">the users id</param>
        /// <param name="username">the username of the user</param>
        /// <returns>The created token</returns>
        string CreateUserToken(int userId, string username);
    }
}
