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
                "XPML11",
                "MXRF11",
                "BVAR11",
                "SNCI11",
                "BTRA11"
            };
            string emojiVerde = char.ConvertFromUtf32(0x1F7E2);
            string mensagem = $"   {emojiVerde} FUNDOS IMOBILIÁRIOS \n\n";

            foreach (var fundo in fundosImobiliarios) 
            {
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl($"https://statusinvest.com.br/fundos-imobiliarios/{fundo}");
                IndicadoresFundoImobiliario indicadoresFundo = new IndicadoresFundoImobiliario();
                var elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class = 'value']"));

                indicadoresFundo.ValorAtual = elementosIndicadores[0].Text;
                indicadoresFundo.Min52Semanas = elementosIndicadores[1].Text;
                indicadoresFundo.Max52Semanas = elementosIndicadores[2].Text;
                indicadoresFundo.DividendYeld = elementosIndicadores[3].Text;
                indicadoresFundo.Valorizacao12Meses = elementosIndicadores[4].Text;
                indicadoresFundo.ValPatrimonialPorCota = elementosIndicadores[5].Text;
                indicadoresFundo.PVP = elementosIndicadores[6].Text;
                indicadoresFundo.ValorEmCaixa = elementosIndicadores[7].Text;
                var ultimoRendimento = driver.FindElement(By.XPath(".//strong[@class = 'value d-inline-block fs-5 fw-900'][1]"));                
                indicadoresFundo.UltimoRendimento = ultimoRendimento.Text;
                elementosIndicadores = driver.FindElements(By.XPath(".//b[@class = 'sub-value fs-4 lh-3']"));
                indicadoresFundo.Rendimento = elementosIndicadores[0].Text;
                indicadoresFundo.CotacaoBase = elementosIndicadores[1].Text;
                indicadoresFundo.DataBase = elementosIndicadores[2].Text;
                indicadoresFundo.DataPagamento = elementosIndicadores[3].Text;


                mensagem += $"\u2705  Fundo: {fundo.ToUpper()}; \n" +
                            $" Valor Atual: R${indicadoresFundo.ValorAtual}; \n" +
                            $" Min(52) Semanas: R${indicadoresFundo.Min52Semanas}; \n" +
                            $" Max(52) Semanas: R${indicadoresFundo.Max52Semanas}; \n" +
                            $" DY: {indicadoresFundo.DividendYeld}%; \n" +
                            $" Valorização(12m): {indicadoresFundo.Valorizacao12Meses}; \n" +
                            $" PVP: {indicadoresFundo.PVP}; \n" +
                            $" Ultimo Rendimento: R$ {indicadoresFundo.UltimoRendimento}; \n" +
                            $" Data Base: {indicadoresFundo.DataBase}; \n" +
                            $" Data Pagamento: {indicadoresFundo.DataPagamento};" +
                            $"\n\n";


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
                "MGLU3",
                "ITSA3"
            };
            string emojiVermelho = char.ConvertFromUtf32(0x1F534);
            string mensagem = $"  {emojiVermelho} AÇÕES \n\n";

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

                elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class='value d-block lh-4 fs-4 fw-700']"));
                indicadoresAcoes.PrecoLucro = elementosIndicadores[1].Text;
                indicadoresAcoes.PrecoSobreValorPatrimonial = elementosIndicadores[3].Text;
                indicadoresAcoes.ValorPatrimonialPorAcao = elementosIndicadores[8].Text;
                indicadoresAcoes.LucroPorAcao = elementosIndicadores[9].Text;
                indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido = elementosIndicadores[14].Text;
                indicadoresAcoes.MargemBruta = elementosIndicadores[20].Text;
                indicadoresAcoes.RetornoSobrePatrimonioLiquido = elementosIndicadores[24].Text;


                mensagem += $"\U0001F6A9 Ativo: {acao.ToUpper()}; \n" +
                            $" Valor Atual: R${indicadoresAcoes.ValorAtual}; \n" +
                            $" VPA: R${indicadoresAcoes.ValorPatrimonialPorAcao}; \n" +
                            $" Min(52) Semanas: R${indicadoresAcoes.Min52Semanas}; \n" +
                            $" Max(52) Semanas: R${indicadoresAcoes.Max52Semanas}; \n" +
                            $" DY: {indicadoresAcoes.DividendYeld}%; \n" +
                            $" PL: {indicadoresAcoes.PrecoLucro}; \n" +
                            $" LPA: {indicadoresAcoes.LucroPorAcao}; \n" +
                            $" DÍV. LÍQUIDA/PL: {indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido}; \n" +
                            $" Margem Bruta: {indicadoresAcoes.MargemBruta}; \n" +
                            $" ROE: {indicadoresAcoes.RetornoSobrePatrimonioLiquido};" +
                            $"\n\n";
            }

            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
        }
    }
}