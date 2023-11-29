using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models;
using EPE3_PUNTONET.Repositorio;

namespace EPE3_PUNTONET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepository _reservaRepository;

        public ReservaController(ReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        // Método GET para listar todas las reservas
        [HttpGet]
        public async Task<IActionResult> GetAllReservas()
        {
            return Ok(await _reservaRepository.GetAllReservas());
        }

        // Método GET para obtener los detalles de una reserva en particular
        [HttpGet("{idReserva}")]
        public async Task<IActionResult> GetReservaById(int idReserva)
        {
            return Ok(await _reservaRepository.GetReservaById(idReserva));
        }

        // Método POST para crear y guardar una nueva reserva
        [HttpPost]
        public async Task<IActionResult> CreateReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaReservaId = await _reservaRepository.InsertReserva(reserva);
            return Created($"api/Reserva/{nuevaReservaId}", nuevaReservaId);
        }

        // Método PUT para editar y guardar cambios en una reserva seleccionada
        [HttpPut("{idReserva}")]
        public async Task<IActionResult> UpdateReserva(int idReserva, [FromBody] Reserva reserva)
        {
            if (reserva == null || idReserva != reserva.IdReserva)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reservaRepository.UpdateReserva(reserva);
            return NoContent();
        }

        // Método DELETE para eliminar una reserva seleccionada
        [HttpDelete("{idReserva}")]
        public async Task<IActionResult> DeleteReserva(int idReserva)
        {
            await _reservaRepository.DeleteReserva(idReserva);
            return NoContent();
        }
    }
}
