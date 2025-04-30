using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private const string ENERGY_KEY = "PlayerEnergy";
    private const string SATIETY_KEY = "PlayerSatiety";
    private const string JOY_KEY = "PlayerJoy";

    private const int DEFAULT_ENERGY = 100;
    private const int DEFAULT_SATIETY = 100;
    private const int DEFAULT_JOY = 100;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Métodos para gestionar la energía
    public int GetEnergy()
    {
        return PlayerPrefs.GetInt(ENERGY_KEY, DEFAULT_ENERGY);
    }

    public void SetEnergy(int value)
    {
        PlayerPrefs.SetInt(ENERGY_KEY, value);
        PlayerPrefs.Save();
    }

    public void DecreaseEnergy(int amount)
    {
        int currentEnergy = GetEnergy();
        SetEnergy(currentEnergy - amount);
    }

    // Métodos para la saciedad
    public int GetSatiety()
    {
        return PlayerPrefs.GetInt(SATIETY_KEY, DEFAULT_SATIETY);
    }

    public void SetSatiety(int value)
    {
        PlayerPrefs.SetInt(SATIETY_KEY, value);
        PlayerPrefs.Save();
    }

    // Métodos para la alegría
    public int GetJoy()
    {
        return PlayerPrefs.GetInt(JOY_KEY, DEFAULT_JOY);
    }

    public void SetJoy(int value)
    {
        PlayerPrefs.SetInt(JOY_KEY, value);
        PlayerPrefs.Save();
    }

}
