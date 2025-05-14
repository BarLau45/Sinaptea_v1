using System.Collections.Generic;
using UnityEngine;

public class WardrobeController : MonoBehaviour
{
    [Header("Prenda a cambiar")]
    public SpriteRenderer wear;
    [Header("Prendas en ciclo")]
    public List<Sprite> options = new List<Sprite>();

    private int currentOption = 0;

    public void NextOption() {
        currentOption++;
        if (currentOption >= options.Count) {
            currentOption = 0;
        }
        wear.sprite = options[currentOption];
    }

    public void PreviousOption() {
        currentOption--;
        if (currentOption < 0) {
            currentOption = options.Count - 1;
        }
        wear.sprite = options[currentOption];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
