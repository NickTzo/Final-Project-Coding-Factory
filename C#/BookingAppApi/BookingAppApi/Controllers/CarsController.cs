using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAppApi.Data;
using BookingAppApi.DTO;
using AutoMapper;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace BookingAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IMapper _mapper;

        public CarsController(IMapper mapper,IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
            _mapper = mapper;
        }

        //Used
        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarReadOnlyDTO>>> GetCars()
        {
           List<CarReadOnlyDTO> carsDto = await _unitOfWorkService.Car.GetAllCars();
           if(carsDto.Count == 0) return NotFound();
           return Ok(carsDto);
        }

        //Used
        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarReadOnlyDTO>> GetCar(int id)
        {
            CarReadOnlyDTO car = await _unitOfWorkService.Car.GetCarById(id);
            if (await _unitOfWorkService.Car.GetCarById(id) == null)
          {
              return NotFound();
          }
            return Ok(car);
        }


        //not used
        // GET: api/Cars/Brand
        [HttpGet("Brand/{brand}")]
        public async Task<ActionResult<IEnumerable<CarReadOnlyDTO>>> GetCars(string brand)
        {
            List<CarReadOnlyDTO> cars = await _unitOfWorkService.Car.GetCarByBrand(brand);
            if (await _unitOfWorkService.Car.GetAllCars() == null || cars is null)
            {
                return NotFound();
            }
            return Ok(cars);
        }

        //not used
        // GET: api/Cars/Dates
        [HttpGet("ByDates")]
        public async Task<ActionResult<IEnumerable<CarReadOnlyDTO>>> GetCars(DateTime start, DateTime end)
        {
            List<CarReadOnlyDTO> cars = await _unitOfWorkService.Car.GetAvailiableCarsByDate(start, end);
            return Ok(cars);
        }


        //Used
        //PUT: api/Cars
        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> PutCar(CarUpdateDTO carUpdateDto)
        {
            CarReadOnlyDTO car = await _unitOfWorkService.Car.GetCarById(carUpdateDto.Id);
            if (car == null)
            {
                return NotFound();
            }
            try
            {               
                CarReadOnlyDTO carUpdated = await _unitOfWorkService.Car.CarUpdateAsync(carUpdateDto.Id,carUpdateDto);
                return Ok(carUpdated);
            }
            catch (Exception)
            {   
                return BadRequest("Internal Error Service");
            }

        }

        //Used
        // POST: api/Cars
        [Authorize]
        [HttpPost("PostCar")]
        public async Task<ActionResult<Car>> PostCar([FromForm] CarCreateDTO carCreateDTO)
        {
            try
            {
                //carCreateDTO.Photo = file;
                CarReadOnlyDTO dto = await _unitOfWorkService.Car.CarCreateAsync(carCreateDTO);
                return CreatedAtAction(nameof(GetCar), new { id = dto.Id }, dto);
            }
            catch (Exception)
            {
                
                return BadRequest("Internal Error Service");
            }
        }


        // POST: api/Cars/UserId/userId
        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<CarReadOnlyDTO>> GetCarByUserId(int userId)
        {
            try
            {
                List<CarReadOnlyDTO> cars = await _unitOfWorkService.Car.GetCarByUserId(userId);
                if (cars.Count == 0) return NotFound();
                return Ok(cars);
            }
            catch
            {
                return BadRequest("Internal Error Service");
            }
           
        }

        //POST: api/Cars/SearchBar
        [HttpPost("GetCarByBrandAndByDate")]
        public async Task<ActionResult<IEnumerable<CarReadOnlyDTO>>> GetCarByBrandAndByDate(SearchDTO request)
        {
            try
            {
                List<CarReadOnlyDTO> cars = await _unitOfWorkService.Car.GetCarBySearch(request);
                return Ok(cars);
            }
            catch
            {
                return BadRequest("Internal Error Service");
            }
           
        }

        // DELETE: api/Cars/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _unitOfWorkService.Car.GetCarById(id);
            if (await _unitOfWorkService.Car.GetAllCars() == null || car == null)
            {
                return NotFound();
            }
            await _unitOfWorkService.Car.CarDeleteAsync(id);
            return Ok();
        }
    }
}
