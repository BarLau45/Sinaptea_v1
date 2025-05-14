using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SelectWordTest
{
    
    private class MockSelectWord
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
        
        public string palabraCorrecta = "manzana";
        public string palabraCorrecta2 = "flor";
        public string palabraCorrecta3 = "miel";
        public string palabraCorrecta4 = "pera";
        public string palabraCorrecta5 = "sol";
        
        public bool escenaCambiada = false;
        
        
        public void Check()
        {
            
            string respuestaUsuario = inputText.text.Trim().ToLower();
            if (respuestaUsuario == palabraCorrecta.ToLower())
            {
                luz.color = Color.green;
            }
            else
            {
                luz.color = Color.red;
            }

            
            string respuestaUsuario2 = inputText2.text.Trim().ToLower();
            if (respuestaUsuario2 == palabraCorrecta2.ToLower())
            {
                luz2.color = Color.green;
            }
            else
            {
                luz2.color = Color.red;
            }

            
            string respuestaUsuario3 = inputText3.text.Trim().ToLower();
            if (respuestaUsuario3 == palabraCorrecta3.ToLower())
            {
                luz3.color = Color.green;
            }
            else
            {
                luz3.color = Color.red;
            }

            
            string respuestaUsuario4 = inputText4.text.Trim().ToLower();
            if (respuestaUsuario4 == palabraCorrecta4.ToLower())
            {
                luz4.color = Color.green;
            }
            else
            {
                luz4.color = Color.red;
            }

            
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
        
        
        public void Aceptar()
        {
            if(luz.color == Color.green && luz2.color == Color.green && luz3.color == Color.green &&
               luz4.color == Color.green && luz5.color == Color.green)
            {
                
                escenaCambiada = true;
            }
        }
    }

    private MockSelectWord mockSelectWord;
    private TMP_InputField inputText1;
    private TMP_InputField inputText2;
    private TMP_InputField inputText3;
    private TMP_InputField inputText4;
    private TMP_InputField inputText5;
    private Image luz1;
    private Image luz2;
    private Image luz3;
    private Image luz4;
    private Image luz5;

    [SetUp]
    public void SetUp()
    {
        
        mockSelectWord = new MockSelectWord();
        
        
        inputText1 = new GameObject("InputField1").AddComponent<TMP_InputField>();
        inputText2 = new GameObject("InputField2").AddComponent<TMP_InputField>();
        inputText3 = new GameObject("InputField3").AddComponent<TMP_InputField>();
        inputText4 = new GameObject("InputField4").AddComponent<TMP_InputField>();
        inputText5 = new GameObject("InputField5").AddComponent<TMP_InputField>();
        
        luz1 = new GameObject("Luz1").AddComponent<Image>();
        luz1.color = Color.red;
        luz2 = new GameObject("Luz2").AddComponent<Image>();
        luz2.color = Color.red;
        luz3 = new GameObject("Luz3").AddComponent<Image>();
        luz3.color = Color.red;
        luz4 = new GameObject("Luz4").AddComponent<Image>();
        luz4.color = Color.red;
        luz5 = new GameObject("Luz5").AddComponent<Image>();
        luz5.color = Color.red;
        
        
        mockSelectWord.inputText = inputText1;
        mockSelectWord.inputText2 = inputText2;
        mockSelectWord.inputText3 = inputText3;
        mockSelectWord.inputText4 = inputText4;
        mockSelectWord.inputText5 = inputText5;
        
        mockSelectWord.luz = luz1;
        mockSelectWord.luz2 = luz2;
        mockSelectWord.luz3 = luz3;
        mockSelectWord.luz4 = luz4;
        mockSelectWord.luz5 = luz5;
    }

    [TearDown]
    public void TearDown()
    {
       
        Object.Destroy(inputText1.gameObject);
        Object.Destroy(inputText2.gameObject);
        Object.Destroy(inputText3.gameObject);
        Object.Destroy(inputText4.gameObject);
        Object.Destroy(inputText5.gameObject);
        Object.Destroy(luz1.gameObject);
        Object.Destroy(luz2.gameObject);
        Object.Destroy(luz3.gameObject);
        Object.Destroy(luz4.gameObject);
        Object.Destroy(luz5.gameObject);
    }

    [Test]
    public void AceptarBoton_CuandoTodasLucesVerdes_DebeCambiarEscena()
    {
        
        luz1.color = Color.green;
        luz2.color = Color.green;
        luz3.color = Color.green;
        luz4.color = Color.green;
        luz5.color = Color.green;

      
        mockSelectWord.Aceptar();
        
        
        Assert.IsTrue(mockSelectWord.escenaCambiada, 
            "La escena debería cambiar cuando todas las luces son verdes");
    }

    [Test]
    public void AceptarBoton_CuandoAlgunaLuzRoja_NoDebeCambiarEscena()
    {
        
        luz1.color = Color.green;
        luz2.color = Color.red; 
        luz3.color = Color.green;
        luz4.color = Color.green;
        luz5.color = Color.green;
        
        
        mockSelectWord.Aceptar();
        
        
        Assert.IsFalse(mockSelectWord.escenaCambiada, 
            "La escena no debería cambiar cuando hay al menos una luz roja");
    }

    [Test]
    public void ComprobarFuncionamientoIndividualBotones()
    {
      
        inputText1.text = "manzana"; // Correcta
        inputText2.text = "flor";    // Correcta
        inputText3.text = "azucar";  // Incorrecta
        inputText4.text = "pera";    // Correcta
        inputText5.text = "luna";    // Incorrecta
        
        
        mockSelectWord.Check();
        
      
        Assert.AreEqual(Color.green, luz1.color, "La luz 1 debe estar verde para 'manzana'");
        Assert.AreEqual(Color.green, luz2.color, "La luz 2 debe estar verde para 'flor'");
        Assert.AreEqual(Color.red, luz3.color, "La luz 3 debe estar roja para 'azucar'");
        Assert.AreEqual(Color.green, luz4.color, "La luz 4 debe estar verde para 'pera'");
        Assert.AreEqual(Color.red, luz5.color, "La luz 5 debe estar roja para 'luna'");
    }
}