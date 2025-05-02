using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Un singleton que permite ejecutar acciones en el hilo principal de Unity desde otros hilos.
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher _instance = null;
    private readonly Queue<Action> _executionQueue = new Queue<Action>();
    private readonly object _lock = new object();
    private bool _isQuitting = false;

    public static UnityMainThreadDispatcher Instance()
    {
        if (_instance == null)
        {
            // Buscar primero si ya existe una instancia
            _instance = FindFirstObjectByType<UnityMainThreadDispatcher>();
            
            // Si no existe, crear una nueva
            if (_instance == null)
            {
                GameObject go = new GameObject("UnityMainThreadDispatcher");
                _instance = go.AddComponent<UnityMainThreadDispatcher>();
                DontDestroyOnLoad(go);
            }
        }
        
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    void Update()
    {
        // Realizar todas las acciones en cola en cada frame
        lock (_lock)
        {
            while (_executionQueue.Count > 0)
            {
                Action action = _executionQueue.Dequeue();
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error al ejecutar acción en hilo principal: {e.Message}");
                    Debug.LogError($"Stack trace: {e.StackTrace}");
                }
            }
        }
    }

    /// <summary>
    /// Encola una acción para ser ejecutada en el hilo principal de Unity
    /// </summary>
    /// <param name="action">La acción a ejecutar</param>
    /// <returns>La misma acción para encadenamiento</returns>
    public void Enqueue(Action action)
    {
        if (_isQuitting)
        {
            Debug.LogWarning("No se puede encolar acción: la aplicación está cerrándose");
            return;
        }
        
        lock (_lock)
        {
            _executionQueue.Enqueue(action);
        }
    }

    /// <summary>
    /// Ejecuta una acción en el hilo principal de Unity y espera a que se complete
    /// </summary>
    /// <param name="action">La acción a ejecutar</param>
    /// <returns>Tarea que se completa cuando la acción se ha ejecutado</returns>
    public Task EnqueueAsync(Action action)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        
        Enqueue(() =>
        {
            try
            {
                action();
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });
        
        return tcs.Task;
    }
}