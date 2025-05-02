using UnityEngine;
using Firebase.Auth;
using System;

public class AuthManager : MonoBehaviour
{
    // Instancia singleton
    private static AuthManager _instance;
    public static AuthManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente
                _instance = FindFirstObjectByType<AuthManager>();
                
                // Si no existe, crear una nueva
                if (_instance == null)
                {
                    try 
                    {
                        GameObject obj = new GameObject("AuthManager");
                        _instance = obj.AddComponent<AuthManager>();
                        DontDestroyOnLoad(obj);
                        Debug.Log("Se ha creado una nueva instancia de AuthManager");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error al crear AuthManager: {e.Message}");
                        throw;
                    }
                }
            }
            return _instance;
        }
    }

    // Firebase Auth
    private FirebaseAuth auth;
    private bool isInitialized = false;
    
    // Evento para notificar cambios en el estado de autenticación
    public event Action<FirebaseUser> OnAuthStateChanged;

    void Awake()
    {
        // Singleton pattern
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Se destruyó una instancia duplicada de AuthManager");
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("AuthManager Awake - Implementando singleton");
        
        // Inicializar Firebase Auth
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        try
        {
            Debug.Log("Inicializando Firebase Auth...");
            
            // Verificar si FirebaseAuth está disponible
            if (FirebaseAuth.DefaultInstance == null)
            {
                Debug.LogError("Firebase Auth no está inicializado. Asegúrate de que Firebase esté correctamente configurado.");
                return;
            }
            
            auth = FirebaseAuth.DefaultInstance;
            
            // Suscribirse al evento de cambio de estado
            auth.StateChanged += AuthStateChanged;
            
            // Verificar el estado actual de autenticación
            AuthStateChanged(auth, null);
            
            isInitialized = true;
            Debug.Log("Firebase Auth inicializado correctamente");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al inicializar Firebase Auth: {ex.Message}");
            Debug.LogError($"Stack trace: {ex.StackTrace}");
        }
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        try
        {
            if (auth != null && auth.CurrentUser != null)
            {
                // El usuario está autenticado
                Debug.Log($"Usuario autenticado: {auth.CurrentUser.Email}");
            }
            else
            {
                // El usuario no está autenticado
                Debug.Log("Usuario no autenticado");
            }
            
            // Notificar a los listeners
            if (auth != null)
            {
                OnAuthStateChanged?.Invoke(auth.CurrentUser);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error en AuthStateChanged: {ex.Message}");
        }
    }

    // Obtener el usuario actual
    public FirebaseUser GetCurrentUser()
    {
        try
        {
            if (!isInitialized || auth == null)
            {
                Debug.LogWarning("AuthManager no inicializado. Intentando inicializar...");
                InitializeFirebase();
            }
            
            return auth?.CurrentUser;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al obtener usuario actual: {ex.Message}");
            return null;
        }
    }

    // Verificar si hay un usuario autenticado
    public bool IsUserLoggedIn()
    {
        try
        {
            if (!isInitialized || auth == null)
            {
                Debug.LogWarning("AuthManager no inicializado. Intentando inicializar...");
                InitializeFirebase();
            }
            
            bool isLoggedIn = auth?.CurrentUser != null;
            Debug.Log($"IsUserLoggedIn: {isLoggedIn}");
            return isLoggedIn;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al verificar estado de inicio de sesión: {ex.Message}");
            return false;
        }
    }

    // Cerrar sesión
    public void SignOut()
    {
        try
        {
            if (!isInitialized || auth == null)
            {
                Debug.LogWarning("AuthManager no inicializado. Intentando inicializar...");
                InitializeFirebase();
            }
            
            if (auth != null)
            {
                auth.SignOut();
                Debug.Log("Sesión cerrada correctamente");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al cerrar sesión: {ex.Message}");
        }
    }

    // Método para actualizar/refrescar el token del usuario
    public async void RefreshUserToken()
    {
        try
        {
            if (!isInitialized || auth == null)
            {
                Debug.LogWarning("AuthManager no inicializado. Intentando inicializar...");
                InitializeFirebase();
            }
            
            if (IsUserLoggedIn())
            {
                await auth.CurrentUser.ReloadAsync();
                Debug.Log("Token de usuario actualizado");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al actualizar token: {ex.Message}");
        }
    }

    void OnDestroy()
    {
        // Limpiar event handlers
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
            Debug.Log("AuthManager destruido y event handlers limpiados");
        }
    }
}