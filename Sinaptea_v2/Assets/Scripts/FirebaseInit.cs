using UnityEngine;
using Firebase.Analytics;
using Firebase;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class FirebaseInit : MonoBehaviour
{
    public GameObject loadingScreen;
    public float initializationTimeout = 10f;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(InitializeFirebase());
        loadingScreen = GameObject.Find("LoadingScreen");
    }

    IEnumerator InitializeFirebase()
    {
        loadingScreen.SetActive(true);
        float timeElapsed = 0f;
        bool initializationComplete = false;
        Debug.Log("Verificando dependencias de Firebase...");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            try 
            {
                var dependencyStatus = task.Result;
                Debug.Log($"Estado de dependencias: {dependencyStatus}");
                initializationComplete = true;
                
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // Firebase está listo para usar
                    Debug.Log("Firebase inicializado correctamente");
                    
                    // Habilitar Firebase Analytics (mantenemos tu código original)
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    
                    // Asegurarnos de ejecutar este código en el hilo principal
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        try
                        {
                            // Inicializar AuthManager (si no está ya inicializado)
                            Debug.Log("Inicializando AuthManager");
                            var authManager = AuthManager.Instance;
                            
                            // Intentar comprobar si hay una sesión activa
                            bool isLoggedIn = false;
                            try
                            {
                                isLoggedIn = authManager.IsUserLoggedIn();
                                Debug.Log($"Estado de inicio de sesión: {isLoggedIn}");
                            }
                            catch (Exception e)
                            {
                                Debug.LogError($"Error al verificar el estado de inicio de sesión: {e.Message}");
                            }
                            
                            // Decidir a qué escena ir
                            Debug.Log("Navegando a la escena apropiada...");
                            
                            if (isLoggedIn)
                            {
                                Debug.Log("Usuario con sesión iniciada, cargando MainScene");
                                SceneManager.LoadScene("MainScene");
                            }
                            else
                            {
                                Debug.Log("Usuario sin sesión, cargando LoginScene");
                                SceneManager.LoadScene("LoginScene");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error en la fase final de inicialización: {e.Message}");
                            Debug.LogError($"Stack trace: {e.StackTrace}");
                            // En caso de error, redirigir a la pantalla de login como fallback seguro
                            SceneManager.LoadScene("LoginScene");
                        }
                    });
                }
                else
                {
                    Debug.LogError($"No se pudieron resolver las dependencias de Firebase: {dependencyStatus}");
                    // En caso de error, ejecutar en el hilo principal
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        // Ir a login como fallback en caso de error
                        SceneManager.LoadScene("LoginScene");
                    });
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error durante la inicialización de Firebase: {e.Message}");
                Debug.LogError($"Stack trace: {e.StackTrace}");
                initializationComplete = true;
                
                // En caso de error, ejecutar en el hilo principal
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    // Ir a login como fallback en caso de error
                    SceneManager.LoadScene("LoginScene");
                });
            }
        });
        
        // Esperar a que la inicialización se complete o se agote el tiempo
        while (!initializationComplete && timeElapsed < initializationTimeout)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        // Si se agota el tiempo, forzar la carga de la escena de login
        if (!initializationComplete)
        {
            Debug.LogWarning($"Tiempo de inicialización de Firebase agotado después de {timeElapsed} segundos");
            SceneManager.LoadScene("LoginScene");
        }
        
        // Ocultar pantalla de carga
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
            Debug.Log("Pantalla de carga desactivada");
        }
    }
}
