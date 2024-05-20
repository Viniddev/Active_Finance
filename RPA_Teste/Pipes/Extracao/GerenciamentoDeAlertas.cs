using RPA_Teste.Pipes.Excel;
using RPA_Teste.Pipes.Navegador.Acoes;
using RPA_Teste.Pipes.Navegador.FundosImobiliarios;
using RPA_Teste.Pipes.Telegram;
using RPA_Teste.Pipes.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Extracao
{
    internal class GerenciamentoDeAlertas
    {
        public static void Send() 
        {

            if (ReadConfigs.EnviarEtapaMensagem)
            {
                TelegramApi.SendMessageAsync(BuscarAcoes.Mensagem).Wait();
                TelegramApi.SendMessageAsync(BuscarFundos.Mensagem).Wait();
            }

            if (ReadConfigs.EnviarEtapaLogTxt) 
            {
                TelegramApi.SendLogText(BuscarAcoes.LogTxt, "Acoes").Wait();
                TelegramApi.SendLogText(BuscarFundos.LogTxt, "Fundos").Wait();
            }

            if (ReadConfigs.EnviarEtapaAlertaPrecos)
            {
                AlertaPrecoFundos.CreateAlert();
                AlertaPrecoAcoes.CreateAlert();
            }

            if (ReadConfigs.EnviarEtapaLogExcel)
            {
                BuildLogExcel.Montar();
            }

            if (ReadConfigs.EnviarEtapaWord) 
            {
                WriteWordDoc.Write(BuscarFundos.LogTxt, BuscarAcoes.LogTxt);
            }
        }
    }
}
