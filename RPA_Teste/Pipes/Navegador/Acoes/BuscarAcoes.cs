using OpenQA.Selenium.Chrome;
using RPA_Teste.DataBase;
using RPA_Teste.Models;
using RPA_Teste.Pipes.Extracao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Navegador.Acoes
{
    internal class BuscarAcoes
    {
        public static void Buscar(ChromeDriver driver)
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
                            $" • Valor Atual: R${indicadoresAcoes.ValorAtual}; \n" +
                            $" • VPA: R${indicadoresAcoes.ValorPatrimonialPorAcao}; \n" +
                            $" • Min(52) Semanas: R${indicadoresAcoes.Min52Semanas}; \n" +
                            $" • Max(52) Semanas: R${indicadoresAcoes.Max52Semanas}; \n" +
                            $" • DY: {indicadoresAcoes.DividendYeld}%; \n" +
                            $" • PL: {indicadoresAcoes.PrecoLucro}; \n" +
                            $" • LPA: {indicadoresAcoes.LucroPorAcao}; \n" +
                            $" • DÍV. LÍQUIDA/PL: {indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido}; \n" +
                            $" • Margem Bruta: {indicadoresAcoes.MargemBruta}; \n" +
                            $" • ROE: {indicadoresAcoes.RetornoSobrePatrimonioLiquido};" +
                            $"\n\n";

                listaDeAcoes.Add(indicadoresAcoes);
                GerenciamentoDeTabelasAcoes.PopulateTable(indicadoresAcoes);
            }
            conn.Bulky(GerenciamentoDeTabelasAcoes.TabelaAcoes, "[ACTIVE_FINANCE].[DBO].[EXTRACOESACOES]", 2);
            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
            Telegram.TelegramApi.SendLogText(mensagem, "Acoes").Wait();
        }
    }
}
