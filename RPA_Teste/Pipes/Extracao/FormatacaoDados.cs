using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Extracao
{
    internal class FormatacaoDados
    {
        public static DateTime ConvertDate(string data)
        {
            var dataFormatada = DateTime.Now;

            if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada))
                return dataFormatada;
            else
                throw new Exception("Não foi possivel formatar data");
        }

        public static double ReceberDados(string value)
        {
            value = value.ToString().Equals("-") || value.ToString().Equals("-%") ? "0" : value.ToString().Replace("%", "");

            if (double.TryParse(value, out double formatado))
                return formatado;
            else
                throw new Exception("Não foi possivel formatar");

        }
    }
}
