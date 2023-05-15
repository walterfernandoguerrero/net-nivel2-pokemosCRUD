using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;



namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> Listar()    //devuelve una lista 
        {
            // conexion a datos  "MODO CONECTADO" 
            
            //Instancia
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector=null;

            // 
            try
            {
                //cadena de conexion a Base de Datos---------
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB;integrated security=sspi";
                // ejecutar en el motor de base de datos SQL, un string con la sentencia sql
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Numero, Nombre, Descripcion, UrlImagen from POKEMONS";
                comando.Connection = conexion;

                conexion.Open();

                // ejecutar una lectura contra la base de datos
                lector = comando.ExecuteReader();// tabla virtual en memoria con los campos 

                while (lector.Read()) //recorro el lector de memoria y lo guardo en mi lista de objetos
                {
                    Pokemon aux = new Pokemon();
                   
                    aux.Numero = (int)lector["Numero"];
                    aux.Nombre = (string)lector["Nombre"];//se puede cargar el campo con esta linea 
                    aux.Descripcion = lector.GetString(2);// o con esta otra 
                    aux.UrlImagen = lector.GetString(3);

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {

                 throw ex;
            }
            finally
            {
               if (lector != null) //si el lector es distinto de nulo cierro el lecto               
                    lector.Close();//cierro el lector
                conexion.Close();
            }


           
        }
        public List<Pokemon> Listar2()
        {
            // conexion a datos  "MODO CONECTADO" 

            //Instancia
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            // 
            try
            {
                //cadena de conexion
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB;integrated security=sspi";
                // ejecutar en el motor de base de datos SQL, un string con la sentencia sql
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select P.Id, P.Numero, Nombre, P.Descripcion, " +
                    "UrlImagen, T.Descripcion as Tipo, D.Descripcion as Debilidad " +
                    "from POKEMONS P inner join ELEMENTOS T on P.idTipo =T.IdElemento inner join ELEMENTOS D on P.idDebilidad =D.IdElemento";
                comando.Connection = conexion;

                conexion.Open();

                // ejecutar una lectura contra la base de datos
                lector = comando.ExecuteReader();// tabla virtual en memoria con los campos 

                while (lector.Read()) //recorro el lector de memoria y lo guardo en mi lista de objetos
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = (int)lector["Numero"];
                    aux.Nombre = (string)lector["Nombre"];//se puede cargar el campo con esta linea 
                    aux.Descripcion = lector.GetString(3);// o con esta otra 
                    aux.UrlImagen = lector.GetString(4);
                    aux.Tipo = new Elemento((string)lector["Tipo"]);
                    aux.Debilidad=new Elemento((string)lector["Debilidad"]);

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (lector != null) //si el lector es distinto de nulo cierro el lecto               
                    lector.Close();//cierro el lector
                conexion.Close();
            }



        }
       
        public List<Pokemon> Listar3() 
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select P.Id, P.Numero, Nombre, P.Descripcion," +
                    " UrlImagen, T.Descripcion as Tipo, D.Descripcion as Debilidad, " +
                    "T.Id as IdTipo, " +
                    "D.Id as IdDebilidad" +
                    " from POKEMONS P inner join ELEMENTOS T on P.idTipo =T.Id inner join ELEMENTOS D on P.idDebilidad =D.Id");
                datos.ejecutarLectura();
                SqlDataReader lector = datos.Lector;
                while (lector.Read()) //recorro el lector de memoria y lo guardo en mi lista de objetos
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)lector["Id"];
                    aux.Numero = (int)lector["Numero"];
                    aux.Nombre = (string)lector["Nombre"];//se puede cargar el campo con esta linea 
                    aux.Descripcion = lector.GetString(3);// o con esta otra "descropcion"
                    aux.UrlImagen = lector.GetString(4);// url imagen
                    aux.Tipo = new Elemento((string)lector["Tipo"]);
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Debilidad = new Elemento((string)lector["Debilidad"]);
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        
        public void agregar(Pokemon nuevo) 
        {
            AccesoDatos datos = new AccesoDatos();//base,conexion,comando
            try
            {
                //consulta SQL en este caso para grabar una accion
                datos.setearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, UrlImagen, idTipo, idDebilidad)values("+nuevo.Numero+",'"+nuevo.Nombre+"','"+nuevo.Descripcion+"','"+nuevo.UrlImagen+"',"+nuevo.Tipo.Id+","+nuevo.Debilidad.Id+")");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void modificar(Pokemon modificar) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @desc, UrlImagen = @url, idTipo = @IdTipo, idDebilidad = @IdDebilidad where Id = @Id");
                datos.agregarParametro("@numero", modificar.Numero);
                datos.agregarParametro("@nombre", modificar.Nombre);
                datos.agregarParametro("@desc", modificar.Descripcion);
                datos.agregarParametro("@url", modificar.UrlImagen);
                datos.agregarParametro("@idTipo", modificar.Tipo.Id);
                datos.agregarParametro("@idDebilidad", modificar.Debilidad.Id);
                datos.agregarParametro("@Id", modificar.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Pokemons where id =" + id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
