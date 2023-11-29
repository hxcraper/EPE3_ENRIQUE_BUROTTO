namespace EPE3_PUNTONET.Models
{
    public class Medico
    {

        public int IdMedico { get; set; }
        public string NombreMed { get; set; }
        public string ApellidoMed { get; set; }
        public string RunMed { get; set; }
        public string Eunacom { get; set; }
        public string NacionalidadMed { get; set; }
        public string Especialidad { get; set; }
        public TimeSpan Horarios { get; set; }
        public int TarifaHr { get; set; }


    }
}
