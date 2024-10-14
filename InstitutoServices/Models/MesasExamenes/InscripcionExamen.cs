using InstitutoServices.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstitutoServices.Models.MesasExamenes
{
   public class InscripcionExamen
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }
        public int CarreraId { get; set; }

        public Carrera? Carrera { get; set; }
        public int TurnoExamenId { get; set; }
        public TurnoExamen? TurnoExamen { get; set; }
        public bool Eliminado { get; set; } = false;

        public override string ToString()
        {
            return $"{Alumno?.ApellidoNombre} {Carrera?.Nombre}" ?? $"Inscripción {Id}";
        }
    }

}
