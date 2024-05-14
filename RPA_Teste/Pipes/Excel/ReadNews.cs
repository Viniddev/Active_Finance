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
        private static DataTable Ativos, dataResultBd;

        public static void ReadExcel()
        {
            ConectionDb conn = new ConectionDb();
           
            string filepath = $@"{System.AppDomain.CurrentDomain.BaseDirectory}NewsPath\Input.xlsx";
            Ativos = new DataTable();
            Ativos.Columns.Add("Name");
            Ativos.Columns.Add("Price");
            Ativos.Columns.Add("Status");

            ExtracAcoesBdAndExcel(conn, filepath);
            CompareAcoesBdAndExcel(dataResultBd, Ativos);

            dataResultBd.Clear();
            Ativos.Clear();

            ExtractFundosBdAndExcel(conn, filepath);
            CompareFundosBdAndExcel(dataResultBd, Ativos);

        }

        private static void ExtracAcoesBdAndExcel(ConectionDb conn, string filepath)
        {
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

            dataResultBd = conn.Select(Consultas.GetAcoes(false)).Tables[0];
            Wb.Save();
        }

        private static void ExtractFundosBdAndExcel(ConectionDb conn, string filepath)
        {
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
            Wb.Save();
        }

        public static void CompareAcoesBdAndExcel(DataTable dataResultBd, DataTable Ativos) 
        {
            ConectionDb conn = new ConectionDb();

            bool existenobd = false;
            foreach (DataRow rowAtivos in Ativos.Rows)
            {
                foreach (DataRow rowDataResultBd in dataResultBd.Rows)
                {
                    Console.WriteLine($"{rowAtivos[0].ToString().ToUpper()} | {rowDataResultBd[0].ToString()}");



                    if (rowAtivos[0].ToString().ToUpper().Equals(rowDataResultBd[0].ToString().ToString()))
                    {
                        existenobd = true;
                        break;
                    }
                    else 
                    {
                        existenobd = false;
                    }
                }

                InsertAcoes(conn, existenobd, rowAtivos);
            }
        }

        private static void InsertAcoes(ConectionDb conn, bool existenobd, DataRow rowAtivos)
        {
            if (existenobd)
            {
                Console.WriteLine($"Existia no bd: {rowAtivos[0].ToString().ToUpper()}");
                conn.ExecuteQuery(Consultas.UpdateAcoes(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
            }
            else
            {
                Console.WriteLine($"Não existia no bd: {rowAtivos[0].ToString().ToUpper()}");
                conn.ExecuteQuery(Consultas.InsertAcoes(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
            }
        }

        public static void CompareFundosBdAndExcel(DataTable dataResultBd, DataTable Ativos)
        {
            ConectionDb conn = new ConectionDb();

            bool existenobd = false;
            foreach (DataRow rowAtivos in Ativos.Rows)
            {
                foreach (DataRow rowDataResultBd in dataResultBd.Rows)
                {
                    Console.WriteLine($"{rowAtivos[0].ToString().ToUpper()} | {rowDataResultBd[0].ToString()}");

                    if (rowAtivos[0].ToString().ToUpper().Equals(rowDataResultBd[0].ToString().ToString()))
                    {
                        existenobd = true;
                        break;
                    }
                    else 
                    {
                        existenobd = false;
                    }
                }

                InsertFundos(conn, existenobd, rowAtivos);
            }
        }

        private static void InsertFundos(ConectionDb conn, bool existenobd, DataRow rowAtivos)
        {
            if (existenobd)
            {
                Console.WriteLine($"Existia no bd: {rowAtivos[0].ToString().ToUpper()}");
                conn.ExecuteQuery(Consultas.UpdateFundos(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
            }
            else
            {
                Console.WriteLine($"Não existia no bd: {rowAtivos[0].ToString().ToUpper()}");
                conn.ExecuteQuery(Consultas.InsertFundos(rowAtivos[0].ToString().ToUpper(), rowAtivos[1].ToString(), Convert.ToInt16(rowAtivos[2])));
            }
        }
    }
}
