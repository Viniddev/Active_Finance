using RPA_Teste.Pipes.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Navegador.Acoes
{
    internal class EnviarLogTextAcoes
    {
        public static void Enviar() 
        {
            TelegramApi.SendMessageAsync(BuscarAcoes.Mensagem).Wait();
            TelegramApi.SendLogText(BuscarAcoes.LogTxt, "Acoes").Wait();
        }
    }
}
