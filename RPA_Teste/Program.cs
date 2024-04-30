using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Teste;
using RPA_Teste.Models;
using RPA_Teste.Pipes.Excel;
using RPA_Teste.Pipes.Navegador;
using Telegram.Bot.Types;

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
                GetInformations.BuscarFundosImobiliarios(driver);
                GetInformations.BuscarAcoes(driver);
                Telegram.TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded, Chefão.").Wait();
                Aplication.KillChromeDriver();
            }
        }
    }
}