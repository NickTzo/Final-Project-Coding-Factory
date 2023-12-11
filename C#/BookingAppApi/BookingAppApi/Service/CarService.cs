using AutoMapper;
using BookingAppApi.Data;
using BookingAppApi.DTO;
using BookingAppApi.Helpers;
using BookingAppApi.Services.IServices;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace BookingApiApp.Services
{
    public class CarService : ICarService
    {
        public readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public CarService(IUnitOfWorkRepo unitOfWorkRepo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _mapper = mapper;

            var acc = new Account
            (
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500),
                    Folder = "ba-net6"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<CarReadOnlyDTO> CarCreateAsync(CarCreateDTO request)
        {
            try
            {
                Car car = _mapper.Map<Car>(request);    
                if (request.Photo != null)
                {
                    ImageUploadResult photoUploadResults = await AddPhotoAsync(request.Photo!);
                    car.PhotoUrl = photoUploadResults.SecureUrl.AbsoluteUri;
                    car.PhotoId = photoUploadResults.PublicId;
                }
                Car carCreated = await _unitOfWorkRepo.Car.InsertAsync(car);
                await _unitOfWorkRepo.SaveAsync();
                CarReadOnlyDTO returnCar = _mapper.Map<CarReadOnlyDTO>(carCreated);
                return returnCar;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CarDeleteAsync(int id)
        {
            try
            {
                await _unitOfWorkRepo.Car.HardDeleteAsync(id);
                bool result = await _unitOfWorkRepo.SaveAsync();
                return result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<DeletionResult> DeleteAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<CarReadOnlyDTO> CarUpdateAsync(int id,CarUpdateDTO request)
        {
            Car oldCar = await _unitOfWorkRepo.Car.GetByIdAsync(id);
            oldCar.IsVisible = request.IsVisible;
            oldCar.Model = request.Model;
            oldCar.Year = request.Year;
            oldCar.Seat = request.Seat;
            oldCar.Cc = request.Cc;
            oldCar.Transmission = request.Transmission;
            oldCar.Price = request.Price;
            await _unitOfWorkRepo.SaveAsync();
            CarReadOnlyDTO carReadOnly = _mapper.Map<CarReadOnlyDTO>(oldCar);
            return carReadOnly;
        }

        public async Task<List<CarReadOnlyDTO>> GetAllCars()
        {
            List<Car> cars = new();
            cars = await _unitOfWorkRepo.Car.GetAllAsync();
            List<CarReadOnlyDTO> carsReadOnly = _mapper.Map<List<CarReadOnlyDTO>>(cars);
            return carsReadOnly;
        }

        public async Task<List<CarReadOnlyDTO>> GetCarByBrand(string brand)
        {
            List<Car> cars = new();
            cars = await _unitOfWorkRepo.Car.GetAllAsync(x => x.Brand == brand);
            List<CarReadOnlyDTO> carsReadOnly = _mapper.Map<List<CarReadOnlyDTO>>(cars);
            return carsReadOnly;
        }
        public async Task<List<CarReadOnlyDTO>> GetAvailiableCarsByDate(DateTime? startDate, DateTime? endDate)
        {
            List<Car> availiableCars = await _unitOfWorkRepo.Car.GetAvailiableCarsByDate(startDate, endDate);
            return _mapper.Map<List<CarReadOnlyDTO>>(availiableCars);
        }

        public async Task<CarReadOnlyDTO> GetCarById(int id)
        {
            Car car = await _unitOfWorkRepo.Car.GetByIdAsync(id);
            CarReadOnlyDTO carReadOnly = _mapper.Map<CarReadOnlyDTO>(car);
            return carReadOnly;
        }

        public async Task<List<CarReadOnlyDTO>> GetCarsByDate(DateTime startDate, DateTime endDate)
        {
            List<Car> cars = new();
            cars = await _unitOfWorkRepo.Car.GetAllAsync(x => x.IsVisible == true);
            List<CarReadOnlyDTO> carsReadOnly = _mapper.Map<List<CarReadOnlyDTO>>(cars);
            return carsReadOnly;
        }

       

      

        public async Task<List<CarReadOnlyDTO>> GetCarByBrandAndByDate(SearchDTO request)
        {
            List<Car> cars = new();
             cars = await _unitOfWorkRepo.Car.GetCarByBrandAndByDate(request.Brand,request.StartDate,request.EndDate);
            List<CarReadOnlyDTO> carReadOnly = _mapper.Map<List<CarReadOnlyDTO>>(cars);
            return carReadOnly;
        }

        public async Task<List<CarReadOnlyDTO>> GetCarBySearch(SearchDTO request)
        {
           if(!request.Brand.IsNullOrEmpty() && request.StartDate != null && request.EndDate != null)
            {
             return await GetCarByBrandAndByDate(request);
            }else if(!request.Brand.IsNullOrEmpty() && request.StartDate == null || request.EndDate == null)
            {
                return await GetCarByBrand(request.Brand!);
            }else if(request.Brand.IsNullOrEmpty() && request.StartDate != null && request.EndDate != null)
            {
                return await GetAvailiableCarsByDate(request.StartDate, request.EndDate);
            }
            else
            {
                return await GetAllCars();
            }
        }

        public async Task<List<CarReadOnlyDTO>> GetCarByUserId(int? userId)
        {
            List<Car> cars = new();
            cars = await _unitOfWorkRepo.Car.GetAllAsync(x => x.UserId == userId);
            List<CarReadOnlyDTO> carsReadOnly = _mapper.Map<List<CarReadOnlyDTO>>(cars);
            return carsReadOnly;
        }
    }
}
