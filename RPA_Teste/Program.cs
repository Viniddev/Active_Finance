using DocumentFormat.OpenXml.Bibliography;
using OpenQA.Selenium.Chrome;
using RPA_Teste.Pipes.Navegador;
using RPA_Teste.Pipes.Navegador.Acoes;
using RPA_Teste.Pipes.Navegador.FundosImobiliarios;
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

                    if (Aplication.EhPeriodoUtil())
                    {
                        ChromeDriver driver = Launch.LaunchNavegador();

                        Task Cont = Aplication.Contador();
                        Task CloseBtn = Aplication.ClosePopUp(driver);

                        BuscarFundos.Buscar(driver);
                        AlertaPrecoFundos.CreateAlert();

                        BuscarAcoes.Buscar(driver);
                        AlertaPrecoAcoes.CreateAlert();

                        Telegram.TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded, Chefão.").Wait();
                    }

                    Program.ExecucaoFinalizou = true;
                    Console.WriteLine("Process Concluded");
                }
                catch (Exception ex)
                {
                    string TextError = ex.Message.ToString();
                    contadorErros++;

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
            Process processo = Process.GetCurrentProcess();
            processo.CloseMainWindow();
            processo.WaitForExit();

            Environment.Exit(0);
        }
    }
}