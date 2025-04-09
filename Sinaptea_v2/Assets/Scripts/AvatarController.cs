using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Avatar Instance;

    public float timeScale = 1;
    public int energy = 100;
    public int energyWasteRate = 2;
    public int satiety = 100;
    public int joy = 100;
    int timeWasted;

    public Slider energyBar;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(energyBar);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
        InvokeRepeating("UpdateMeters", 0, timeScale);
        energyBar = GameObject.Find("Canvas/Bateria/BarraEnergia").GetComponent<Slider>();
    }

    void SetUp()
    {
        energyBar.value = energy;
    }

    // Update is called once per frame
    int energyTime;

    void UpdateMeters()
    {
        if (energyTime > energyWasteRate)
        {
            energyTime = 0;
            energy--;
            energyBar.value = energy;
            
        }
        energyTime++;
        timeWasted++;
    }
    void Update()
    {
        // PRUEBA DE PUSH AL REPOSITORIO
        
    }
}
