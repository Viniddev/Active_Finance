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
        public string ValorAtual { get; set; }
        public string Min52Semanas { get; set; }
        public string Max52Semanas { get; set; }
        public string DividendYeld { get; set; }
        public string Valorizacao12Meses { get; set; }
        public string ValPatrimonialPorCota { get; set; }
        public string PVP { get; set; }
        public string ValorEmCaixa { get; set; }
        public string UltimoRendimento { get; set; }
        public string Rendimento { get; set; }
        public string CotacaoBase { get; set; }
        public string DataBase { get; set; }
        public string DataPagamento { get; set; }
    }
}
