using AutoMapper;
using BookingAppApi.Data;
using BookingAppApi.DTO;
using BookingAppApi.Helpers;
using BookingAppApi.Security;
using BookingAppApi.Services.IServices;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingApiApp.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;



        public UserService(IUnitOfWorkRepo unitOfWorkRepo, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _mapper = mapper;
            _configuration = configuration;


        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await _unitOfWorkRepo.User.HardDeleteAsync(id);
                bool result = await _unitOfWorkRepo.SaveAsync();
                return result;
            }catch (Exception ex)
            {
                return false;
            }
          
        }

        public async Task<UserReadOnlyDTO?> GetUserByUsername(string username)
        {
            User user = (await _unitOfWorkRepo.User.GetAllAsync(x => x.Username == username)).FirstOrDefault()!;
            await _unitOfWorkRepo.SaveAsync();
            return _mapper.Map<UserReadOnlyDTO?>(user);
        }

        public async Task<List<UserReadOnlyDTO>> GetAll()
        {
            List<User> users = await _unitOfWorkRepo.User.GetAllAsync();
            List<UserReadOnlyDTO> usersReadOnly = _mapper.Map<List<UserReadOnlyDTO>>(users);
            return usersReadOnly;
        }

        public async Task<UserReadOnlyDTO> SingInUserAsync(UserLoginDTO request)
        {
            User user = await _unitOfWorkRepo.User.GetUserAsync(request);            
            return _mapper.Map<UserReadOnlyDTO>(user);
        }

        public async Task<UserReadOnlyDTO> SingUpUserAsync(UserCreateDTO request)
        {
            User user = _mapper.Map<User>(request);
            user.Password = EncryptionUtil.Encrypt(user.Password);
            User userCreated = await _unitOfWorkRepo.User.InsertAsync(user);
            UserReadOnlyDTO returnUser = _mapper.Map<UserReadOnlyDTO>(userCreated);
            await _unitOfWorkRepo.SaveAsync();
            return returnUser;
        }

        public async Task<UserReadOnlyDTO?> UpdateUserAsync(int id, UserUpdateDTO request)
        {
            User newUser = await _unitOfWorkRepo.User.GetByIdAsync(id);
            newUser.Username = request.Username;
            newUser.Email = request.Email;
            newUser.Phone = request.Phone;
            newUser.Firstname = request.Firstname;
            newUser.Lastname = request.Lastname;
            await _unitOfWorkRepo.SaveAsync();
            UserReadOnlyDTO returnUser = _mapper.Map<UserReadOnlyDTO>(newUser);           
            return returnUser;
        }

        public async Task<UserReadOnlyDTO> GetUserById(int id)
        {
            User user = await _unitOfWorkRepo.User.GetByIdAsync(id);
            UserReadOnlyDTO userReadOnly = _mapper.Map<UserReadOnlyDTO>(user);
            return userReadOnly;
        }

        public string CreateUserToken(int userId, string username)
        {
            string appSecurityKey = _configuration["TokenKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claimsInfo = new List<Claim>();
            claimsInfo.Add(new Claim(ClaimTypes.Name, username));
            claimsInfo.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            
            var jwtSecurityToken = new JwtSecurityToken(null,null,claimsInfo,DateTime.UtcNow,DateTime.UtcNow.AddHours(3), signingCredentials);

            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return userToken;
        }


    }
}
