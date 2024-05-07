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
        public static async Task Main(string[] args)
        {
            int contadorErros = 0;
            bool execucaoFinalizou = false;

            do
            {
                try 
                {
                    if (Aplication.EhPeriodoUtil())
                    {
                        ChromeDriver driver = Launch.LaunchNavegador();

                        Task Cont = Aplication.Contador();
                        Task CloseBtn = Aplication.ClosePopUp(driver);

                        BuscarFundos.BuscarFundosImobiliarios(driver);
                        BuscarAcoes.Buscar(driver);

                        Telegram.TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded, Chefão.").Wait();

                    }
                    else 
                    {
                        Telegram.TelegramApi.SendMessageAsync(" \u2705 Não é período útil, Chefão.").Wait();
                    }
                    execucaoFinalizou = true;
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
            } while (!execucaoFinalizou && contadorErros < 10);

            Aplication.KillChromeDriver();
            Environment.Exit(0);

            Process processo = Process.GetCurrentProcess();
            processo.CloseMainWindow();
            processo.WaitForExit();
        }
    }
}