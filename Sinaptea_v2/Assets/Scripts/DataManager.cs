using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private const string ENERGY_KEY = "PlayerEnergy";
    private const string SATIETY_KEY = "PlayerSatiety";
    private const string JOY_KEY = "PlayerJoy";

    private const string HAIR_KEY = "AvatarHair";
    private const string SHIRT_KEY = "AvatarShirt";
    private const string PANTS_KEY = "AvatarPants";
    private const string SHOES_KEY = "AvatarShoes";

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

    // Métodos para las prendas personalizadas

    public string GetHair()
    {
        return PlayerPrefs.GetString(HAIR_KEY, "");
    }

    public void SetHair(string name)
    {
        PlayerPrefs.SetString(HAIR_KEY, name);
        PlayerPrefs.Save();
    }

    public string GetShirt()
    {
        return PlayerPrefs.GetString(SHIRT_KEY, "");
    }

    public void SetShirt(string name)
    {
        PlayerPrefs.SetString(SHIRT_KEY, name);
        PlayerPrefs.Save();
    }

    public string GetPants()
    {
        return PlayerPrefs.GetString(PANTS_KEY, "");
    }

    public void SetPants(string name)
    {
        PlayerPrefs.SetString(PANTS_KEY, name);
        PlayerPrefs.Save();
    }

    public string GetShoes()
    {
        return PlayerPrefs.GetString(SHOES_KEY, "");
    }

    public void SetShoes(string name)
    {
        PlayerPrefs.SetString(SHOES_KEY, name);
        PlayerPrefs.Save();
    }
}
