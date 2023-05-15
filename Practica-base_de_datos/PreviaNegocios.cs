using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Practica_base_de_datos
{
    class PreviaNegocios
    {
        public List<InscripcionPrevia> ListaDatos() // dev una lista 
        {
            List<InscripcionPrevia> lista = new List<InscripcionPrevia>();//instancia de lista de datos
            //instacias nesesarias para entrar a una base de datos 
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;                    //conexion en modo conectado

            try
            {
                //cadena de conexion---- 4 pasos para tener disponible mi base de datos
                conexion.ConnectionString= "data source=.\\SQLEXPRESS; initial catalog=JARDIN_DB;integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Id,Nombre,Apellido,Telefono,Direccion from INSCRIPCION";
                comando.Connection = conexion;

                conexion.Open(); //abro mi conexion

                //lectura de base de Datos
                lector = comando.ExecuteReader();//tengo tabla virtual en memoria

                while (lector.Read()) //recorro mi copia de db en memoria 
                {
                    InscripcionPrevia aux = new InscripcionPrevia(); //variable auxiliar
                    aux.Id = (int)lector["Id"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Apellido= (string)lector["Apellido"];
                    aux.Telefono= (string)lector["Telefono"];
                    aux.Direccion= (string)lector["Direccion"];

                    lista.Add(aux); //cada vuelta de ciclo agrega una coleccion

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (lector!=null)
                {
                    lector.Close(); //cierro la tabla virtual
                }
                conexion.Close(); // cierro mi conexion
            }





           
        }
    }
}
