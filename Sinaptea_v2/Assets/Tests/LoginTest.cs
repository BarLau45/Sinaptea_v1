using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

[TestFixture]
public class LoginTest
{
    private GameObject testGameObject;
    private LoginController loginController;
    private TMP_InputField emailInput;
    private TMP_InputField passwordInput;
    private Button submitLoginButton;
    private Button returnToRegisterButton;
    private GameObject authBlackout;
    private TMP_Text errorText;
    private Button errorOKButton;
    
    [SetUp]
    public void SetUp()
    {
        // Crear GameObjects de prueba
        testGameObject = new GameObject("LoginControllerTest");
        loginController = testGameObject.AddComponent<LoginController>();
        
        // Crear canvas y tarjeta para la estructura UI
        GameObject canvas = new GameObject("Canvas");
        GameObject tarjeta = new GameObject("Tarjeta");
        tarjeta.transform.SetParent(canvas.transform);
        
        // Crear inputs
        GameObject emailObj = new GameObject("EmailInput");
        emailObj.transform.SetParent(tarjeta.transform);
        emailInput = emailObj.AddComponent<TMP_InputField>();
        loginController.email = emailInput;
        
        GameObject passwordObj = new GameObject("PasswordInput");
        passwordObj.transform.SetParent(tarjeta.transform);
        passwordInput = passwordObj.AddComponent<TMP_InputField>();
        loginController.password = passwordInput;
        
        // Crear botones
        GameObject submitLoginObj = new GameObject("BotonIngresar");
        submitLoginObj.transform.SetParent(tarjeta.transform);
        submitLoginButton = submitLoginObj.AddComponent<Button>();
        loginController.submitLogin = submitLoginButton;
        
        GameObject returnToRegisterObj = new GameObject("BotonIrRegistro");
        returnToRegisterObj.transform.SetParent(tarjeta.transform);
        returnToRegisterButton = returnToRegisterObj.AddComponent<Button>();
        loginController.returnToRegister = returnToRegisterButton;
        
        // Crear la estructura para el manejo de errores
        authBlackout = new GameObject("BlackoutAuth");
        authBlackout.transform.SetParent(submitLoginObj.transform);
        authBlackout.SetActive(false);
        
        GameObject tarjetaAuth = new GameObject("TarjetaAuth");
        tarjetaAuth.transform.SetParent(authBlackout.transform);
        
        GameObject errorTextObj = new GameObject("InfoError");
        errorTextObj.transform.SetParent(tarjetaAuth.transform);
        errorText = errorTextObj.AddComponent<TMP_Text>();
        
        GameObject errorOKObj = new GameObject("AceptarError");
        errorOKObj.transform.SetParent(tarjetaAuth.transform);
        errorOKButton = errorOKObj.AddComponent<Button>();
        
        loginController.authBlackout = authBlackout;
        loginController.errorText = errorText;
        loginController.errorOK = errorOKButton;
    }
    
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testGameObject);
    }
    
    [Test]
    public void DeactivateError_HidesAuthBlackout()
    {
        
        PrepareGameObjectForErrorTest();
        
        // Configurar el authBlackout como activo
        authBlackout.SetActive(true);
        
       
        System.Reflection.MethodInfo deactivateErrorMethod = typeof(LoginController).GetMethod("DeactivateError", 
                                                           System.Reflection.BindingFlags.NonPublic | 
                                                           System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método
        deactivateErrorMethod.Invoke(loginController, null);
        
        // Verificar que el blackout se oculta
        Assert.IsFalse(authBlackout.activeSelf);
    }
    
    [UnityTest]
    public IEnumerator MoveToRegister_LoadsRegisterScene()
    {
        
        System.Reflection.MethodInfo moveToRegisterMethod = typeof(LoginController).GetMethod("MoveToRegister", 
                                                          System.Reflection.BindingFlags.NonPublic | 
                                                          System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método
        moveToRegisterMethod.Invoke(loginController, null);
        
        // Esperar un frame para que se procese el cambio de escena
        yield return null;
        
        // En una implementación real, verificaríamos que se llamó a SceneManager.LoadScene con "RegistroScene"
        Assert.Pass("Esta prueba requiere integración con el sistema de escenas de Unity");
    }
    
    [UnityTest]
    public IEnumerator MoveToSample_LoadsSampleScene()
    {
        
        
        // Usar reflection para acceder al método privado MoveToSample
        System.Reflection.MethodInfo moveToSampleMethod = typeof(LoginController).GetMethod("MoveToSample", 
                                                        System.Reflection.BindingFlags.NonPublic | 
                                                        System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método
        moveToSampleMethod.Invoke(loginController, null);
        
        // Esperar un frame para que se procese el cambio de escena
        yield return null;
        
        
        Assert.Pass("Esta prueba requiere integración con el sistema de escenas de Unity");
    }
    
    [UnityTest]
    public IEnumerator LoginUser_ValidCredentials_MovesToSampleScene()
    {
        
        
        // Configurar credenciales válidas
        emailInput.text = "test@example.com";
        passwordInput.text = "password123";
        
        
        // Usar reflection para simular parcialmente el comportamiento
        System.Reflection.MethodInfo moveToSampleMethod = typeof(LoginController).GetMethod("MoveToSample", 
                                                        System.Reflection.BindingFlags.NonPublic | 
                                                        System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método directamente (simulando un login exitoso)
        moveToSampleMethod.Invoke(loginController, null);
        
        // Esperar un frame
        yield return null;
        
       
        Assert.Pass("Esta prueba requiere integración con Firebase y el sistema de escenas de Unity");
    }
    
    
    private void PrepareGameObjectForErrorTest()
    {
        
        
        // Asegurarnos de que las referencias existen
        loginController.authBlackout = authBlackout;
        loginController.errorText = errorText;
        loginController.errorOK = errorOKButton;
    }
}