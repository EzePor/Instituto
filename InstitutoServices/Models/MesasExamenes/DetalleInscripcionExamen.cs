using InstitutoServices.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstitutoServices.Models.MesasExamenes
{
    public class DetalleInscripcionExamen
    {
        public int Id { get; set; }
        public int InscripcionExamenId { get; set; }
        public InscripcionExamen? InscripcionExamen { get; set; }
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
        public bool Eliminado { get; set; } = false;

        public override string ToString()
        {
            return $"{Materia?.Nombre}" ?? $"Detalle inscripción {Id}";
        }
    }
}
