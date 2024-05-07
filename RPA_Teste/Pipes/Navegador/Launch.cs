using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using DocumentFormat.OpenXml.Bibliography;

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
            opt.AddArgument("--allow-running-insecure-content");
            opt.AddArgument("--unsafely-treat-insecure-origin-as-secure=https://statusinvest.com.br/fundos-imobiliarios/");
            opt.AddArgument("--unsafely-treat-insecure-origin-as-secure=https://statusinvest.com.br/acoes/");
            opt.AddArgument("--start-maximized");
            opt.AddArgument("--aways-authorize-plugins");
            opt.AddArgument("--disable-notifications");
            opt.AddArgument("--no-sandbox");
            opt.AddArgument("--ignore-certificate-errors");
            opt.AddArgument("--ignore-ssl-errors");
            opt.AddArgument("--headless=new");
            opt.AddUserProfilePreference("download.default_directory", pathDownload);
            opt.AddArgument("--enable-features=NetworkService,NetworkServiceInProcess");

            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService("");
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.EnableVerboseLogging = true;
            Driver = new ChromeDriver(chromeDriverService, opt, TimeSpan.FromSeconds(500));

            Driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));



            Cookie cookie = new Cookie("cf_clearance", 
                "psIhLs.6yK0WhfQV2s350VQuVaEKVFroSUue6yPeyCM-1714659566-1.0.1.1-yuv3j5SZ3F_zmBR2Mac.pxN3el6Uz3vsAqU.t7ySPxBWbNYsnDIfOVbsh6yVy0EZSV8BIM.1IGqEAw0WKZqxVw",
                "https://statusinvest.com.br/fundos-imobiliarios/", 
                "/", 
                DateTime.Now.AddDays(1)
            );

            Driver.Manage().Cookies.AddCookie(cookie);



            return Driver;
        }
    }
}