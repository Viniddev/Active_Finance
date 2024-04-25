using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Teste;
using RPA_Teste.Models;
using RPA_Teste.Pipes.Excel;
using RPA_Teste.Pipes.Navegador;

/*
    Espaço para estudar sobre automação
    criado em 23/12/2023 por Vinícius Dias

*/

namespace RPA_Teste
{
    public class Program 
    {
        static void Main(string[] args) 
        {
            ChromeDriver driver = Launch.LaunchNavegador();
            IReadOnlyCollection<IWebElement> values = GetInformations.Challenge(driver);
            Excel.InserirIndicadores(values, driver);

            Console.ReadLine();

            Aplication.KillChromeDriver();
        }
    }
}