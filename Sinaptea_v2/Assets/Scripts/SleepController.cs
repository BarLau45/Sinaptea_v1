using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SleepController : MonoBehaviour
{
    public Button sleepButton;
    public int recoveryRate = 5;
    private bool asleep = false;
    
    void Start()
    {
        sleepButton = GameObject.Find("Canvas/BotonDormir").GetComponent<Button>();
        sleepButton.onClick.AddListener(ToggleSleep);
    }

    void ToggleSleep()
    {
        if(!asleep)
        {
            asleep = true;
            TMP_Text toChange;
            toChange = GameObject.Find("DormirTexto").GetComponent<TMP_Text>();
            toChange.text = "¡Despierta!";
            InvokeRepeating("RecoverEnergy", 0, 1f);
        } else
        {
            asleep = false;
            TMP_Text toChange;
            toChange = GameObject.Find("DormirTexto").GetComponent<TMP_Text>();
            toChange.text = "¡Buenas noches!";
            CancelInvoke("RecoverEnergy");
        }
    }

    
    void RecoverEnergy()
    {
        if (DataManager.Instance == null) return;

        int currentEnergy = DataManager.Instance.GetEnergy();
        if (currentEnergy < 100)
        {
            int newEnergy = Mathf.Min(currentEnergy + recoveryRate, 100);
            DataManager.Instance.SetEnergy(newEnergy);
        }
    }
}
