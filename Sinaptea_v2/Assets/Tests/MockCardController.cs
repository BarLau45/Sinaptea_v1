using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Tests
{
   
    public class CardControllerTestDouble : MonoBehaviour
    {
        public float CardSize { get; set; } = 1.0f;
        public int MaxCardTypes { get; set; } = 5;
        public int CardType { get; set; }
        
        public UnityEvent<CardController> OnClicked = new UnityEvent<CardController>();
        private CardController _realCardController;
        private bool _isRevealed = false;

        public void Initialize(CardController realCard)
        {
            _realCardController = realCard;
        }

        public void Reveal()
        {
            _isRevealed = true;
           
        }

        public void Hide()
        {
            _isRevealed = false;
            
        }

        public bool IsRevealed()
        {
            return _isRevealed;
        }

       
        public void SimulateClick()
        {
            if (_realCardController != null)
            {
                OnClicked.Invoke(_realCardController);
            }
        }
    }
}