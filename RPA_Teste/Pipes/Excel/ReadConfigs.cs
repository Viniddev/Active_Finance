using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.Pipes.Excel
{
    public class ReadConfigs
    {
        public static bool EnviarEtapaLogTxt { get; set; } = false;
        public static bool EnviarEtapaLogExcel { get; set; } = false;
        public static bool EnviarEtapaAlertaPrecos { get; set; } = false;

        private static IXLWorkbook wb;
        private static IXLWorksheet ws;

        public static void Read() 
        {
            string filepath = $@"{System.AppDomain.CurrentDomain.BaseDirectory}NewsPath\Input.xlsx";

            wb = new XLWorkbook(filepath);
            ws = wb.Worksheet("Configs");

            foreach (IXLRow linha in ws.RowsUsed()) 
            {
                if (linha.RowNumber() == 1) 
                {
                    continue;
                }

                switch (linha.Cell(1).Value.ToString()) 
                {
                    case var textLine when textLine.Contains("ENVIAR LOG TXT"):
                        if (linha.Cell(2).Value.ToString() == "1")
                            EnviarEtapaLogTxt = true;
                        break;
                    case var textLine when textLine.Contains("ENVIAR LOG EXCEL"):
                        if (linha.Cell(2).Value.ToString() == "1")
                            EnviarEtapaLogExcel = true;
                        break;
                    case var textLine when textLine.Contains("ENVIAR ALERTA PRECOS"):
                        if (linha.Cell(2).Value.ToString() == "1")
                            EnviarEtapaAlertaPrecos = true;
                        break;
                    default:
                        break;

                }
            }

        }
    }
}
