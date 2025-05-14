using UnityEngine;

public class Avatar : MonoBehaviour
{
    public float timeScale = 20f;
    public int energyWasteRate = 1;
    
    private int energyTime = 0;
    private int timeWasted = 0;
    
    void Start()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager no encontrado. Asegúrate de que existe en la escena.");
            return;
        }
        
        InvokeRepeating("UpdateMeters", 0, timeScale);
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
    
    
}