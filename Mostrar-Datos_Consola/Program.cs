using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;
namespace Mostrar_Datos_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pokemon> lista; //variable tipo lista de pokemon

            PokemonNegocio negocio = new PokemonNegocio();  //intancia de variable

            lista = negocio.Listar();

            foreach (Pokemon pokemon in lista)
            {
                Console.Write(pokemon.Nombre +"  ");
                Console.WriteLine(pokemon.Descripcion);

            }
            Console.ReadKey();


        }
    }
}
