using RPA_Teste.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Extracao
{
    internal class GerenciamentoDeTabelasAcoes
    {
        public static DataTable TabelaAcoes { get; set; }

        public static void CreateTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nome");
            dataTable.Columns.Add("ValorAtual");
            dataTable.Columns.Add("Min52Semanas");
            dataTable.Columns.Add("Max52Semanas");
            dataTable.Columns.Add("DividendYeld");
            dataTable.Columns.Add("Valorizacao12Meses");
            dataTable.Columns.Add("PrecoLucro");
            dataTable.Columns.Add("PrecoSobreValorPatrimonial");
            dataTable.Columns.Add("ValorPatrimonialPorAcao");
            dataTable.Columns.Add("LucroPorAcao");
            dataTable.Columns.Add("DividaLiquidaPorPatrimonioLiquido");
            dataTable.Columns.Add("MargemBruta");
            dataTable.Columns.Add("RetornoSobrePatrimonioLiquido");

            TabelaAcoes = dataTable;
        }

        public static void PopulateTable(IndicadoresAcoes indicadoresAcoes)
        {

            DataRow row = TabelaAcoes.NewRow();

            row[0] = indicadoresAcoes.Nome.ToUpper();
            row[1] = indicadoresAcoes.ValorAtual;
            row[2] = indicadoresAcoes.Min52Semanas;
            row[3] = indicadoresAcoes.Max52Semanas;
            row[4] = indicadoresAcoes.DividendYeld;
            row[5] = indicadoresAcoes.Valorizacao12Meses;
            row[6] = indicadoresAcoes.PrecoLucro;
            row[7] = indicadoresAcoes.PrecoSobreValorPatrimonial;
            row[8] = indicadoresAcoes.ValorPatrimonialPorAcao;
            row[9] = indicadoresAcoes.LucroPorAcao;
            row[10] = indicadoresAcoes.DividaLiquidaPorPatrimonioLiquido;
            row[11] = indicadoresAcoes.MargemBruta;
            row[12] = indicadoresAcoes.RetornoSobrePatrimonioLiquido;

            TabelaAcoes.Rows.Add(row);
        }
    }
}

