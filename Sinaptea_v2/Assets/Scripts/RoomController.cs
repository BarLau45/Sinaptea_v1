using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public Button room;
    public GameObject blackout;
    public Button sala;
    public Button emociones;
    public Button dormitorio;
    public Button armario;
    public Button tienda;
    public Button comedor;
    void Start()
    {
        room = GameObject.Find("Canvas/BotonHabitacion").GetComponent<Button>();
        room.onClick.AddListener(DeployModal);
    }

    void DeployModal()
    {
        blackout.SetActive(!blackout.activeSelf);
        blackout = GameObject.Find("Canvas/BotonHabitacion/Blackout");
        sala = GameObject.Find("Canvas/BotonHabitacion/Blackout/ModalHabitaciones/BotonIrSala").GetComponent<Button>();
        sala.onClick.AddListener(() => Navigate("SampleScene"));
        emociones = GameObject.Find("Canvas/BotonHabitacion/Blackout/ModalHabitaciones/BotonIrEmociones").GetComponent<Button>();
        emociones.onClick.AddListener(() => Navigate("EmocionesScene"));
        dormitorio = GameObject.Find("Canvas/BotonHabitacion/Blackout/ModalHabitaciones/BotonIrDormitorio").GetComponent<Button>();
        dormitorio.onClick.AddListener(() => Navigate("DormitorioScene"));
        armario = GameObject.Find("Canvas/BotonHabitacion/Blackout/ModalHabitaciones/BotonIrArmario").GetComponent<Button>();
        armario.onClick.AddListener(() => Navigate("ArmarioScene"));
        // To Do armario, tienda, comedor
    }

    void Navigate(string roomName)
    {
        SceneManager.LoadScene(roomName);
    }
}
