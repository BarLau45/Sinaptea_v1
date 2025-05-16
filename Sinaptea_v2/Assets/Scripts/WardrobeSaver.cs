using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WardrobeSaver : MonoBehaviour
{
    public Button submitButton;
    public Button resetButton;
    public List<WardrobeController> wears = new List<WardrobeController>();
    public List<string> wearTypes = new List<string>();

    void Start()
    {
        if(DataManager.Instance == null)
        {
            Debug.LogError("DataManager no encontrado");
            return;
        }
        LoadAllWearings();
        submitButton.onClick.AddListener(SaveAllWearings);
        if (wears.Count != wearTypes.Count)
        {
            Debug.LogError("Error de configuración: El número de controladores de prendas (" + 
                          wears.Count + ") no coincide con los tipos definidos (" + 
                          wearTypes.Count + ")");
        }
    }

    private void SaveSingleWearing(string type, string spriteName)
    {
        switch(type.ToLower())
        {
            case "hair":
                DataManager.Instance.SetHair(spriteName);
                break;
            case "shirt":
                DataManager.Instance.SetShirt(spriteName);
                break;
            case "pants":
                DataManager.Instance.SetPants(spriteName);
                break;
            case "shoes":
                DataManager.Instance.SetShoes(spriteName);
                break;
        }
    }

    public void SaveAllWearings()
    {
        for (int i = 0; i < wears.Count; i++)
        {
            if (wears[i] != null)
            {
                string spriteName = wears[i].submitWear();
                SaveSingleWearing(wearTypes[i], spriteName);
            }
        }
        PlayerPrefs.Save();
        Scene thisScene = SceneManager.GetActiveScene();
        string thisSceneName = thisScene.name;
        if (thisSceneName == "CrearAvatarScene")
        {
            SceneManager.LoadScene("LoginScene");
        }
        SceneManager.LoadScene("SampleScene");
    }

    private void LoadSingleWearing(string type, WardrobeController controller)
    {
        string savedSpriteName = "";
        switch (type.ToLower())
        {
            case "hair":
                savedSpriteName = DataManager.Instance.GetHair();
                break;
            case "shirt":
                savedSpriteName = DataManager.Instance.GetShirt();
                break;
            case "pants":
                savedSpriteName = DataManager.Instance.GetPants();
                break;
            case "shoes":
                savedSpriteName = DataManager.Instance.GetShoes();
                break;
            default:
                Debug.LogWarning("Tipo de prenda no reconocido: " + type);
                return;
        }

        for (int i = 0; i < controller.options.Count; i++)
        {
            if (controller.options[i] != null && controller.options[i].name == savedSpriteName)
            {
                controller.currentOption = i;
                controller.wear.sprite = controller.options[i];
                break;
            }
        }
    }

    public void LoadAllWearings()
    {
        if (wears.Count != wearTypes.Count)
        {
            Debug.LogError("El número de controladores de prendas no coincide con los tipos definidos");
            return;
        }

        for (int i = 0; i < wears.Count; i++)
        {
            if (wears[i] != null)
            {
                LoadSingleWearing(wearTypes[i], wears[i]);
            }
        }
    }

    public void ResetAvatarPrefs()
    {
        PlayerPrefs.DeleteKey("AvatarHair");
        PlayerPrefs.DeleteKey("AvatarShirt");
        PlayerPrefs.DeleteKey("AvatarPants");
        PlayerPrefs.DeleteKey("AvatarShoes");
        PlayerPrefs.Save();
        SceneManager.LoadScene("ArmarioScene");
    }

}
