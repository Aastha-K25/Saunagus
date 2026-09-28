// Importerer Selenium biblioteket
// OpenQA.Selenium indeholder interfaces og funktioner til browser automation
using OpenQA.Selenium;

// Importerer ChromeDriver. ChromeDriver bruges til at styre. Google Chrome browseren automatisk
using OpenQA.Selenium.Chrome;
// Namespace bruges til at organisere testklasser
namespace SaunagusTest;

// Testklasse. Klassen indeholder automatiske tests
public class SaunagusTest
{
    // [Fact] bruges i xUnit - Fortæller at metoden er en testmetode
    [Fact]

    // Tester om frontend kan hente data fra API'et og vise tabellen korrekt
    public void GetAllSaunagusTest()
    {
        // IWebDriver bruges til at styre browseren. ChromeDriver åbner en Chrome browser, som Selenium kan kontrollere automatisk
        IWebDriver driver = new ChromeDriver();

        // Navigate bruges til at åbne en URL. GoToUrl åbner frontend applikationen
        driver.Navigate().GoToUrl(
            "http://127.0.0.1:5500/SaunagusWeb/Saunagus.html"
        );

        // FindElement bruges til at finde HTML elementer By.TagName("button") finder første button element på siden
        IWebElement button =
            driver.FindElement(By.TagName("button"));
        // Click simulerer et klik fra brugeren. Klikker på knappen som henter data
        button.Click();

        // Thread.Sleep pauser programmet i 2 sekunder. Bruges for at give API request tid til at hente data og opdatere tabellen
        Thread.Sleep(2000);

        // Finder tabel elementet på siden. Hvis tabellen findes betyder det at data er blevet vist
        IWebElement table =
            driver.FindElement(By.TagName("table"));
        // Assert bruges til at teste forventet resultat. NotNull checker at objektet findes og ikke er null
        Assert.NotNull(table);

        // Quit lukker browseren. Det er vigtigt at lukke browseren efter testen er færdig
        driver.Quit();
    }
}

// c)
// Hvad er ellers godt at UI teste i denne web applikation?
// Man kan også teste:
// - Navigation mellem sider
// - Formular input
// - Validering af data
// - Fejlbeskeder
// - Knapper
// - Om data vises korrekt
// - Responsivt design
// - Om API data bliver opdateret korrekt

// UI tests hjælper med at sikre:
// - god brugeroplevelse
// - korrekt funktionalitet
// - færre fejl i systemet

// Selenium bruges fordi det kan simulere
// en rigtig bruger i browseren