using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion
{
    public partial class Form1 : Form
    {

        private List<Pokemon> listaPokemons;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmPokemon agregar = new frmPokemon(); //declaro na variable tipo frmPokemon
            agregar.ShowDialog();
            Form1_Load(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string imagen = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/001.png";


            try
            {
                PokemonNegocio pokemonNegocio = new PokemonNegocio();

                //listaPokemons = pokemonNegocio.Listar();
                // listaPokemons = pokemonNegocio.Listar2();
                listaPokemons = pokemonNegocio.Listar3();

                dgvPokemons.DataSource = listaPokemons;

                //Oculto Columnas de la grilla.

                //Puedo poner el indice de la columna o el nombre de la propiedad.
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["Ficha"].Visible = false;
                dgvPokemons.Columns["Id"].Visible = false;
                //dgvPokemons.Columns["Debilidad"].Visible = false;
                dgvPokemons.Columns["Evolucion"].Visible = false;

                RecargarImg(imagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void dgvPokemons_MouseClick(object sender, MouseEventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            RecargarImg(seleccionado.UrlImagen);
        }

        private void RecargarImg(string img)
        {
            
            try
            {
                if(img!="")
                pbxPokemon.Load(img);
            }
            catch (Exception )
            {

                MessageBox.Show("debes cargar una imagen");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //El pokemon seleccionado en la grilla(dvgPokemons) lo meto completo en una variable de objeto
            Pokemon elemento = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
           
            frmPokemon modificar = new frmPokemon(elemento); //uso constructor para pasar elemento a modificar
            modificar.ShowDialog();
            Form1_Load(sender, e);
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //El pokemon seleccionado en la grilla(dvgPokemons) lo meto completo en una variable de objeto
            Pokemon elemento = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
           
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
               if( MessageBox.Show("Eliminar", "Se eliminara el Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    negocio.eliminar(elemento.Id);
                    MessageBox.Show("Eliminado Correctamente");
                    Form1_Load(sender, e);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            frmPokemon detalle = new frmPokemon(seleccionado,true);
            detalle.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtFiltro.Text!="")
            {
                //lista con filtro ----variable labmda--- igual a lo que tenga la textbox// x.Nombre==txtFiltro.Text
                List<Pokemon> listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                dgvPokemons.DataSource = null;     //...... x.Nombre.Contains(txtFiltro.Text)); ....contiene alguna letra              
                dgvPokemons.DataSource = listaFiltrada; // ----Nombre.ToUpper() paso a mayusculas
            }
            else 
            {
                dgvPokemons.DataSource = null;
                dgvPokemons.DataSource = listaPokemons;
            }                    
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFiltro.Text != "" && txtFiltro.Text.Length>=2)
            {
                //lista con filtro ----variable labmda--- igual a lo que tenga la textbox// x.Nombre==txtFiltro.Text
                List<Pokemon> listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                dgvPokemons.DataSource = null;     //...... x.Nombre.Contains(txtFiltro.Text)); ....contiene alguna letra              
                dgvPokemons.DataSource = listaFiltrada; // ----Nombre.ToUpper() paso a mayusculas
            }
            else
            {
                dgvPokemons.DataSource = null;
                dgvPokemons.DataSource = listaPokemons;
            }
        }
    }
}
