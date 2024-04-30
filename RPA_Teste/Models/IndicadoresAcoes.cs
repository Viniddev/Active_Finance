using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Models
{
    public class IndicadoresAcoes
    {
        public string Nome { get; set; }
        public double ValorAtual { get; set; }
        public double Min52Semanas { get; set; }
        public double Max52Semanas { get; set; }
        public double DividendYeld { get; set; }
        public double Valorizacao12Meses { get; set; }
        public double PrecoLucro { get; set; }
        public double PrecoSobreValorPatrimonial { get; set; }
        public double ValorPatrimonialPorAcao { get; set; }
        public double LucroPorAcao { get; set; }
        public double DividaLiquidaPorPatrimonioLiquido { get; set; }
        public double MargemBruta { get; set; }
        public double RetornoSobrePatrimonioLiquido { get; set; }

    }
}
