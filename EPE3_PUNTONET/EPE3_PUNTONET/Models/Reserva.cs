namespace EPE3_PUNTONET.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public string Especialidad { get; set; }
        public DateTime DiaReserva { get; set; }
        public int Paciente_idPaciente { get; set; }
    }
}
