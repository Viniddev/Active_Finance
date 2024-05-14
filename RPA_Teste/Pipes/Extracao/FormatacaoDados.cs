using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Extracao
{
    public class FormatacaoDados
    {
        public static DateTime ConvertDate(string data)
        {
            var dataFormatada = DateTime.Now;

            if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada))
                return dataFormatada;
            else
                throw new Exception("Não foi possivel formatar data");
        }
        public static string ConvertDataToLogExcel(string data)
        {
            var dataFormatada = DateTime.Now;

            if (DateTime.TryParseExact(data, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada))
                return dataFormatada.ToString("dd/MM/yyyy");
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
        public static decimal ConvertToDecimal(string value)
        {
            value = !value.Equals("0.00") ? value.Replace(".", ",") : value;

            if (decimal.TryParse(value, out decimal formatado))
                return formatado;
            else
                throw new Exception("Não foi possivel formatar");
        }
    }
}
