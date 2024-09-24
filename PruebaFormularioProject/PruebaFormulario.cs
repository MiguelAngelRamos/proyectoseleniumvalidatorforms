using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PruebaFormularioProject
{
    [TestFixture]
    public class PruebaFormulario
    {
        private IWebDriver driver; // Driver para controlar el navegador
        [SetUp]
        public void SetUp()
        {
            // Método que se ejecuta antes de cada test
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize(); // Maximizar la ventana para tener una mejor visualización
            //Navegar a la página de la aplicación
            driver.Navigate().GoToUrl("https://seleniumtestweb.netlify.app/");

        }

        [TearDown]
        public void TearDown()
        {
            // Método que se ejecuta después de cada test
            // driver.Quit(); // cerrar el navegador
            // driver.Dispose(); // liberar recursos
        }

        [Test]
        public void PruebaEnvioExitoso()
        {
            Console.WriteLine("Iniciando prueba de envio exitoso con datos válidos");

            // Encontrar los elementos de la página
            // Encontrar el campo texto por su ID y escribimos un texto válido
            IWebElement inputTexto = driver.FindElement(By.Id("input-texto")); // Campo Texto
            inputTexto.Clear(); // Limpiar el campo
            inputTexto.SendKeys("Sofia"); // Escribir un texto

        }
    }
}
