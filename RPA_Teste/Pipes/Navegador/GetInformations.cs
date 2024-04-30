using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;
using Telegram.Bot.Types;
using System.Globalization;
using System.Collections.Generic;
using RPA_Teste.DataBase;
using System.Data;
using RPA_Teste.Pipes.Extracao;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static void BuscarFundosImobiliarios(ChromeDriver driver)
        {
            Thread.Sleep(1500);
            ConectionDb conn = new ConectionDb();
            List<IndicadoresFundoImobiliario> ListaDeFundos = new List<IndicadoresFundoImobiliario>();
            List<string> fundosImobiliarios = new List<string>();

            var TabelaFundos = conn.Select(Consultas.GetFundos()).Tables[0];
            foreach (DataRow item in TabelaFundos.Rows)
            {
                fundosImobiliarios.Add(item[0].ToString());
            }
            GerenciamentoTabelasFIIs.CreateTable();

            string emojiVerde = char.ConvertFromUtf32(0x1F7E2);
            string mensagem = $"   {emojiVerde} FUNDOS IMOBILIÁRIOS \n\n";


            foreach (var fundo in fundosImobiliarios) 
            {
                driver.Navigate().GoToUrl($"https://statusinvest.com.br/fundos-imobiliarios/{fundo}");
                IndicadoresFundoImobiliario indicadoresFundo = MontarObjetoIndicadoresFundo.Montar(driver, fundo);

                mensagem += $"\u2705  Fundo: {fundo.ToUpper()}; \n" +
                            $" Valor Atual: R${indicadoresFundo.ValorAtual}; \n" +
                            $" Min(52) Semanas: R${indicadoresFundo.Min52Semanas}; \n" +
                            $" Max(52) Semanas: R${indicadoresFundo.Max52Semanas}; \n" +
                            $" DY: {indicadoresFundo.DividendYeld}%; \n" +
                            $" Valorização(12m): {indicadoresFundo.Valorizacao12Meses}%; \n" +
                            $" PVP: {indicadoresFundo.PVP}; \n" +
                            $" Ultimo Rendimento: R$ {indicadoresFundo.UltimoRendimento}; \n" +
                            $" Data Base: {indicadoresFundo.DataBase.ToString("dd/MM/yyyy")}; \n" +
                            $" Data Pagamento: {indicadoresFundo.DataPagamento.ToString("dd/MM/yyyy")};" +
                            $"\n\n";


                ListaDeFundos.Add(indicadoresFundo);
                GerenciamentoTabelasFIIs.PopulateTable(indicadoresFundo);
            }
            conn.Bulky(GerenciamentoTabelasFIIs.TabelaFII, "[ACTIVE_FINANCE].[DBO].[EXTRACOESFUNDOIMOBILIARIO]", 2);
            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
        }

        public static void BuscarAcoes(ChromeDriver driver) 
        {
            Thread.Sleep(1500);
            ConectionDb conn = new ConectionDb();
            List<IndicadoresAcoes> listaDeAcoes = new List<IndicadoresAcoes>();
            List<string> acoes = new List<string>();

            var TabelaAcoes = conn.Select(Consultas.GetAcoes()).Tables[0];
            foreach (DataRow item in TabelaAcoes.Rows)
            {
                acoes.Add(item[0].ToString());
            }
            GerenciamentoDeTabelasAcoes.CreateTable();

            string emojiVermelho = char.ConvertFromUtf32(0x1F534);
            string mensagem = $"  {emojiVermelho} AÇÕES \n\n";

            foreach (string acao in acoes) 
            {
                driver.Navigate().GoToUrl(@$"https://statusinvest.com.br/acoes/{acao}");
                IndicadoresAcoes indicadoresAcoes = MontarObjetoIndicadoresAcoes.Montar(driver, acao);

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
                GerenciamentoDeTabelasAcoes.PopulateTable(indicadoresAcoes);
            }
            conn.Bulky(GerenciamentoDeTabelasAcoes.TabelaAcoes, "[ACTIVE_FINANCE].[DBO].[EXTRACOESACOES]", 2);
            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
        }
    }
}