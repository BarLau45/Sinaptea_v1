using UnityEngine;

public class Avatar : MonoBehaviour
{
    public float timeScale = 20f;
    public int energyWasteRate = 1;

    private int energyTime = 0;
    private int timeWasted = 0;

    [Header("Partes del avatar")]
    public SpriteRenderer hairRenderer;
    public SpriteRenderer shirtRenderer;
    public SpriteRenderer pantsRenderer;
    public SpriteRenderer shoesRenderer;

    [Header("Sprites disponibles")]
    public Sprite[] hairSprites;
    public Sprite[] shirtSprites;
    public Sprite[] pantsSprites;
    public Sprite[] shoesSprites;

    void Start()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager no encontrado. Asegúrate de que existe en la escena.");
            return;
        }

        InvokeRepeating("UpdateMeters", 0, timeScale);
        LoadAvatarSprite();
    }

    void UpdateMeters()
    {
        if (DataManager.Instance == null) return;

        if (energyTime > energyWasteRate)
        {
            energyTime = 0;

            int currentEnergy = DataManager.Instance.GetEnergy();
            currentEnergy = Mathf.Max(0, currentEnergy - 1);
            DataManager.Instance.SetEnergy(currentEnergy);

        }
        energyTime++;
        timeWasted++;
    }

    private void LoadSpriteByName(SpriteRenderer renderer, Sprite[] options, string spriteName)
    {
        if (string.IsNullOrEmpty(spriteName) || renderer == null)
        {
            Debug.LogWarning($"No se pudo cargar sprite: {spriteName} es nulo o vacío");
            return;
        }

        bool found = false;
        foreach (Sprite sprite in options)
        {
            if (sprite != null && sprite.name == spriteName)
            {
                renderer.sprite = sprite;
                found = true;
                break;
            }
        if (!found)
        {
            Debug.LogWarning($"No se encontró el sprite con nombre: {spriteName}");
        }

        }
    }

    public void LoadAvatarSprite()
    {
        LoadSpriteByName(hairRenderer, hairSprites, DataManager.Instance.GetHair());
        LoadSpriteByName(shirtRenderer, shirtSprites, DataManager.Instance.GetShirt());
        LoadSpriteByName(pantsRenderer, pantsSprites, DataManager.Instance.GetPants());
        LoadSpriteByName(shoesRenderer, shoesSprites, DataManager.Instance.GetShoes());
    }

}
