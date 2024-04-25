using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA_Teste.Models;


namespace RPA_Teste.Pipes.Navegador
{
    public class GetInformations
    {
        public static IReadOnlyCollection<IWebElement> Challenge(ChromeDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.fundsexplorer.com.br/funds/htmx11");
            Thread.Sleep(1000);

            IReadOnlyCollection<IWebElement> elementosIndicadores = driver.FindElements(By.XPath("//*[@id=\"indicators\"]/div/p[2]/b"));
            
            foreach (IWebElement element in elementosIndicadores) 
            {

                Console.WriteLine("elemento: " + element.Text);

            }

            return elementosIndicadores;
        }
    }
}