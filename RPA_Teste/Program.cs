using OpenQA.Selenium.Chrome;
using RPA_Teste.Pipes.Navegador;
using RPA_Teste.Pipes.Navegador.Acoes;
using RPA_Teste.Pipes.Navegador.FundosImobiliarios;

/*
    Espaço para estudar sobre automação
    criado em 23/12/2023 por Vinícius Dias
*/

namespace RPA_Teste
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (Aplication.EhPeriodoUtil())
            {
                Task Cont = Aplication.Contador();
                ChromeDriver driver = Launch.LaunchNavegador();
                BuscarFundos.BuscarFundosImobiliarios(driver);
                BuscarAcoes.Buscar(driver);

                Telegram.TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded, Chefão.").Wait();
                Aplication.KillChromeDriver();
            }
        }
    }
}