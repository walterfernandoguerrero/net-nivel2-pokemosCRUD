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
    public partial class frmPokemon : Form
    {
        private Pokemon pokemon = null; //variable para usar en todo el formulario
        private bool detalle = false;

        public frmPokemon()//constructor original
        {
            InitializeComponent();
        }

        public frmPokemon(Pokemon poke)//constructor 1 sobrecarga recibe un objeto pokemon
        {
            InitializeComponent();
            this.pokemon = poke;
            Text = "Modificar Pokemon";
        }
        public frmPokemon(Pokemon poke, bool detalle)//constructor 2 sobrecarga recibe un objeto pokemon y una variable bool
        {
            InitializeComponent();
            this.pokemon = poke;
            this.detalle = detalle;
            Text = "Detalle Pokemon";
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPokemon_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    if (MessageBox.Show("De verad querés salir? Perderás los datos", "Saliendo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
        //        return;

        //    Dispose();
        }

        private void frmPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();//clase donde esta mi consulta sql 


            cboTipo.DataSource = elementoNegocio.Listar();// origen de datos de mi combo
            cboTipo.ValueMember = "Id";// clave donde busca el combo
            cboTipo.DisplayMember = "Nombre";  //lo que se muestra en pantalla

            cboDebilidad.DataSource = elementoNegocio.Listar();//origen
            cboDebilidad.ValueMember = "Id";//clave
            cboDebilidad.DisplayMember = "Nombre";//pantalla

            try
            {
                if (pokemon != null)
                {
                    txtNombre.Text = pokemon.Nombre;
                    numNumero.Value = pokemon.Numero;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;

                    if (txtUrlImagen.Text != "")
                        pbxPokemon.Load(pokemon.UrlImagen);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            activarDesactivar();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
               // Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                if (pokemon == null) //si es nulo apreta agregar y me creo una variable 
                {
                    pokemon = new Pokemon(); //no poner  " Pokemon pokemon = new Pokemon();"

                }

                    pokemon.Nombre = txtNombre.Text;
                    pokemon.Numero = (int)numNumero.Value;
                    pokemon.Descripcion = txtDescripcion.Text;
                    pokemon.UrlImagen = txtUrlImagen.Text;
                    pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                    pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem;
                
                if(pokemon.Id==0)// si no tiene Id vino de Agregar
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Se agrego un nuevo pokemon");
                    Dispose();
                }
                else // si tiene Id vino de modificar
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificaste el pokemon");
                    Dispose();
                }
 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUrlImagen.Text != "")
                {
                    pbxPokemon.Load(txtUrlImagen.Text);
                }
            }
            catch (Exception )
            {

                MessageBox.Show("no es valida la url");
            }
            
        }
        private void activarDesactivar()
        {
            gbxPokemons.Enabled = !detalle;//trae true
            btnAceptar.Visible = !detalle;//true
            btnCancelar.Visible = !detalle;//true

        }
    }
}
