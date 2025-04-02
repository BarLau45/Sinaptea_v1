using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField]
    private CardController _cardPrefab;

    private List<CardController> _cards = new List<CardController>();

    [SerializeField]
    private int _columns = 4;
    [SerializeField]
    private int _rows = 4;
    [SerializeField]
    private int _difficulty = 4;

    private void Start()
    {
        StartLevel();
    }
    public void StartLevel()
    {
        if(_difficulty > _cardPrefab.MaxCardTypes)
        {
            Debug.Assert(false);
            _difficulty = Math.Min(_difficulty, _cardPrefab.MaxCardTypes);
        }
        Debug.Assert((_rows * _columns) % 2 == 0 );


        _cards.ForEach(c => Destroy(c.gameObject));
        _cards.Clear();

        List<int> allTypes = new List<int>();
        for(int i = 0; i < _cardPrefab.MaxCardTypes; ++i)
        {
            allTypes.Add(i);
        }

        List<int> gameTypes = new List<int>();
        for(int i = 0; i < _difficulty; i++)
        {
            int chosenType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
            allTypes.Remove(chosenType);
            gameTypes.Add(chosenType);
        }

        List<int> chosenTypes = new List<int>();
        for(int i = 0; i < (_rows * _columns) / 2; i++)
        {
            int chosenType = gameTypes[UnityEngine.Random.Range(0, gameTypes.Count)];
            chosenTypes.Add(chosenType);
            chosenTypes.Add(chosenType);
        }


        Vector3 offset = new Vector3((_columns - 1) * _cardPrefab.CardSize, (_rows - 1) * _cardPrefab.CardSize, 0f) * 
        0.5f;
        for(int y = 0; y < _rows; ++y)
        {
            for(int x = 0; x < _columns; ++x)
            {
                Vector3 position = new Vector3(x * _cardPrefab.CardSize, y * _cardPrefab.CardSize, 0f);
                var card = Instantiate(_cardPrefab, position - offset,
                 quaternion.identity);
                card.CardType = chosenTypes[UnityEngine.Random.Range(0, chosenTypes.Count)];
                chosenTypes.Remove(card.CardType);
                card.OnClicked.AddListener(OnCardClicked);
                _cards.Add(card);
            }
        }
    }


    private void OnCardClicked(CardController card)
    {
        card.TestAnimation();
    }


    private void Update()
    {

    }
}
