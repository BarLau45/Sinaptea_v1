using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileController: MonoBehaviour
{
    public Button privacy;

    void Start()
    {
        privacy = GameObject.Find("Canvas/BotonPolitica").GetComponent<Button>();
        privacy.onClick.AddListener(MoveToPrivacy);
    }
    void MoveToPrivacy()
    {
        SceneManager.LoadScene("PoliticaScene");
    }
}
