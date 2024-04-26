using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;
using Telegram.Bot.Types;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static void BuscarFundosImobiliarios(ChromeDriver driver)
        {
            List<string> fundosImobiliarios = new List<string>() 
            {
                "xpml11",
                "mxrf11",
                "bvar11",
                "SNCI11"
            };
            string emojiStop = char.ConvertFromUtf32(0x1F6D1);
            string mensagem = $"   {emojiStop} FUNDOS IMOBILIÁRIOS \n\n";

            foreach (var fundo in fundosImobiliarios) 
            {
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl($"https://www.fundsexplorer.com.br/funds/{fundo}");
                IndicadoresFundoImobiliario indicadores = new IndicadoresFundoImobiliario();

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

                mensagem += $"\u2705 Fundo: {fundo.ToUpper()}; \n Preço: {indicadores.Price}; \n Liquidez Media Diária: {indicadores.LiquidezMediaDiaria}; \n Ultimo Rendimento: {indicadores.UltimoRendimento}; \n Rentabilidade No Mes: {indicadores.RentabilidadeNoMes} \n Ultimo Dividendo: {indicadores.UltimoDividendo} \n\n";

            }
                Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
        }

        public static void BuscarAcoes(ChromeDriver driver) 
        {
            Thread.Sleep(1000);
           
            List<string> acoes = new List<string>()
            {
                "VALE3",
                "ITUB4",
                "PETR4",
                "MGLU3"
            };
            string emojiStop = char.ConvertFromUtf32(0x1F6D1);
            string mensagem = $"  {emojiStop} AÇÕES \n\n";

            foreach (string acao in acoes) 
            {
                driver.Navigate().GoToUrl(@$"https://statusinvest.com.br/acoes/{acao}");
                var elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class = 'value']"));
                IndicadoresAcoes indicadoresAcoes = new IndicadoresAcoes();


                indicadoresAcoes.ValorAtual = elementosIndicadores[0].Text;
                indicadoresAcoes.Min52Semanas = elementosIndicadores[1].Text;
                indicadoresAcoes.Max52Semanas = elementosIndicadores[2].Text;
                indicadoresAcoes.DividendYeld = elementosIndicadores[3].Text;
                indicadoresAcoes.Valorizacao12Meses = elementosIndicadores[4].Text;

                mensagem += $"\U0001F6A9 Ativo: {acao.ToUpper()}; \n Valor Atual: R${indicadoresAcoes.ValorAtual}; \n Min 52 Semanas: R${indicadoresAcoes.Min52Semanas}; \n Max 52 Semanas: R${indicadoresAcoes.Max52Semanas}; \n Dividend Yeld: {indicadoresAcoes.DividendYeld}% \n Valorização 12 meses: {indicadoresAcoes.Valorizacao12Meses} \n\n";
            }

            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
        }
    }
}