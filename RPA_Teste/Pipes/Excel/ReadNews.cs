using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using RPA_Teste.DataBase;
using System.Data;
using System.Linq;
using RPA_Teste.Pipes.Extracao;

namespace RPA_Teste.Pipes.Excel
{
    public class ReadNews
    {
        private static IXLWorkbook Wb;
        private static IXLWorksheet Ws;

        public static void ReadExcel()
        {
            ConectionDb conn = new ConectionDb();
            DataTable Ativos = new DataTable();
            Ativos.Columns.Add("Name");
            Ativos.Columns.Add("Price");
            Ativos.Columns.Add("Status");

            string filepath = $@"{System.AppDomain.CurrentDomain.BaseDirectory}NewsPath\Input.xlsx";
            Wb = new XLWorkbook(filepath);
            Ws = Wb.Worksheet("Acoes");

            foreach (var row in Ws.RowsUsed())
            {
                if (row.RowNumber() == 1)
                {
                    continue;
                }
                else
                {
                    DataRow linha = Ativos.NewRow();
                    linha[0] = row.Cell(1).Value.ToString();
                    linha[1] = row.Cell(2).Value.ToString();
                    linha[2] = Convert.ToInt16(row.Cell(3).Value.ToString());

                    Ativos.Rows.Add(linha);
                }
            }


            DataTable dataResultBd = conn.Select(Consultas.GetAcoes(false)).Tables[0];
            CompareAndInsertAcoes(dataResultBd, Ativos);


            Wb.Save();
            dataResultBd.Clear();
            Ativos.Clear();

            filepath = $@"{System.AppDomain.CurrentDomain.BaseDirectory}NewsPath\Input.xlsx";
            Wb = new XLWorkbook(filepath);
            Ws = Wb.Worksheet("Fundos");    

            foreach (var row in Ws.RowsUsed())
            {
                if (row.RowNumber() == 1)
                {
                    continue;
                }
                else
                {
                    DataRow linha = Ativos.NewRow();
                    linha[0] = row.Cell(1).Value.ToString();
                    linha[1] = row.Cell(2).Value.ToString();
                    linha[2] = Convert.ToInt16(row.Cell(3).Value.ToString());

                    Ativos.Rows.Add(linha);
                }
            }

            dataResultBd = conn.Select(Consultas.GetFundos(false)).Tables[0];
            CompareAndInsertFundos(dataResultBd, Ativos);
        }

        public static void CompareAndInsertAcoes(DataTable dataResultBd, DataTable Ativos) 
        {
            ConectionDb conn = new ConectionDb();
            DataTable resultComparison = new DataTable();

            bool existenobd = false;
            foreach (DataRow rowAtivos in Ativos.Rows)
            {
                foreach (DataRow rowDataResultBd in dataResultBd.Rows)
                {
                    if (rowAtivos[0].ToString().ToUpper().Equals(rowDataResultBd[0].ToString()))
                    {
                        existenobd = true;
                    }
                }

                if (existenobd)
                {
                    Console.WriteLine("Existe " + rowAtivos[0].ToString());
                    conn.ExecuteQuery(Consultas.UpdateAcoes(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
                }
                else
                {
                    Console.WriteLine("Nao existe " + rowAtivos[0].ToString());
                    conn.ExecuteQuery(Consultas.InsertAcoes(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
                }
            }
            Console.WriteLine("\n--- --- --- --- --- --- --- ---\n");
        }
        public static void CompareAndInsertFundos(DataTable dataResultBd, DataTable Ativos)
        {
            ConectionDb conn = new ConectionDb();
            DataTable resultComparison = new DataTable();

            bool existenobd = false;
            foreach (DataRow rowAtivos in Ativos.Rows)
            {
                foreach (DataRow rowDataResultBd in dataResultBd.Rows)
                {
                    if (rowAtivos[0].ToString().ToUpper().Equals(rowDataResultBd[0].ToString()))
                    {
                        existenobd = true;
                    }
                }

                if (existenobd)
                {
                    Console.WriteLine("Existe " + rowAtivos[0].ToString());
                    conn.ExecuteQuery(Consultas.UpdateFundos(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
                }
                else
                {
                    Console.WriteLine("Nao existe " + rowAtivos[0].ToString());
                    conn.ExecuteQuery(Consultas.InsertFundos(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
                }
            }
            Console.WriteLine("\n--- --- --- --- --- --- --- ---\n");
        }
    }
}
