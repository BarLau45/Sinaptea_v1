using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{

    public Slider energyBar;
    public float timeScale = 20f;
    public int energyWasteRate = 1;
    
    private int energyTime = 0;
    
    void Start()
    {
        if (energyBar == null)
        {
            energyBar = GameObject.Find("Canvas/Bateria/BarraEnergia").GetComponent<Slider>();
        }
        
        if (DataManager.Instance != null)
        {
            energyBar.value = DataManager.Instance.GetEnergy();
        }

        InvokeRepeating("UpdateEnergy", 0, timeScale);
    }
    
    void UpdateEnergy()
    {
        if (DataManager.Instance == null) return;
        
        if (energyTime > energyWasteRate)
        {
            energyTime = 0;

            int currentEnergy = DataManager.Instance.GetEnergy();
            currentEnergy--;
            DataManager.Instance.SetEnergy(currentEnergy);

            if (energyBar != null)
            {
                energyBar.value = currentEnergy;
            }
        }
        energyTime++;
    }

}
