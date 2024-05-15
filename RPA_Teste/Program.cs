using OpenQA.Selenium.Chrome;
using RPA_Teste.Pipes.Excel;
using RPA_Teste.Pipes.Navegador;
using RPA_Teste.Pipes.Navegador.Acoes;
using RPA_Teste.Pipes.Navegador.FundosImobiliarios;
using RPA_Teste.Pipes.Telegram;

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
            do
            {
                try
                {
                    if (Aplication.EhPeriodoUtil())
                    {
                        ReadNews.ReadExcel();
                        ReadConfigs.Read();

                        ChromeDriver driver = Launch.LaunchNavegador();
                        Task Cont = Aplication.Contador();
                        Task CloseBtn = Aplication.ClosePopUp(driver);

                        BuscarFundos.Buscar(driver);
                        BuscarAcoes.Buscar(driver);

                        if (ReadConfigs.EnviarEtapaLogTxt) 
                        {
                            EnviarLogTextFundos.Enviar();
                            EnviarLogTextAcoes.Enviar();
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
                        
                        TelegramApi.SendMessageAsync(" \u2705 Extraction Concluded.").Wait();
                    }

                    Aplication.ExecucaoFinalizou = true;
                }
                catch (Exception ex)
                {
                    string TextError = ex.Message.ToString();
                    contadorErros++;
                    Console.ReadLine();

                    switch (TextError)
                    {
                        case string erro when erro.Contains("Nao conectou ao BD"):
                            Console.WriteLine("Não conectou ao BD");
                            break;
                        default:
                            Console.WriteLine("ERRO :: " + ex.ToString());
                            break;
                    }
                }
            } while (!Aplication.ExecucaoFinalizou && contadorErros < 10);

            Aplication.KillChromeDriver();
            Environment.Exit(0);
        }
    }
}