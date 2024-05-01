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

namespace RPA_Teste.Pipes.Navegador.FundosImobiliarios
{
    internal class BuscarFundos
    {
        public static void BuscarFundosImobiliarios(ChromeDriver driver)
        {
            Thread.Sleep(1500);
            ConectionDb conn = new ConectionDb();

            List<string> fundosImobiliarios = new List<string>();
            var TabelaFundos = conn.Select(Consultas.GetFundos()).Tables[0];
            foreach (DataRow item in TabelaFundos.Rows)
            {
                fundosImobiliarios.Add(item[0].ToString());
            }

            GerenciamentoTabelasFIIs.CreateTable();

            string emojiVerde = char.ConvertFromUtf32(0x1F7E2);
            string mensagem = $"   {emojiVerde} FUNDOS IMOBILIÁRIOS \n\n";

            foreach (string fundo in fundosImobiliarios)
            {
                driver.Navigate().GoToUrl($"https://statusinvest.com.br/fundos-imobiliarios/{fundo}");
                IndicadoresFundoImobiliario indicadoresFundo = MontarObjetoIndicadoresFundo.Montar(driver, fundo);

                mensagem += $"\u2705  Fundo: {fundo.ToUpper()}; \n" +
                            $" • Valor Atual: R${indicadoresFundo.ValorAtual}; \n" +
                            $" • Min(52) Semanas: R${indicadoresFundo.Min52Semanas}; \n" +
                            $" • Max(52) Semanas: R${indicadoresFundo.Max52Semanas}; \n" +
                            $" • DY: {indicadoresFundo.DividendYeld}%; \n" +
                            $" • Valorização(12m): {indicadoresFundo.Valorizacao12Meses}%; \n" +
                            $" • PVP: {indicadoresFundo.PVP}; \n" +
                            $" • Ultimo Rendimento: R$ {indicadoresFundo.UltimoRendimento}; \n" +
                            $" • Data Base: {indicadoresFundo.DataBase.ToString("dd/MM/yyyy")}; \n" +
                            $" • Data Pagamento: {indicadoresFundo.DataPagamento.ToString("dd/MM/yyyy")};" +
                            $"\n\n";

                GerenciamentoTabelasFIIs.PopulateTable(indicadoresFundo);
            }

            conn.Bulky(GerenciamentoTabelasFIIs.TabelaFII, "[ACTIVE_FINANCE].[DBO].[EXTRACOESFUNDOIMOBILIARIO]", 2);
            Telegram.TelegramApi.SendMessageAsync(mensagem).Wait();
            Telegram.TelegramApi.SendLogText(mensagem, "Fundos").Wait();
        }
    }
}
