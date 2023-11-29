using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models; 
using EPE3_PUNTONET.Repositorio; 

namespace EPE3_PUNTONET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteRepository _pacienteRepository;

        // Constructor: Recibe una instancia de PacienteRepository mediante inyección de dependencias
        public PacienteController(PacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPacientes()
        {
            // Retorna un resultado exitoso (200 OK) con la lista de todos los pacientes
            return Ok(await _pacienteRepository.GetAllPacientes());
        }


        /// <param name="idPaciente">ID del paciente</param>
        [HttpGet("{idPaciente}")]
        public async Task<IActionResult> GetPacienteById(int idPaciente)
        {
            // Retorna un resultado exitoso (200 OK) con el paciente correspondiente al ID
            return Ok(await _pacienteRepository.GetPacienteById(idPaciente));
        }

   
        /// <param name="paciente">Datos del nuevo paciente</param>
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] Paciente paciente)
        {
            // Verifica si el paciente recibido es nulo y retorna un BadRequest si es así
            if (paciente == null)
                return BadRequest();

            // Verifica si el modelo del paciente es válido y retorna un BadRequest si no lo es
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Inserta el nuevo paciente y retorna una respuesta de Created con la ubicación del nuevo recurso
            var nuevoPacienteId = await _pacienteRepository.InsertPaciente(paciente);
            return Created($"api/Paciente/{nuevoPacienteId}", nuevoPacienteId);
        }

        /// <param name="idPaciente">ID del paciente a editar</param>
        /// <param name="paciente">Datos actualizados del paciente</param>
        [HttpPut("{idPaciente}")]
        public async Task<IActionResult> UpdatePaciente(int idPaciente, [FromBody] Paciente paciente)
        {
            // Verifica si el paciente o el ID en el cuerpo no son válidos y retorna un BadRequest si es así
            if (paciente == null || idPaciente != paciente.IdPaciente)
                return BadRequest();

            // Verifica si el modelo del paciente es válido y retorna un BadRequest si no lo es
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Actualiza el paciente y retorna una respuesta de NoContent
            await _pacienteRepository.UpdatePaciente(paciente);
            return NoContent();
        }

        /// <param name="idPaciente">ID del paciente a eliminar</param>
        [HttpDelete("{idPaciente}")]
        public async Task<IActionResult> DeletePaciente(int idPaciente)
        {
            // Elimina el paciente y retorna una respuesta de NoContent
            await _pacienteRepository.DeletePaciente(idPaciente);
            return NoContent();
        }
    }
}
