using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.DataBase
{
    internal class Consultas
    {
        public static string InsertAcoes(double ValorAtual, double min52sem, double max52sem, double dy, double valorizacao, double precolucro, double pvp, double vpa, double lpa, double dividaLiquida, double margembruta, double retorno)
        {
            return $@"
                insert into Active_Finance.dbo.extracoesacoes values (
	                {ValorAtual},
                    {min52sem},
                    {max52sem},
                    {dy},
                    {valorizacao},
                    {precolucro},
                    {pvp},
                    {vpa},
                    {lpa},
                    {dividaLiquida},
                    {margembruta},
                    {retorno}
                )
            ;";
        }

    }
}