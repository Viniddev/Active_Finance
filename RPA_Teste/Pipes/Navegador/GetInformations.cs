using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static void Buscar(ChromeDriver driver)
        {

            List<string> acoes = new List<string>() 
            {
                "xpml11",
                "mxrf11"
            };

            foreach (var acao in acoes) 
            {
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl($"https://www.fundsexplorer.com.br/funds/{acao}");
                Indicadores indicadores = new Indicadores();

                var price = driver.FindElement(By.XPath(@".//div[@class='headerTicker__content__price']/p"));
                var elementosIndicadores = driver.FindElements(By.XPath("//*[@id='indicators']/div/p[2]/b"));

                IList<IWebElement> listaElementos = elementosIndicadores.ToList();

                indicadores.Price = price.Text;
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

                string mensagem = $"Ação: {acao.ToUpper()}; \n Price: {indicadores.Price}; \n Liquidez Media Diária: {indicadores.LiquidezMediaDiaria}; \n Ultimo Rendimento: {indicadores.UltimoRendimento}; ";


                Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
            }
        }
    }
}