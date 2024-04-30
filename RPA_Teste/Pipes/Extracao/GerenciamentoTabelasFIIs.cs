using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using RPA_Teste.Models;

namespace RPA_Teste.Pipes.Extracao
{
    internal class GerenciamentoTabelasFIIs
    {
        public static DataTable TabelaFII { get; set; }

        public static void CreateTable()
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nome");
            dataTable.Columns.Add("ValorAtual");
            dataTable.Columns.Add("Min52Semanas");
            dataTable.Columns.Add("Max52Semanas");
            dataTable.Columns.Add("DividendYeld");
            dataTable.Columns.Add("Valorizacao12Meses");
            dataTable.Columns.Add("valpatrimonialporcota");
            dataTable.Columns.Add("pvp");
            dataTable.Columns.Add("valoremcaixa");
            dataTable.Columns.Add("ultimorendimento");
            dataTable.Columns.Add("rendimento");
            dataTable.Columns.Add("cotacaobase");
            dataTable.Columns.Add("database");
            dataTable.Columns.Add("datapagamento");

            TabelaFII = dataTable;
        }

        public static void PopulateTable(IndicadoresFundoImobiliario indicadoresFundo) 
        {

            DataRow row = TabelaFII.NewRow();

            row[0] = indicadoresFundo.Nome.ToUpper();
            row[1] = indicadoresFundo.ValorAtual;
            row[2] = indicadoresFundo.Min52Semanas;
            row[3] = indicadoresFundo.Max52Semanas;
            row[4] = indicadoresFundo.DividendYeld;
            row[5] = indicadoresFundo.Valorizacao12Meses;
            row[6] = indicadoresFundo.ValPatrimonialPorCota;
            row[7] = indicadoresFundo.PVP;
            row[8] = indicadoresFundo.ValorEmCaixa;
            row[9] = indicadoresFundo.UltimoRendimento;
            row[10] = indicadoresFundo.Rendimento;
            row[11] = indicadoresFundo.CotacaoBase;
            row[12] = indicadoresFundo.DataBase;
            row[13] = indicadoresFundo.DataPagamento;

            TabelaFII.Rows.Add(row);
        }
    }
}
