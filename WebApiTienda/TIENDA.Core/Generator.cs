using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Core
{
    public static class Generator
    {

        //Generar codigo
        public static string GenerarCodigo(int logintud)
        {
            Random al = new Random();
            string codigo = "";
            int n;
            for (int i = 0; i < logintud; i++)
            {
                n = al.Next(48, 57);

                codigo = codigo + (char)n;
            }
            return codigo;
        }

        //Generar Clave
        public static string CrearPassword(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        //Rellenar caracteres
        public static string RellenarCaracteres(this int numero, string caracter, int digitos)
        {
            var codigo = numero.ToString();
            var largo = codigo.Length;
            var ceros = digitos - largo;

            for (int i = 1; i <= ceros; i++)
            {
                codigo = caracter + codigo;
            }
            return codigo;
        }

    }
}
