using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectActivity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void elegirAct1()
    {
        SceneManager.LoadScene("MemoramaScene");
    }

    public void elegirAct2()
    {
        SceneManager.LoadScene("PalabrasScene");
    }
}
