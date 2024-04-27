using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;
using Telegram.Bot.Types;
using System.Globalization;
using System.Collections.Generic;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static List<IndicadoresFundoImobiliario> BuscarFundosImobiliarios(ChromeDriver driver)
        {
            List<IndicadoresFundoImobiliario> ListaDeFundos = new List<IndicadoresFundoImobiliario>();
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
                driver.Navigate().GoToUrl($"https://statusinvest.com.br/fundos-imobiliarios/{fundo}");
                IndicadoresFundoImobiliario indicadoresFundo = new IndicadoresFundoImobiliario();
                var elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class = 'value']"));

                indicadoresFundo.ValorAtual = ReceberDados(elementosIndicadores[0].Text);
                indicadoresFundo.Min52Semanas = ReceberDados(elementosIndicadores[1].Text);
                indicadoresFundo.Max52Semanas = ReceberDados(elementosIndicadores[2].Text);
                indicadoresFundo.DividendYeld = ReceberDados(elementosIndicadores[3].Text);
                indicadoresFundo.Valorizacao12Meses = ReceberDados(elementosIndicadores[4].Text);
                indicadoresFundo.ValPatrimonialPorCota = ReceberDados(elementosIndicadores[5].Text);
                indicadoresFundo.PVP = ReceberDados(elementosIndicadores[6].Text);
                indicadoresFundo.ValorEmCaixa = ReceberDados(elementosIndicadores[7].Text);
                var ultimoRendimento = driver.FindElement(By.XPath(".//strong[@class = 'value d-inline-block fs-5 fw-900'][1]"));
                indicadoresFundo.UltimoRendimento = ReceberDados(ultimoRendimento.Text);
                elementosIndicadores = driver.FindElements(By.XPath(".//b[@class = 'sub-value fs-4 lh-3']"));
                indicadoresFundo.Rendimento = ReceberDados(elementosIndicadores[0].Text);
                indicadoresFundo.CotacaoBase = ReceberDados(elementosIndicadores[1].Text);
                indicadoresFundo.DataBase = ConvertDate(elementosIndicadores[2].Text);
                indicadoresFundo.DataPagamento = ConvertDate(elementosIndicadores[3].Text);


                mensagem += $"\u2705  Fundo: {fundo.ToUpper()}; \n" +
                            $" Valor Atual: R${indicadoresFundo.ValorAtual}; \n" +
                            $" Min(52) Semanas: R${indicadoresFundo.Min52Semanas}; \n" +
                            $" Max(52) Semanas: R${indicadoresFundo.Max52Semanas}; \n" +
                            $" DY: {indicadoresFundo.DividendYeld}%; \n" +
                            $" Valorização(12m): {indicadoresFundo.Valorizacao12Meses}%; \n" +
                            $" PVP: {indicadoresFundo.PVP}; \n" +
                            $" Ultimo Rendimento: R$ {indicadoresFundo.UltimoRendimento}; \n" +
                            $" Data Base: {indicadoresFundo.DataBase}; \n" +
                            $" Data Pagamento: {indicadoresFundo.DataPagamento};" +
                            $"\n\n";


                ListaDeFundos.Add(indicadoresFundo);
            }
            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();

            return ListaDeFundos;
        }

        public static List<IndicadoresAcoes> BuscarAcoes(ChromeDriver driver) 
        {

            List<IndicadoresAcoes> listaDeAcoes = new List<IndicadoresAcoes>();
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


                indicadoresAcoes.ValorAtual = ReceberDados(elementosIndicadores[0].Text);
                indicadoresAcoes.Min52Semanas = ReceberDados(elementosIndicadores[1].Text);
                indicadoresAcoes.Max52Semanas = ReceberDados(elementosIndicadores[2].Text);
                indicadoresAcoes.DividendYeld = ReceberDados(elementosIndicadores[3].Text);
                indicadoresAcoes.Valorizacao12Meses = ReceberDados(elementosIndicadores[4].Text);
                elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class='value d-block lh-4 fs-4 fw-700']"));
                indicadoresAcoes.PrecoLucro = ReceberDados(elementosIndicadores[1].Text);
                indicadoresAcoes.PrecoSobreValorPatrimonial = ReceberDados(elementosIndicadores[3].Text);
                indicadoresAcoes.ValorPatrimonialPorAcao = ReceberDados(elementosIndicadores[8].Text);
                indicadoresAcoes.LucroPorAcao = ReceberDados(elementosIndicadores[9].Text);
                indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido = ReceberDados(elementosIndicadores[14].Text);
                indicadoresAcoes.MargemBruta = ReceberDados(elementosIndicadores[20].Text);
                indicadoresAcoes.RetornoSobrePatrimonioLiquido = ReceberDados(elementosIndicadores[24].Text);


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


                listaDeAcoes.Add(indicadoresAcoes);
            }

            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
            return listaDeAcoes;
        }


        public static DateTime ConvertDate(string data) 
        {
            var dataFormatada = DateTime.Now;

            if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada))
                return dataFormatada;
            else 
                throw new Exception("Não foi possivel formatar data");
        }

        public static double ReceberDados(string value) 
        {
            value = value.ToString().Equals("-") || value.ToString().Equals("-%") ? "0" : value.ToString().Replace("%", "");

            if (double.TryParse(value, out double formatado))
                return formatado;
            else 
                throw new Exception("Não foi possivel formatar");
                
        }
    }
}