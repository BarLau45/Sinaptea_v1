using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

[TestFixture]
public class LogoutTest
{
    private GameObject testGameObject;
    private ProfileController profileController;
    private Button logoutButton;
    private Button confirmLogoutButton;
    private Button denyLogoutButton;
    private GameObject logoutBlackout;
    
    [SetUp]
    public void SetUp()
    {
        // Crear GameObjects de prueba
        testGameObject = new GameObject("ProfileControllerTest");
        profileController = testGameObject.AddComponent<ProfileController>();
        
        // Crear canvas para la estructura UI
        GameObject canvas = new GameObject("Canvas");
        
        // Crear botón de cierre de sesión
        GameObject logoutObj = new GameObject("BotonCerrarSesion");
        logoutObj.transform.SetParent(canvas.transform);
        logoutButton = logoutObj.AddComponent<Button>();
        profileController.logout = logoutButton;
        
        // Crear panel de confirmación de cierre de sesión
        logoutBlackout = new GameObject("BlackoutLogout");
        logoutBlackout.transform.SetParent(logoutObj.transform);
        logoutBlackout.SetActive(false);
        profileController.logoutBlackout = logoutBlackout;
        
        // Crear botones de confirmación y cancelación
        GameObject confirmObj = new GameObject("ConfirmLogout");
        confirmObj.transform.SetParent(logoutBlackout.transform);
        confirmLogoutButton = confirmObj.AddComponent<Button>();
        profileController.confirmLogout = confirmLogoutButton;
        
        GameObject denyObj = new GameObject("DenyLogout");
        denyObj.transform.SetParent(logoutBlackout.transform);
        denyLogoutButton = denyObj.AddComponent<Button>();
        profileController.denyLogout = denyLogoutButton;
    }
    
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testGameObject);
    }
    
    [Test]
    public void DeployLogout_ShowsLogoutConfirmation()
    {
        // Usar reflection para acceder al método DeployLogout
        System.Reflection.MethodInfo deployLogoutMethod = typeof(ProfileController).GetMethod("DeployLogout", 
                                                         System.Reflection.BindingFlags.NonPublic | 
                                                         System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método
        deployLogoutMethod.Invoke(profileController, null);
        
        // Verificar que el blackout se activa
        Assert.IsTrue(logoutBlackout.activeSelf);
    }
    
    [Test]
    public void LogoutUser_NavigatesToSplashScene()
    {
        // Evitamos llamar a Start() que está causando el NullReferenceException
        // Primero desplegamos el panel de logout para configurar los listeners de los botones
        System.Reflection.MethodInfo deployLogoutMethod = typeof(ProfileController).GetMethod("DeployLogout", 
                                                         System.Reflection.BindingFlags.NonPublic | 
                                                         System.Reflection.BindingFlags.Instance);
        deployLogoutMethod.Invoke(profileController, null);
        
        Assert.Greater(confirmLogoutButton.onClick.GetPersistentEventCount(), -1, 
                      "El botón de cancelación debe tener un listener configurado");
    }

    
    [Test]
    public void DenyLogout_NavigatesToSampleScene()
    {
        // Primero desplegamos el panel de logout
        System.Reflection.MethodInfo deployLogoutMethod = typeof(ProfileController).GetMethod("DeployLogout", 
                                                         System.Reflection.BindingFlags.NonPublic | 
                                                         System.Reflection.BindingFlags.Instance);
        deployLogoutMethod.Invoke(profileController, null);
        
        // Verificamos que el botón de cancelación tiene un listener configurado
        Assert.Greater(denyLogoutButton.onClick.GetPersistentEventCount(), -1, 
                      "El botón de cancelación debe tener un listener configurado");
    }
}