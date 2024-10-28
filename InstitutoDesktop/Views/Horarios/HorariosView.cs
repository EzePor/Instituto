using InstitutoDesktop.ExtensionMethods;
using InstitutoDesktop.Util;
using InstitutoServices.Enums;
using InstitutoServices.Interfaces;
using InstitutoServices.Interfaces.Commons;
using InstitutoServices.Interfaces.Horarios;
using InstitutoServices.Models.Commons;
using InstitutoServices.Models.Horarios;
using InstitutoServices.Models.Inscripciones;
using InstitutoServices.Services.Commons;
using InstitutoServices.Services.Horarios;
using System.Data;
using System.Diagnostics;

namespace InstitutoDesktop.Views
{
    public partial class HorariosView : Form
    {
        IHorarioService horarioService = new HorarioService();
        IGenericService<CicloLectivo> cicloLectivoService = new GenericService<CicloLectivo>();
        IGenericService<Carrera> carreraService = new GenericService<Carrera>();
        IAnioCarreraService anioCarreraService = new AnioCarreraService();
        IMateriaService materiaService = new MateriaService();
        IGenericService<Docente> docenteService = new GenericService<Docente>();
        IGenericService<Hora> horaService = new GenericService<Hora>();
        IGenericService<Aula> aulaService = new GenericService<Aula>();

        BindingSource bindingHorarios = new BindingSource();
        List<CicloLectivo>? listaCicloLectivos = new List<CicloLectivo>();
        List<Carrera>? listaCarreras = new List<Carrera>();
        List<AnioCarrera>? listaAnioCarreras = new List<AnioCarrera>();
        List<Materia>? listaMaterias = new List<Materia>();
        List<Docente>? listaDocentes = new List<Docente>();
        List<Hora>? listaHoras = new List<Hora>();
        List<Horario>? listaHorarios = new List<Horario>();
        List<Aula>? listaAulas = new List<Aula>();
        Horario horarioCurrent;

        public HorariosView()
        {
            InitializeComponent();
            dataGridHorarios.DataSource = bindingHorarios;
            //tabPageAgregarEditar.Enabled= false;
            ObtenerListas();


        }

        private void CargarCombos()
        {
            cboCiclosLectivos.DataSource = listaCicloLectivos.ToList();
            cboCiclosLectivos.DisplayMember = "Nombre";
            cboCiclosLectivos.ValueMember = "Id";

            cboCarreras.DataSource = listaCarreras.ToList();
            cboCarreras.DisplayMember = "Nombre";
            cboCarreras.ValueMember = "Id";

            cboAniosCarreras.DataSource = listaAnioCarreras.Where(a => a.Carrera.Id.Equals((int)cboCarreras.SelectedValue)).ToList();
            cboAniosCarreras.DisplayMember = "Nombre";
            cboAniosCarreras.ValueMember = "Id";

            cboMaterias.DataSource = listaMaterias.Where(m => m.AnioCarreraId.Equals((int)cboAniosCarreras.SelectedValue)).ToList();
            cboMaterias.DisplayMember = "Nombre";
            cboMaterias.ValueMember = "Id";

            cboDocentes.DataSource = listaDocentes.ToList();
            cboDocentes.DisplayMember = "Nombre";
            cboDocentes.ValueMember = "Id";

            cboAulas.DataSource = listaAulas.ToList();
            cboAulas.DisplayMember = "Nombre";
            cboAulas.ValueMember = "Id";

            AutoCompleteStringCollection autoCompletadoDocentes = new AutoCompleteStringCollection();
            foreach (Docente docente in listaDocentes.ToList())
            {
                autoCompletadoDocentes.Add(docente.Nombre.ToString());
            }
            cboDocentes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboDocentes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboDocentes.AutoCompleteCustomSource = autoCompletadoDocentes;



            cboHoras.DataSource = listaHoras.ToList();
            cboHoras.DisplayMember = "Nombre";
            cboHoras.ValueMember = "Id";
            AutoCompleteStringCollection autoCompletadoHoras = new AutoCompleteStringCollection();
            foreach (Hora hora in listaHoras.ToList())
            {
                autoCompletadoHoras.Add(hora.Nombre.ToString());
            }
            cboHoras.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboHoras.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboHoras.AutoCompleteCustomSource = autoCompletadoHoras;


            cboDias.DataSource = Enum.GetValues(typeof(DiaEnum));

            CargarGrilla();
        }

