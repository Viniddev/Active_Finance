using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Teste.Models;

namespace RPA_Teste.Pipes.Excel
{

    /*
        refazer os metodos para substituir o log em txt para log xlsx
     */


    public class Excel
    {
        public static void InserirIndicadores(IReadOnlyCollection<IWebElement> valoresAcao, ChromeDriver driver, string tipo = "Ativo") 
        {
            IWebElement nomeAcao = driver.FindElement(By.XPath("//*[@id='tickerName']"));
            string caminho = AppDomain.CurrentDomain.BaseDirectory + $@"{tipo}.xlsx";

            try
            {
                using (var workbook = File.Exists(caminho) ? new XLWorkbook(caminho) : new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault() ?? workbook.Worksheets.Add("Dados");

                    if (IsFirstRowEmpty(worksheet))
                    {
                        AddHeaders(worksheet, Constants.Cabecalho.Propriedades);
                    }

                    FillData(worksheet, valoresAcao, Constants.Cabecalho.Propriedades);


                    if (File.Exists(caminho))
                    {
                        workbook.Save();
                    }
                    else
                    {
                        workbook.SaveAs(caminho);
                    }

                    Console.WriteLine($"As informações foram gravadas no arquivo {caminho}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possivel salvar o arquivo excel {ex.ToString()}");
                Console.WriteLine($"\n\n {ex.StackTrace}");
            }
        }

        public static bool IsFirstRowEmpty(IXLWorksheet worksheet)
        {
            return worksheet.Cell(1, 1).Value.ToString() == "";
        }

        public static void AddHeaders(IXLWorksheet worksheet, List<string> headers)
        {
            int headerIndex = 1;
            foreach (var header in headers)
            {
                worksheet.Cell(1, headerIndex).Value = header;
                worksheet.Cell(1, headerIndex).Value = header;
                headerIndex++;
            }
        }

        public static void FillData(IXLWorksheet worksheet, IReadOnlyCollection<IWebElement> obj, List<string> properties)
        {
            var objectProperties = typeof(IReadOnlyCollection<IWebElement>).GetProperties();
            int proximaLinhaVazia = worksheet.LastRowUsed().RowNumber();

            var newRow = worksheet.Row(proximaLinhaVazia + 1);
            int cellIndex = 1;
            foreach (var propertyName in properties)
            {
                var property = objectProperties.FirstOrDefault(p => p.Name == propertyName);
                if (property != null)
                {
                    var value = property.GetValue(obj);
                    newRow.Cell(cellIndex).Value = value != null ? value.ToString() : string.Empty;
                    cellIndex++;
                }
            }
        }
    }
 }
