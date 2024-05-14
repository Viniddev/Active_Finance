using DocumentFormat.OpenXml.Bibliography;
using OpenQA.Selenium.Chrome;
using RPA_Teste.Pipes.Excel;
using RPA_Teste.Pipes.Navegador;
using RPA_Teste.Pipes.Navegador.Acoes;
using RPA_Teste.Pipes.Navegador.FundosImobiliarios;
using RPA_Teste.Pipes.Telegram;
using System.Diagnostics;

/*
    Espaço para estudar sobre automação
    criado em 23/12/2023 por Vinícius Dias
*/

namespace RPA_Teste
{
    public class Program
    {
        public static bool ExecucaoFinalizou { get; set; } = false;
        public static async Task Main(string[] args)
        {
            int contadorErros = 0;
            do
            {
                try
                {
                    ReadNews.ReadExcel();
                    Console.ReadLine();

                    if (true)
                    {

                        ChromeDriver driver = Launch.LaunchNavegador();
                        Task Cont = Aplication.Contador();
                        Task CloseBtn = Aplication.ClosePopUp(driver);

                        BuscarFundos.Buscar(driver);
                        BuscarAcoes.Buscar(driver);
                        AlertaPrecoFundos.CreateAlert();
                        AlertaPrecoAcoes.CreateAlert();

                        TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded, Chefão.").Wait();
                    }
                    else 
                    {
                        TelegramApi.SendMessageAsync(" \U0001f6d1 Fora Do Horário util").Wait();
                    }


                    Program.ExecucaoFinalizou = true;
                }
                catch (Exception ex)
                {
                    string TextError = ex.Message.ToString();
                    contadorErros++;
                    Console.ReadLine();

                    switch (TextError)
                    {
                        case string erro when erro.Contains("Não conectou ao BD"):
                            Console.WriteLine("Não conectou ao BD");
                            break;
                        default:
                            Console.WriteLine("ERRO :: " + ex.ToString());
                            break;
                    }
                }
            } while (!Program.ExecucaoFinalizou && contadorErros < 10);

            Aplication.KillChromeDriver();
            Environment.Exit(0);
        }
    }
}