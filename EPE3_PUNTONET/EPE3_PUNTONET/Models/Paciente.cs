namespace EPE3_PUNTONET.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string NombrePac { get; set; }
        public string ApellidoPac { get; set; }
        public string RunPac { get; set; }
        public string Nacionalidad { get; set; }
        public string Visa { get; set; }
        public string Genero { get; set; }
        public string SintomasPac { get; set; }
        public int Medico_idMedico { get; set; }
    }
}
