using ClosedXML.Excel;
using RPA_Teste.Pipes.Extracao;
using RPA_Teste.Pipes.Telegram;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Excel
{
    public class BuildLogExcel
    {
        private static IXLWorkbook wb;
        private static IXLWorksheet ws;
        public static void Montar() 
        {
            DataTable TabelaFiis = GerenciamentoTabelasFIIs.TabelaFII;
            string filepath = @$"{System.AppDomain.CurrentDomain.BaseDirectory}Output\OutputModel.xlsx";
            string filesave = @$"{System.AppDomain.CurrentDomain.BaseDirectory}Output\OutpuDef.xlsx";

            wb = new XLWorkbook(filepath);
            ws = wb.Worksheet("Fundos");

            foreach (DataRow dr in TabelaFiis.Rows) 
            {
                int ultimalinha = ws.LastRowUsed().RowBelow().RowNumber();
                IXLRow objetoUltimaLinha = ws.LastRowUsed().RowBelow();

                formatarCelulas(objetoUltimaLinha);

                ws.Cell(ultimalinha, "A").Value = dr[0].ToString();
                ws.Cell(ultimalinha, "B").Value = FormatacaoDados.ReceberDados(dr[1].ToString());
                ws.Cell(ultimalinha, "C").Value = FormatacaoDados.ReceberDados(dr[2].ToString());
                ws.Cell(ultimalinha, "D").Value = FormatacaoDados.ReceberDados(dr[3].ToString());
                ws.Cell(ultimalinha, "E").Value = FormatacaoDados.ReceberDados(dr[4].ToString());
                ws.Cell(ultimalinha, "F").Value = FormatacaoDados.ReceberDados(dr[5].ToString());
                ws.Cell(ultimalinha, "G").Value = FormatacaoDados.ReceberDados(dr[6].ToString());
                ws.Cell(ultimalinha, "H").Value = FormatacaoDados.ReceberDados(dr[7].ToString());
                ws.Cell(ultimalinha, "I").Value = FormatacaoDados.ReceberDados(dr[8].ToString());
                ws.Cell(ultimalinha, "J").Value = FormatacaoDados.ReceberDados(dr[9].ToString());
                ws.Cell(ultimalinha, "K").Value = FormatacaoDados.ReceberDados(dr[10].ToString());
                ws.Cell(ultimalinha, "L").Value = FormatacaoDados.ReceberDados(dr[11].ToString());
                ws.Cell(ultimalinha, "M").Value = FormatacaoDados.ConvertDataToLogExcel(dr[12].ToString());
                ws.Cell(ultimalinha, "N").Value = FormatacaoDados.ConvertDataToLogExcel(dr[12].ToString());

            }

            DataTable TabelaAcoes = GerenciamentoDeTabelasAcoes.TabelaAcoes;
            ws = wb.Worksheet("Acoes");

            foreach (DataRow dr in TabelaAcoes.Rows)
            {
                int ultimalinha = ws.LastRowUsed().RowBelow().RowNumber();
                IXLRow objetoUltimaLinha = ws.LastRowUsed().RowBelow();

                formatarCelulas(objetoUltimaLinha);

                ws.Cell(ultimalinha, "A").Value = dr[0].ToString();
                ws.Cell(ultimalinha, "B").Value = FormatacaoDados.ReceberDados(dr[1].ToString());
                ws.Cell(ultimalinha, "C").Value = FormatacaoDados.ReceberDados(dr[2].ToString());
                ws.Cell(ultimalinha, "D").Value = FormatacaoDados.ReceberDados(dr[3].ToString());
                ws.Cell(ultimalinha, "E").Value = FormatacaoDados.ReceberDados(dr[4].ToString());
                ws.Cell(ultimalinha, "F").Value = FormatacaoDados.ReceberDados(dr[5].ToString());
                ws.Cell(ultimalinha, "G").Value = FormatacaoDados.ReceberDados(dr[6].ToString());
                ws.Cell(ultimalinha, "H").Value = FormatacaoDados.ReceberDados(dr[7].ToString());
                ws.Cell(ultimalinha, "I").Value = FormatacaoDados.ReceberDados(dr[8].ToString());
                ws.Cell(ultimalinha, "J").Value = FormatacaoDados.ReceberDados(dr[9].ToString());
                ws.Cell(ultimalinha, "K").Value = FormatacaoDados.ReceberDados(dr[10].ToString());
                ws.Cell(ultimalinha, "L").Value = FormatacaoDados.ReceberDados(dr[11].ToString());
                ws.Cell(ultimalinha, "M").Value = FormatacaoDados.ReceberDados(dr[12].ToString());

            }

            wb.SaveAs(filesave);
            TelegramApi.SendLogArchive(filesave, "Relatorios.xlsx").Wait();
        }
        public static void formatarCelulas(IXLRow lastLine)
        {
            int contador = 2;
            while (contador <= 15)
            {
                lastLine.Cell(contador).Style.NumberFormat.Format = "_(R$* #,##0.00_);_(R$* - #,##0.00;_(R$* \"-\"??_);_(@_)";
                contador++;
            }
        }
    }
}
