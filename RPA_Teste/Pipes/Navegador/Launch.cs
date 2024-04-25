using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace RPA_Teste.Pipes.Navegador
{
    public class Launch
    {
        public static dynamic LaunchNavegador() 
        {
            IWebDriver Driver;

            string pathDownload = $@"C:\RPA\Finanças";

            ChromeOptions opt = new ChromeOptions();

            if (!Directory.Exists(@"C:\ScopeDir\ScopeDir"))
                Directory.CreateDirectory(@"C:\Selenium\Scope");

            opt.AcceptInsecureCertificates = true;
            string scopeDirPath = @"C:\Selenium\Scope\ScopeDir";
            opt.AddArgument($"--user-data-dir={scopeDirPath}");

            opt.AddArgument("--start-maximized");
            opt.AddArgument("--aways-authorize-plugins");
            opt.AddArgument("--disable-notifications");
            opt.AddArgument("--no-sandbox");
            opt.AddArgument("--ignore-certificate-errors");
            opt.AddArgument("--ignore-ssl-errors");
            opt.AddUserProfilePreference("download.default_directory", pathDownload);

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("");
            chromeDriverService.HideCommandPromptWindow = true;
            Driver = new ChromeDriver(chromeDriverService, opt, TimeSpan.FromSeconds(300));

            return Driver;
        }
    }
}
