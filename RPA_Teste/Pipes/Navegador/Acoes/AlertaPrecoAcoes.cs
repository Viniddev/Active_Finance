using RPA_Teste.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Navegador.Acoes
{
    internal class AlertaPrecoAcoes
    {
        public static void CreateAlert() 
        {
            //FAZ A CONSULTA
            ConectionDb conn = new ConectionDb();
            var TabelaAnalitics = conn.Select(Consultas.GetPriceAnalitics()).Tables[0];

            string AlertMessage = $"\U0001F6A9 PREÇOS FAVORÁVEIS \U0001F6A9 \n\n";

            foreach (DataRow datarow in TabelaAnalitics.Rows) 
            {
                if (Convert.ToDecimal(datarow[2]) != 0)
                {
                    if (Convert.ToDecimal(datarow[1]) <= Convert.ToDecimal(datarow[2]))
                        AlertMessage += $"\u2705 {datarow[0].ToString()} \n" +
                                        $" • Preço Atual: {datarow[1].ToString()} \n" +
                                        $" • Preço Desejado: {datarow[2].ToString()} \n";
                }
            }
            Telegram.TelegramApi.SendMessageAsync(AlertMessage).Wait();

            //SE O RETORNO TIVER ALGUEM QUE É MENOR OU IGUAL MANDA MENSAGEM NO TELEGRAM
            //SE NÃO TIVER, IGNORA
        }
    }
}
