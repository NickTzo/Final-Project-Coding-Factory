using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using BookingAppApi.Data;
using BookingAppApi.DTO;
using Microsoft.AspNetCore.Authorization;

namespace BookingAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IMapper _mapper;

        public ReservationController(IUnitOfWorkService unitOfWorkService,IMapper mapper)
        {
            _unitOfWorkService = unitOfWorkService;
            _mapper = mapper;
        }


        // GET: api/Reservation
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            List<ReservationReadOnlyDTO> reservationsDto = await _unitOfWorkService.Reservation.GetAllReservationListAsync();
            if (reservationsDto.Count == 0)
          {
              return NotFound();
          }
            return Ok(reservationsDto);
        }

        // GET: api/Reservation/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
          ReservationReadOnlyDTO reservation = await _unitOfWorkService.Reservation.GetReservationById(id);
            if (await _unitOfWorkService.Reservation.GetAllReservationListAsync() == null || reservation == null)
          {
              return NotFound();
          }
           return Ok(reservation);
        }

        // GET: api/Reservations
        [Authorize]
        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationByUserId(int userId)
        {
            List<ReservationReadOnlyDTO> reservationsDto = await _unitOfWorkService.Reservation.GetReservationByUserId(userId);
            if (reservationsDto.Count == 0)
            {
                return NotFound();
            }
            return Ok(reservationsDto);
        }

        // PUT: api/Reservation/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationUpdateDTO reservationUpdateDto)
        {
            ReservationReadOnlyDTO reservation =await _unitOfWorkService.Reservation.GetReservationById(id);
            if(reservation == null)
            {
                return NotFound();
            }
            try
            {
                await _unitOfWorkService.Reservation.UpdateReservation(id,reservationUpdateDto);
                ReservationReadOnlyDTO reservationReadOnly = _mapper.Map<ReservationReadOnlyDTO>(reservationUpdateDto);
                return Ok(reservationReadOnly);
            }
            catch (Exception)
            {
                return Problem("Internal Server Error");
            }
        }

        // POST: api/Reservation
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationCreateDTO reservationCreateDto)
        {
          if (reservationCreateDto == null)
          {
              return Problem("Fields is empty");
          }
            try
            {               
                ReservationReadOnlyDTO reservationReadOnly = await _unitOfWorkService.Reservation.CreateReservation(reservationCreateDto);
                return CreatedAtAction(nameof(GetReservation), new { id = reservationReadOnly.Id }, reservationReadOnly);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Reservation/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            ReservationReadOnlyDTO reservation = await _unitOfWorkService.Reservation.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            try
            {
                await _unitOfWorkService.Reservation.DeleteReservation(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
