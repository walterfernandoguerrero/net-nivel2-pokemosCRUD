using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_base_de_datos
{
    public partial class Form1 : Form
    {
        List<InscripcionPrevia> miListaPrevia = new List<InscripcionPrevia>();

        public Form1() 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PreviaNegocios Datos = new PreviaNegocios();
            miListaPrevia=(Datos.ListaDatos());


            dgvPreInscripcion.DataSource = miListaPrevia;


        }
        //boton de prueba para probar el objeto de mi clase 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            InscripcionPrevia Reg1 = new InscripcionPrevia();

            Reg1.Nombre = txtNombre.Text;
            Reg1.Apellido = txtApellido.Text;
            Reg1.Telefono = txtTelefono.Text;
            Reg1.Direccion = txtDireccion.Text;

            miListaPrevia.Add(Reg1);
            

            Migrilla();



        }
        private void Migrilla()
        {
            dgvPreInscripcion.DataSource = null;
            dgvPreInscripcion.DataSource = miListaPrevia;
        }

        private void dgvPreInscripcion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //variable de tipo coleccion casteada del datagriv
            InscripcionPrevia seleccion = (InscripcionPrevia)dgvPreInscripcion.CurrentRow.DataBoundItem;
            txtNombre.Text = seleccion.Nombre;
            txtApellido.Text = seleccion.Apellido;
            txtTelefono.Text = seleccion.Telefono;
            txtDireccion.Text = seleccion.Direccion;

        }
    }
}
