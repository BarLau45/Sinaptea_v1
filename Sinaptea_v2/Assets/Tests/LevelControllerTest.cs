using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Events;
using TMPro;
using System.Reflection;
using UnityEngine.SceneManagement;

[TestFixture]
public class LevelControllerTests
{
    private GameObject gameObject;
    private LevelController levelController;
    private CardController mockCardPrefab;
    private TMP_Text mockLevelText;
    private TMP_Text mockMovementsText;
    private GameObject mockGameOverButton;

    [SetUp]
    public void SetUp()
    {
       
        gameObject = new GameObject("LevelController");
        levelController = gameObject.AddComponent<LevelController>();
        
       
        var cardPrefabObj = new GameObject("CardPrefab");
        mockCardPrefab = cardPrefabObj.AddComponent<CardController>();
        var animator = cardPrefabObj.AddComponent<Animator>();
        
       
        mockCardPrefab.CardSize = 2f;
        
        var prefabs = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            prefabs.Add(new GameObject($"CardType_{i}"));
        }
        SetPrivateField<List<GameObject>>(mockCardPrefab, "prefabs", prefabs);
        mockCardPrefab.OnClicked = new UnityEvent<CardController>();
        
      
        mockLevelText = new GameObject("LevelText").AddComponent<TextMeshProUGUI>();
        mockMovementsText = new GameObject("MovementsText").AddComponent<TextMeshProUGUI>();
        mockGameOverButton = new GameObject("GameOverButton");
        mockGameOverButton.SetActive(true);
        
       
        SetPrivateField<CardController>(levelController, "_cardPrefab", mockCardPrefab);
        SetPrivateField<TMP_Text>(levelController, "_levelText", mockLevelText);
        SetPrivateField<TMP_Text>(levelController, "_movementsText", mockMovementsText);
        SetPrivateField<GameObject>(levelController, "_gameOverButton", mockGameOverButton);
       
        var testLevels = new List<LevelController.LevelData>
        {
            new LevelController.LevelData { Columns = 2, Rows = 2, Difficulty = 2, Movements = 5 },
            new LevelController.LevelData { Columns = 4, Rows = 2, Difficulty = 4, Movements = 10 }
        };
        SetPrivateField<List<LevelController.LevelData>>(levelController, "_levels", testLevels);
        SetPrivateField<int>(levelController, "_level", 0);
    }

    [TearDown]
    public void TearDown()
    {
        
        var prefabs = GetPrivateField<List<GameObject>>(mockCardPrefab, "prefabs");
        if (prefabs != null)
        {
            foreach (var prefab in prefabs)
            {
                if (prefab != null)
                    Object.DestroyImmediate(prefab);
            }
        }
        
        Object.DestroyImmediate(mockCardPrefab.gameObject);
        Object.DestroyImmediate(mockLevelText.gameObject);
        Object.DestroyImmediate(mockMovementsText.gameObject);
        Object.DestroyImmediate(mockGameOverButton);
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void CheckForLevelWrap_WhenLevelExceedsCount()
    {
    
        int levelCount = GetPrivateField<List<LevelController.LevelData>>(levelController, "_levels").Count;
        SetPrivateField<int>(levelController, "_level", levelCount); 
        
       
        InvokePrivateMethod(levelController, "Win");
        
        
        int level = GetPrivateField<int>(levelController, "_level");
        Assert.AreEqual(0, level); 
    }

    [Test]
    public void StartLevel_GameOverButtonIsHidden()
    {
        
        mockGameOverButton.SetActive(true);
        
       
        levelController.StartLevel();
        
        Assert.IsFalse(mockGameOverButton.activeSelf, "El botón de Game Over debería estar oculto al iniciar un nivel");
    }

    [Test]
    public void StartLevel_SetsCorrectUIText()
    {
        levelController.StartLevel();
        
        Assert.AreEqual("Level: 0", mockLevelText.text, "El texto del nivel debe mostrar el nivel actual");
        Assert.AreEqual("Moves: 5", mockMovementsText.text, "El texto de movimientos debe mostrar los movimientos disponibles");
    }

    [Test]
    public void StartLevel_ResetsMovementsUsed()
    {
        SetPrivateField<int>(levelController, "_movementsUsed", 10);
        
      
        levelController.StartLevel();
        
       
        int movementsUsed = GetPrivateField<int>(levelController, "_movementsUsed");
        Assert.AreEqual(0, movementsUsed, "Los movimientos usados deben reiniciarse a 0");
    }

    [Test]
    public void StartLevel_UnblocksInput()
    {
        
        SetPrivateField<bool>(levelController, "_blockInput", true);
        
     
        levelController.StartLevel();
        
        
        bool blockInput = GetPrivateField<bool>(levelController, "_blockInput");
        Assert.IsFalse(blockInput, "La entrada debe desbloquearse al iniciar un nivel");
    }


    
    [UnityTest]
    public IEnumerator Lose_ShowsGameOverButton()
    {
        
        mockGameOverButton.SetActive(false);
        
       
        InvokePrivateMethod(levelController, "Lose");
        
       
        yield return null;
        
       
        Assert.IsTrue(mockGameOverButton.activeSelf, "El botón de Game Over debe mostrarse al perder");
    }

    [Test]
    public void OnCardClicked_BlockedInput_DoesNothing()
    {
        
        var card = CreateMockCard();
        SetPrivateField<bool>(levelController, "_blockInput", true);
        SetPrivateField<CardController>(levelController, "_activeCard", null);
        
       
        InvokePrivateMethod(levelController, "OnCardClicked", card);
        
        
        var activeCard = GetPrivateField<CardController>(levelController, "_activeCard");
        Assert.IsNull(activeCard, "No debe establecerse una carta activa cuando la entrada está bloqueada");
        
       
        Object.DestroyImmediate(card.gameObject);
    }



   
    private CardController CreateMockCard()
    {
        var cardObj = new GameObject("MockCard");
        var card = cardObj.AddComponent<CardController>();
        var animator = cardObj.AddComponent<Animator>();
        card.OnClicked = new UnityEvent<CardController>();
        return card;
    }

    private void SetPrivateField<T>(object instance, string fieldName, T value)
    {
        var field = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            field.SetValue(instance, value);
        }
        else
        {
            Debug.LogError($"No se encontró el campo privado '{fieldName}' en {instance.GetType().Name}");
        }
    }

    private T GetPrivateField<T>(object instance, string fieldName)
    {
        var field = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            return (T)field.GetValue(instance);
        }
        else
        {
            Debug.LogError($"No se encontró el campo privado '{fieldName}' en {instance.GetType().Name}");
            return default;
        }
    }

    private object InvokePrivateMethod(object instance, string methodName, params object[] parameters)
    {
        var method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(instance, parameters);
        }
        else
        {
            Debug.LogError($"No se encontró el método privado '{methodName}' en {instance.GetType().Name}");
            return null;
        }
    }
}