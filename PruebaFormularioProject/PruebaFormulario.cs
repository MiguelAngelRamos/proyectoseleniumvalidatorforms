﻿using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace PruebaFormularioProject
{
    [TestFixture]
    public class PruebaFormulario
    {
        private IWebDriver driver; // Driver para controlar el navegador
        private WebDriverWait wait; // Espera explícita
        private Actions actions;
        [SetUp]
        public void SetUp()
        {
            // Método que se ejecuta antes de cada test

            driver = new ChromeDriver();
            // driver.Manage().Window.Maximize(); // Maximizar la ventana para tener una mejor visualización
            driver.Manage().Window.Size = new System.Drawing.Size(600, 400); // Tamaño de la ventana
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Espera implícita de 10 segundos
            //Navegar a la página de la aplicación
            driver.Navigate().GoToUrl("https://seleniumtestweb.netlify.app/");
            actions = new Actions(driver);
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

            actions.MoveToElement(radio1).Click().Perform();
            // radio1.Click(); // Seleccionar el radio button 1

            // Marcar el checkbox requerido
            IWebElement checkbox = driver.FindElement(By.Id("checkbox1")); // Checkbox 1
            actions.MoveToElement(checkbox).Click().Perform();
            // checkbox.Click(); // Marcar el checkbox 1

            // Hacemos Click en el botón "Enviar" id=boton-enviar
            IWebElement botonEnviar = driver.FindElement(By.Id("boton-enviar")); // Botón Enviar
            actions.MoveToElement(botonEnviar).Click().Perform(); // Hacer click en el botón

            // Esperamos hasta que se muestre el mensaje de éxito y sea visible

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mensaje-exito")));

            // id mensaje-exito
            // ¡Formulario enviado con éxito!
            // Verificamos que el mensaje de éxito se muestra
            IWebElement mensajeExito = driver.FindElement(By.Id("mensaje-exito")); // Mensaje de éxito
            // Aserción con FluentAssertions
            // Displayed significa que el mensaje es visible
            mensajeExito.Displayed.Should().BeTrue("El mensaje de exito debería mostrarse"); // Verificar que el mensaje de éxito se muestra

            // Verificar que no hay mensajes de error visibles
            bool hayErrores = driver.FindElements(By.ClassName("invalid-feedback")).Any(element => element.Displayed); // Verificar si hay mensajes de error
            hayErrores.Should().BeFalse("No debería haber mensajes de error visibles"); // Verificar que no hay mensajes de error visibles


        }

        [Test]
        public void PruebaCamposRequeridosVacios()
        {
            Console.WriteLine("Iniciando prueba de campos requeridos vacíos..");
        
            // Hacemos Click en el botón "Enviar" id=boton-enviar
            IWebElement botonEnviar = driver.FindElement(By.Id("boton-enviar")); // Botón Enviar
            actions.MoveToElement(botonEnviar).Click().Perform(); // Hacer click en el botón
        
           
        
            IWebElement inputTexto = driver.FindElement(By.Id("input-texto"));
            inputTexto.GetAttribute("class").Should().Contain("is-invalid");
        
            IWebElement mensajeErrorTexto = inputTexto.FindElement(By.XPath("following-sibling::div[@class='invalid-feedback']"));
            mensajeErrorTexto.Text.Should().Be("El texto debe tener más de 3 caracteres.");
        
            IWebElement inputEmail = driver.FindElement(By.Id("input-email"));
            inputEmail.GetAttribute("class").Should().Contain("is-invalid");
            IWebElement mensajeErrorEmail = inputEmail.FindElement(By.XPath("following-sibling::div[@class='invalid-feedback']"));
            mensajeErrorEmail.Text.Should().Be("Por favor, ingresa un correo electrónico válido (nombre@organizacion.dominio)");
        
            IWebElement inputPassword = driver.FindElement(By.Id("input-password"));
            inputPassword.GetAttribute("class").Should().Contain("is-invalid");
        
            IWebElement mensajeErrorPassword = inputPassword.FindElement(By.XPath("following-sibling::div[@class='invalid-feedback']"));
            mensajeErrorPassword.Text.Should().Be("La contraseña debe tener más de 8 caracteres.");
            // select-opciones
        
            IWebElement selectOpciones = driver.FindElement(By.Id("select-opciones"));
            selectOpciones.GetAttribute("class").Should().Contain("is-invalid");
        
            IWebElement mensajeErrorSelect = selectOpciones.FindElement(By.XPath("following-sibling::div[@class='invalid-feedback']"));
            mensajeErrorSelect.Text.Should().Be("Por favor, selecciona una opción.");
            // select-opciones
        
            // Seleccionamos un radio button
            IWebElement feedBackRadio = driver.FindElement(By.CssSelector("fieldset .invalid-feedback")); // Radio button 1
            feedBackRadio.Text.Should().Be("Por favor, selecciona una opción de radio.");
        
            // Verificar el checbox
            IWebElement checkbox = driver.FindElement(By.Id("checkbox1"));
            checkbox.GetAttribute("class").Should().Contain("is-invalid");
        
            // Verificamos que el mensaje de éxito no se muestra
            bool mensajeExito = driver.FindElements(By.Id("mensaje-exito")).Any(element => element.Displayed);
            mensajeExito.Should().BeFalse("el mensaje de exito no deberia mostrarse");
        
            Console.WriteLine("Prueba de campos requeridos vacios completada");
        }
    }
}
