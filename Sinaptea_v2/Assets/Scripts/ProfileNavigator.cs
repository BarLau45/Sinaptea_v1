using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileDirectioner : MonoBehaviour
{

    public Button profile;
    void Start()
    {
        profile = GameObject.Find("BotonPerfil").GetComponent<Button>();
        profile.onClick.AddListener(NavigateToProfile);
    }

    void NavigateToProfile()
    {
        SceneManager.LoadScene("PerfilScene");
    }
}
