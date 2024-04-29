using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Extracao
{
    internal class MontarObjetoIndicadoresAcoes
    {
        public static IndicadoresAcoes Montar(ChromeDriver driver, string name)
        {

            var elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class = 'value']"));
            IndicadoresAcoes indicadoresAcoes = new IndicadoresAcoes();
            indicadoresAcoes.Nome = name.ToUpper();
            indicadoresAcoes.ValorAtual = FormatacaoDados.ReceberDados(elementosIndicadores[0].Text);
            indicadoresAcoes.Min52Semanas = FormatacaoDados.ReceberDados(elementosIndicadores[1].Text);
            indicadoresAcoes.Max52Semanas = FormatacaoDados.ReceberDados(elementosIndicadores[2].Text);
            indicadoresAcoes.DividendYeld = FormatacaoDados.ReceberDados(elementosIndicadores[3].Text);
            indicadoresAcoes.Valorizacao12Meses = FormatacaoDados.ReceberDados(elementosIndicadores[4].Text);
            elementosIndicadores = driver.FindElements(By.XPath(".//strong[@class='value d-block lh-4 fs-4 fw-700']"));
            indicadoresAcoes.PrecoLucro = FormatacaoDados.ReceberDados(elementosIndicadores[1].Text);
            indicadoresAcoes.PrecoSobreValorPatrimonial = FormatacaoDados.ReceberDados(elementosIndicadores[3].Text);
            indicadoresAcoes.ValorPatrimonialPorAcao = FormatacaoDados.ReceberDados(elementosIndicadores[8].Text);
            indicadoresAcoes.LucroPorAcao = FormatacaoDados.ReceberDados(elementosIndicadores[9].Text);
            indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido = FormatacaoDados.ReceberDados(elementosIndicadores[14].Text);
            indicadoresAcoes.MargemBruta = FormatacaoDados.ReceberDados(elementosIndicadores[20].Text);
            indicadoresAcoes.RetornoSobrePatrimonioLiquido = FormatacaoDados.ReceberDados(elementosIndicadores[24].Text);

            return indicadoresAcoes;
        }
    }
}
