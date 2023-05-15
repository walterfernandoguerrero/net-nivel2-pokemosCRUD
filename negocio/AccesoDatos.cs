using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
   public  class AccesoDatos
    {
        //objetos  como propiedades
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector = null;

    public AccesoDatos()//constructor
        {
            conexion = new SqlConnection("data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB;integrated security=sspi");
            comando = new SqlCommand();
        }
    public void setearConsulta(string consulta) // pido mi consulta pero todavia no una accion ni leer ni modificar ni borrar ni grabar 
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

// hasta aca las acciones son genericas para todas la acciones para todas las bases de Datos


        //ahora las acciones contra la base de datos 


    public void ejecutarLectura() //para leer una base 
        {
            comando.Connection = conexion;  // le doy la conexion al comando
            conexion.Open(); // abro la conexion
            lector = comando.ExecuteReader();
        }

    public  void ejecutarAccion() // esta es para agregar un nuevo registro
        {
            comando.Connection = conexion;  // le doy la conexion al comando
            conexion.Open(); // abro la conexion
            comando.ExecuteNonQuery();//ejecuta escritura 
        }

    public void agregarParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }


        public SqlDataReader Lector   //property
        {
            get { return lector; }
        }

    public void cerrarConexion() // tambien es generico
        {
            if(lector!=null)
            {
                lector.Close();
            }
            conexion.Close();
        }




    }//fin de la clase
}
