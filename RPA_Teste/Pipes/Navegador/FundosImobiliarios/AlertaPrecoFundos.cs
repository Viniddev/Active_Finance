using RPA_Teste.DataBase;
using RPA_Teste.Pipes.Telegram;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Navegador.FundosImobiliarios
{
    internal class AlertaPrecoFundos
    {
        public static void CreateAlert()
        {
            ConectionDb conn = new ConectionDb();
            var TabelaAnalitics = conn.Select(Consultas.GetPriceFundsAnalitics()).Tables[0];

            string AlertMessage = $"\U0001F6A9 FUNDOS FAVORÁVEIS \U0001F6A9 \n\n";

            foreach (DataRow datarow in TabelaAnalitics.Rows)
            {
                if (Convert.ToDecimal(datarow[2]) != 0)
                {
                    if (Convert.ToDecimal(datarow[1]) <= Convert.ToDecimal(datarow[2]))
                        AlertMessage += $"\u2705 {datarow[0].ToString()} \n" +
                                        $" • Preço Atual: R$ {datarow[1].ToString()} \n" +
                                        $" • Preço Desejado: R$ {datarow[2].ToString()} " +
                                        $"\n\n";
                }
            }
            TelegramApi.SendMessageAsync(AlertMessage).Wait();

        }
    }
}
