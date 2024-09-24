using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PruebaFormularioProject
{
    [TestFixture]
    public class PruebaFormulario
    {
        private IWebDriver driver; // Driver para controlar el navegador
        private WebDriverWait wait; // Espera explícita
        [SetUp]
        public void SetUp()
        {
            // Método que se ejecuta antes de cada test
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize(); // Maximizar la ventana para tener una mejor visualización

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Espera implícita de 10 segundos
            //Navegar a la página de la aplicación
            driver.Navigate().GoToUrl("https://seleniumtestweb.netlify.app/");

            // Creamos una instancia para esperas explícitas
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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

            // Encontrar el campo email por su ID y escribimos un email válido
            IWebElement inputEmail = driver.FindElement(By.Id("input-email")); // Campo Email
            inputEmail.Clear(); // Limpiar el campo
            inputEmail.SendKeys("sofia@selenium.com"); // Escribir un email

            // Encontrar el campo de contraseña por su ID y escribimos una contraseña válida
            IWebElement inputPassword = driver.FindElement(By.Id("input-password")); // Campo Contraseña
            inputPassword.Clear(); // Limpiar el campo
            inputPassword.SendKeys("secretoabsoluto"); // Escribir un email

            // Seleccionamos una opción valida del select
            SelectElement selectOpciones = new SelectElement(driver.FindElement(By.Id("select-opciones")));
            selectOpciones.SelectByValue("opcion1");

            // Seleccionamos un radio button
            IWebElement radio1 = driver.FindElement(By.Id("radio1")); // Radio button 1
            radio1.Click(); // Seleccionar el radio button 1

            // Marcar el checkbox requerido
            IWebElement checkbox = driver.FindElement(By.Id("checkbox1")); // Checkbox 1
            checkbox.Click(); // Marcar el checkbox 1
        }
    }
}
