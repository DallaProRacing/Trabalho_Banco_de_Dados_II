using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Banco_De_Dados
{
    internal class Conn
    {
        private static string server = @"DESKTOP-5JMIAIG\SQLEXPRESS";
        private static string dataBase = "AgenciaDallaRosa";
            
        public static string StrCon
        {
            get
            {
                return $"Data Source = {server}; Integrated Security = true; Initial Catalog = {dataBase}";
            }
        }
    }
}
