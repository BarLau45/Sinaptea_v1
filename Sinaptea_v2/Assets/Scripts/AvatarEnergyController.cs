using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float energyDrainRate = 5f;
    public float sleepRecoveryRate = 10f;
    public Button botonDormir;
    private float currentEnergy;
    private bool isSleeping = false;
    private float energyVelocity = 0;

    public Slider energyBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
        botonDormir = GameObject.Find("Canvas/BotonDormir")?.GetComponent<Button>();
        botonDormir.onClick.AddListener(Sleep);
        energyBar = GameObject.Find("Canvas/BarraEnergia")?.GetComponent<Slider>();
        energyBar.interactable = false;
        if (energyBar == null)
        {
            Debug.LogError("No se encontró el elemento");
        }
        else
        {
            Debug.Log("Barra encontrada");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSleeping)
        {
            DrainEnergy();
        }
        else
        {
            RecoverEnergy();
        }
        UpdateEnergyUI();
    }

    void DrainEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= energyDrainRate * Time.deltaTime;
        }
        else
        {
            currentEnergy = 0;
        }
    }

    void RecoverEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += sleepRecoveryRate * Time.deltaTime;
        }
        else
        {
            currentEnergy = maxEnergy;
            Awake();
        }
    }

    void UpdateEnergyUI()
    {
        if(energyBar)
        {
            float targetValue = currentEnergy / maxEnergy;
            energyBar.value = Mathf.SmoothDamp(energyBar.value, targetValue, ref energyVelocity, 0.5f);
        }
    }

    public void Sleep()
    {
        isSleeping = true;
    }

    public void Awake()
    {
        isSleeping = false;
    }
}
