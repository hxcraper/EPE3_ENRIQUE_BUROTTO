using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models;

namespace EPE3_PUNTONET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoRepository _medicoRepository;

        // Constructor: Recibe una instancia de MedicoRepository mediante inyección de dependencias
        public MedicoController(MedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        // Endpoint HTTP GET para obtener todos los médicos
        [HttpGet]
        public async Task<IActionResult> GetAllMedicos()
        {
            // Retorna un resultado exitoso (200 OK) con la lista de todos los médicos
            return Ok(await _medicoRepository.GetAllMedicos());
        }

        // Endpoint HTTP GET para obtener un médico por su ID
        [HttpGet("{idMedico}")]
        public async Task<IActionResult> GetMedicoById(int idMedico)
        {
            // Retorna un resultado exitoso (200 OK) con el médico correspondiente al ID
            return Ok(await _medicoRepository.GetMedicoById(idMedico));
        }

        // Endpoint HTTP POST para crear un nuevo médico
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] Medico medico)
        {
            // Verifica si el médico recibido es nulo y retorna un BadRequest si es así
            if (medico == null)
                return BadRequest();

            // Verifica si el modelo del médico es válido y retorna un BadRequest si no lo es
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserta el nuevo médico y retorna una respuesta de Created con la ubicación del nuevo recurso
            var nuevoMedicoId = await _medicoRepository.InsertMedico(medico);
            return Created($"api/Medico/{nuevoMedicoId}", nuevoMedicoId);
        }

        // Endpoint HTTP PUT para actualizar un médico por su ID
        [HttpPut("{idMedico}")]
        public async Task<IActionResult> UpdateMedico(int idMedico, [FromBody] Medico medico)
        {
            // Verifica si el médico o el ID en el cuerpo no son válidos y retorna un BadRequest si es así
            if (medico == null || idMedico != medico.IdMedico)
                return BadRequest();

            // Verifica si el modelo del médico es válido y retorna un BadRequest si no lo es
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Actualiza el médico y retorna una respuesta de NoContent
            await _medicoRepository.UpdateMedico(medico);
            return NoContent();
        }

        // Endpoint HTTP DELETE para eliminar un médico por su ID
        [HttpDelete("{idMedico}")]
        public async Task<IActionResult> DeleteMedico(int idMedico)
        {
            // Elimina el médico y retorna una respuesta de NoContent
            await _medicoRepository.DeleteMedico(idMedico);
            return NoContent();
        }
    }
}
