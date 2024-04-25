using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static IReadOnlyCollection<IWebElement> Challenge(ChromeDriver driver)
        {
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl("https://www.fundsexplorer.com.br/funds/htmx11");
            Indicadores indicadores = new Indicadores();

            var elementosIndicadores = driver.FindElements(By.XPath("//*[@id='indicators']/div/p[2]/b"));
            IList<IWebElement> listaElementos = elementosIndicadores.ToList();

            indicadores.LiquidezMediaDiaria = elementosIndicadores[0].Text;
            indicadores.UltimoRendimento = elementosIndicadores[1].Text;
            indicadores.DividendYield = elementosIndicadores[2].Text;
            indicadores.PatrimonioLiquido = elementosIndicadores[3].Text;
            indicadores.ValorPatrimonial = elementosIndicadores[4].Text;
            indicadores.RentabilidadeNoMes = elementosIndicadores[5].Text;
            indicadores.PVP = elementosIndicadores[6].Text;
            indicadores.UltimoDividendo = elementosIndicadores[7].Text;
            indicadores.DividendYieldUltimoDividendo = elementosIndicadores[8].Text;
            indicadores.DivPorAcao = elementosIndicadores[9].Text;


            Telegram.TelegramApi.SendMessageAsync(indicadores.LiquidezMediaDiaria).Wait();

            Console.WriteLine(indicadores.LiquidezMediaDiaria);

            return elementosIndicadores;
        }
    }
}