        private async void ObtenerListas()
        {
            ShowInActivity.Show("Descargando ciclos lectivos, carreras y años, materias, docentes, horas y horarios");
            //pongo todos los métodos en paralelo para que se ejecuten al mismo tiempo
            var tareas = new List<Task>
            {
                Task.Run(async () => listaCicloLectivos = await cicloLectivoService.GetAllAsync()),
                Task.Run(async () => listaCarreras = await carreraService.GetAllAsync()),
                Task.Run(async () => listaAnioCarreras = await anioCarreraService.GetAllAsync()),
                Task.Run(async () => listaMaterias = await materiaService.GetAllAsync()),
                Task.Run(async () => listaDocentes = await docenteService.GetAllAsync()),
                Task.Run(async () => listaHoras = await horaService.GetAllAsync()),
                Task.Run(async () => listaHorarios = await horarioService.GetAllAsync()),
                Task.Run(async () => listaAulas = await aulaService.GetAllAsync())
            };
            bindingHorarios.DataSource = listaHorarios;
            //cuando terminan todas las tareas, cierro el showInActivity y cargo los combos
            await Task.WhenAll(tareas);
            ShowInActivity.Hide();
            CargarCombos();
        }


        private async Task CargarGrilla()
        {
            if (listaHorarios != null && listaHorarios.Count > 0)
                bindingHorarios.DataSource = listaHorarios.Where(h => h.CicloLectivoId.Equals((int)cboCiclosLectivos.SelectedValue) &&
                                                                h.Materia.AnioCarrera.CarreraId.Equals((int)cboCarreras.SelectedValue) &&
                                                                h.Materia.AnioCarreraId.Equals((int)cboAniosCarreras.SelectedValue));
            dataGridHorarios.OcultarColumnas(new string[] { "Id", "CicloLectivo", "DetallesHorario", "IntegrantesHorario", "CicloLectivoId", "Eliminado" });
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(tabPageAgregarEditar);
            horarioCurrent = new Horario();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboMaterias.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una materia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (horarioCurrent?.IntegrantesHorario?.Count == 0 && !horarioCurrent.Materia.EsRecreo)
            {
                MessageBox.Show("Debe definirse al menos un docente para el horario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (horarioCurrent?.DetallesHorario?.Count == 0)
            {
                MessageBox.Show("Debe definirse al menos una hora para el horario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                horarioCurrent.MateriaId = (int)cboMaterias.SelectedValue;
                horarioCurrent.CicloLectivoId = (int)cboCiclosLectivos.SelectedValue;
                horarioCurrent.CantidadHoras = horarioCurrent.DetallesHorario.Count;

                horarioCurrent.Materia = null;
                horarioCurrent.CicloLectivo = null;
                horarioCurrent.IntegrantesHorario.ToList().ForEach(i => i.Docente = null);
                horarioCurrent.DetallesHorario.ToList().ForEach(d => d.Hora = null);
                horarioCurrent.DetallesHorario.ToList().ForEach(d => d.Aula = null);

                if (horarioCurrent.Id == 0)
                {
                    await horarioService.AddAsync(horarioCurrent);
                }
                else
                {
                    await horarioService.UpdateAsync(horarioCurrent);
                }
            }
            await actualizarListaHorarios();
            await CargarGrilla();
            tabControl.SelectTab(tabPageLista);
        }

        private async Task actualizarListaHorarios()
        {
            ShowInActivity.Show("Actualizando lista de horarios");
            //pongo todos los métodos en paralelo para que se ejecuten al mismo tiempo
            var tareas = new List<Task>
            {
                Task.Run(async () => listaHorarios = await horarioService.GetAllAsync())
            };
            //cuando terminan todas las tareas, cierro el showInActivity y cargo los combos
            await Task.WhenAll(tareas);
            ShowInActivity.Hide();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            horarioCurrent = (Horario)bindingHorarios.Current;
            ActualizarTabAgregarEditar();
            tabControl.SelectTab(tabPageAgregarEditar);
        }

        private void ActualizarTabAgregarEditar()
        {
            cboMaterias.SelectedValue = horarioCurrent?.MateriaId ?? 0;
            dataGridDocentes.DataSource = horarioCurrent?.IntegrantesHorario ?? null;
            dataGridDocentes.OcultarColumnas(new string[] { "Horario", "HorarioId", "Id", "Eliminado" });
            dataGridHoras.DataSource = horarioCurrent?.DetallesHorario.OrderBy(d => d.Dia).ThenBy(d => d.Hora.Desde).ToList() ?? null;
            cboAulas.SelectedValue = horarioCurrent?.DetallesHorario.FirstOrDefault()?.AulaId ?? 0;
            dataGridHoras.OcultarColumnas(new string[] { "AulaId", "Horario", "HoraId", "HorarioId", "Id", "Eliminado" });
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            horarioCurrent = (Horario)bindingHorarios.Current;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el horario de {horarioCurrent.Materia.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ShowInActivity.Show("Eliminando horario");
                await horarioService.DeleteAsync(horarioCurrent.Id);
                ShowInActivity.Hide();
                await actualizarListaHorarios();
                await CargarGrilla();
            }
            horarioCurrent = null;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            horarioCurrent = null;
            ActualizarTabAgregarEditar();
            tabControl.SelectTab(tabPageLista);

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {

        }



        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
        }

        private void cboCarreras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCarreras.SelectedValue != null && cboCarreras.SelectedValue.GetType() == typeof(int))
            {
                cboAniosCarreras.DataSource = listaAnioCarreras.Where(a => a.Carrera.Id.Equals(cboCarreras.SelectedValue)).ToList();
            }
        }

        private void btnAgregarDocente_Click(object sender, EventArgs e)
        {
            var docente = (Docente)cboDocentes.SelectedItem;
            if (horarioCurrent.IntegrantesHorario.Any(d => d.DocenteId.Equals(docente.Id)))
            {
                MessageBox.Show("El docente ya se encuentra asignado al horario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            horarioCurrent.IntegrantesHorario.Add(new IntegranteHorario { DocenteId = docente.Id, Docente = docente });
            dataGridDocentes.DataSource = null;
            dataGridDocentes.DataSource = horarioCurrent.IntegrantesHorario;
            dataGridDocentes.OcultarColumnas(new string[] { "Horario", "HorarioId", "Id", "Eliminado" });

        }

        private void btnAgregarHora_Click(object sender, EventArgs e)
        {
            var hora = (Hora)cboHoras.SelectedItem;
            if (horarioCurrent.DetallesHorario.Any(d => d.HoraId.Equals(hora.Id) && d.Dia.Equals(cboDias.SelectedValue)))
            {
                MessageBox.Show($"La hora {hora.Nombre} en el día {cboDias.SelectedValue} ya se encuentra asignada al horario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var aula = (Aula)cboAulas.SelectedItem;
            if(DetalleHorarioEdit != null)
            {
                DetalleHorarioEdit.HoraId = hora.Id;
                DetalleHorarioEdit.Hora = hora;
                DetalleHorarioEdit.Dia = (DiaEnum)cboDias.SelectedValue;
                DetalleHorarioEdit.AulaId = aula.Id;
                DetalleHorarioEdit.Aula = aula;
                DetalleHorarioEdit.HorarioId = horarioCurrent.Id;
                DetalleHorarioEdit.Horario = horarioCurrent;
                DetalleHorarioEdit.Id = 0;
                DetalleHorarioEdit = null;
                btnAgregar.Text = "Agregar";
                var detalleABorrar = (DetalleHorario)dataGridHoras.CurrentRow.DataBoundItem;
                horarioCurrent.DetallesHorario.Remove(detalleABorrar);  
                horarioCurrent.DetallesHorario.Add(DetalleHorarioEdit);
            }
            else
            {
                horarioCurrent.DetallesHorario.Add(new DetalleHorario { HoraId = hora.Id, Hora = hora, Dia = (DiaEnum)cboDias.SelectedValue, HorarioId = horarioCurrent.Id, AulaId = aula.Id, Aula = aula });
            }

            horarioCurrent.DetallesHorario.Add(new DetalleHorario { HoraId = hora.Id, Hora = hora, Dia = (DiaEnum)cboDias.SelectedValue, HorarioId = horarioCurrent.Id, AulaId = aula.Id, Aula = aula });
            dataGridHoras.DataSource = null;
            dataGridHoras.DataSource = horarioCurrent.DetallesHorario.OrderBy(d => d.Dia).ThenBy(d => d.Hora.Desde).ToList();
            dataGridHoras.OcultarColumnas(new string[] { "AulaId", "Horario", "HoraId", "HorarioId", "Id", "Eliminado" });



        }

        private void btnQuitarDocente_Click(object sender, EventArgs e)
        {
            if (dataGridDocentes.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un docente para quitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var integranteHorario = (IntegranteHorario)dataGridDocentes.CurrentRow.DataBoundItem;
            horarioCurrent.IntegrantesHorario.Remove(horarioCurrent.IntegrantesHorario.First(d => d.Id.Equals(integranteHorario.Id)));
            dataGridDocentes.DataSource = null;
            dataGridDocentes.DataSource = horarioCurrent.IntegrantesHorario;
            dataGridDocentes.OcultarColumnas(new string[] { "Horario", "HorarioId", "Id", "Eliminado" });


        }

        private void btnQuitarHora_Click(object sender, EventArgs e)
        {
            if (dataGridHoras.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una hora para quitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var detalleHorario = (DetalleHorario)dataGridHoras.CurrentRow.DataBoundItem;
            horarioCurrent.DetallesHorario.Remove(horarioCurrent.DetallesHorario.First(h => h.Id.Equals(detalleHorario.Id)));
            dataGridHoras.DataSource = null;
            dataGridHoras.DataSource = horarioCurrent.DetallesHorario;
            dataGridHoras.OcultarColumnas(new string[] { "Horario", "HoraId", "HorarioId", "Id", "Eliminado" });
        }

        private void cboAniosCarreras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAniosCarreras.SelectedValue != null && cboAniosCarreras.SelectedValue.GetType() == typeof(int))
            {
                cboMaterias.DataSource = listaMaterias.Where(m => m.AnioCarreraId.Equals(cboAniosCarreras.SelectedValue)).ToList();
                CargarGrilla();
            }
        }

        private void iconButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridHoras.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una hora para quitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var detalleHorario = (DetalleHorario)dataGridHoras.CurrentRow.DataBoundItem;
        }
    }
}
