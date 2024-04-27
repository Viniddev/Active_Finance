using DocumentFormat.OpenXml.Office2010.PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Models
{
    public class IndicadoresFundoImobiliario
    {
        public double ValorAtual { get; set; }
        public double Min52Semanas { get; set; }
        public double Max52Semanas { get; set; }
        public double DividendYeld { get; set; }
        public double Valorizacao12Meses { get; set; }
        public double ValPatrimonialPorCota { get; set; }
        public double PVP { get; set; }
        public double ValorEmCaixa { get; set; }
        public double UltimoRendimento { get; set; }
        public double Rendimento { get; set; }
        public double CotacaoBase { get; set; }
        public DateTime DataBase { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
