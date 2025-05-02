using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectWord : MonoBehaviour
{
    public TMP_InputField inputText;
    public TMP_InputField inputText2;
    public TMP_InputField inputText3;
    public TMP_InputField inputText4;
    public TMP_InputField inputText5;
    public Image luz;
    public Image luz2;
    public Image luz3;
    public Image luz4;
    public Image luz5;

    public Button botonAceptar;
    public Button botonCheck;
    public Button botonCheck2;
    public Button botonCheck3;
    public Button botonCheck4;
    public Button botonCheck5;

    public string palabraCorrecta = "manzana";
    public string palabraCorrecta2 = "flor";
    public string palabraCorrecta3 = "miel";
    public string palabraCorrecta4 = "pera";
    public string palabraCorrecta5 = "sol";

    private void Awake()
    {

        luz.color = Color.red;
        luz2.color = Color.red;
        luz3.color = Color.red;
        luz4.color = Color.red;
        luz5.color = Color.red;

        // Asegura que el botón llama a la función aceptar
        botonAceptar = GameObject.Find("BotonAceptar").GetComponent<Button>();
        botonAceptar.onClick.AddListener(aceptar);

        //Boton 1
        botonCheck = GameObject.Find("BotonCheck").GetComponent<Button>();
        botonCheck.onClick.AddListener(check);

        //Boton 2
        botonCheck2 = GameObject.Find("BotonCheck2").GetComponent<Button>();
        botonCheck2.onClick.AddListener(check);

        //Boton 3
        botonCheck3 = GameObject.Find("BotonCheck3").GetComponent<Button>();
        botonCheck3.onClick.AddListener(check);

        //Boton 4
        botonCheck4 = GameObject.Find("BotonCheck4").GetComponent<Button>();
        botonCheck4.onClick.AddListener(check);

        //Boton 5
        botonCheck5 = GameObject.Find("BotonCheck5").GetComponent<Button>();
        botonCheck5.onClick.AddListener(check);
    }

    public void check()
    {
        // Check 1
        string respuestaUsuario = inputText.text.Trim().ToLower();

        if (respuestaUsuario == palabraCorrecta.ToLower())
        {
            luz.color = Color.green;
        }
        else
        {
            luz.color = Color.red;
        }

        // Check 2
        string respuestaUsuario2 = inputText2.text.Trim().ToLower();

        if (respuestaUsuario2 == palabraCorrecta2.ToLower())
        {
            luz2.color = Color.green;
        }
        else
        {
            luz2.color = Color.red;
        }

        // Check 3
        string respuestaUsuario3 = inputText3.text.Trim().ToLower();

        if (respuestaUsuario3 == palabraCorrecta3.ToLower())
        {
            luz3.color = Color.green;
        }
        else
        {
            luz3.color = Color.red;
        }

        // Check 4
        string respuestaUsuario4 = inputText4.text.Trim().ToLower();

        if (respuestaUsuario4 == palabraCorrecta4.ToLower())
        {
            luz4.color = Color.green;
        }
        else
        {
            luz4.color = Color.red;
        }

        // Check 2
        string respuestaUsuario5 = inputText5.text.Trim().ToLower();

        if (respuestaUsuario5 == palabraCorrecta5.ToLower())
        {
            luz5.color = Color.green;
        }
        else
        {
            luz5.color = Color.red;
        }
    }

    public void aceptar()
    {
        if(luz.color == Color.green && luz2.color == Color.green && luz3.color == Color.green &&
        luz4.color == Color.green && luz5.color == Color.green)
        {
            SceneManager.LoadScene("TaskCompletedScene");
        }
    }


}
