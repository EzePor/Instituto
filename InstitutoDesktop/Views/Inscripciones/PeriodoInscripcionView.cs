using InstitutoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstitutoDesktop.Views.Inscripciones
{
    public partial class PeriodoInscripcionView : Form
    {
       IGenericService<PeriodoInscripcion> periodoInscripcionService = new GenericService<PeriodoInscripcion>();
        public PeriodoInscripcionView()
        {
            InitializeComponent();
        }
    }
}
