using DocumentFormat.OpenXml.Bibliography;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Teste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPA_Teste.Pipes.Navegador;
using System.Globalization;

namespace RPA_Teste.Pipes.Extracao
{
    internal class MontarObjetoIndicadoresFundo
    {
        public static IndicadoresFundoImobiliario Montar(ChromeDriver driver, string name) 
        {
            IndicadoresFundoImobiliario indicadoresFundo = new IndicadoresFundoImobiliario();
            var elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class = 'value']"));
            indicadoresFundo.Nome = name.ToUpper();
            indicadoresFundo.ValorAtual = FormatacaoDados.ReceberDados(elementosIndicadores[0].Text);
            indicadoresFundo.Min52Semanas = FormatacaoDados.ReceberDados(elementosIndicadores[1].Text);
            indicadoresFundo.Max52Semanas = FormatacaoDados.ReceberDados(elementosIndicadores[2].Text);
            indicadoresFundo.DividendYeld = FormatacaoDados.ReceberDados(elementosIndicadores[3].Text);
            indicadoresFundo.Valorizacao12Meses = FormatacaoDados.ReceberDados(elementosIndicadores[4].Text);
            indicadoresFundo.ValPatrimonialPorCota = FormatacaoDados.ReceberDados(elementosIndicadores[5].Text);
            indicadoresFundo.PVP = FormatacaoDados.ReceberDados(elementosIndicadores[6].Text);
            indicadoresFundo.ValorEmCaixa = FormatacaoDados.ReceberDados(elementosIndicadores[7].Text);
            var ultimoRendimento = driver.FindElement(By.XPath(".//strong[@class = 'value d-inline-block fs-5 fw-900'][1]"));
            indicadoresFundo.UltimoRendimento = FormatacaoDados.ReceberDados(ultimoRendimento.Text);
            elementosIndicadores = driver.FindElements(By.XPath(".//b[@class = 'sub-value fs-4 lh-3']"));
            indicadoresFundo.Rendimento = FormatacaoDados.ReceberDados(elementosIndicadores[0].Text);
            indicadoresFundo.CotacaoBase = FormatacaoDados.ReceberDados(elementosIndicadores[1].Text);
            indicadoresFundo.DataBase = FormatacaoDados.ConvertDate(elementosIndicadores[2].Text);
            indicadoresFundo.DataPagamento = FormatacaoDados.ConvertDate(elementosIndicadores[3].Text);

            return indicadoresFundo;
        }
    }
}
