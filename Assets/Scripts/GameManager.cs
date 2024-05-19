using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    private int score;
    public int Score { get => score; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        ShowScore();
    }

    /// <summary>
    /// Skoru ekranda gösterir
    /// </summary>
    private void ShowScore()
    {
        scoreText.text = $"Score = {Score}";
    }

    /// <summary>
    /// Puan Ekler
    /// </summary>
    public void AddScore(int amount)
    {
        score += amount;
    }

    private void OnSwallow_GM(GameObject swobject)
    {
        int objectPoint = Calculator.instance.CalculateObjectPoint(swobject);
        AddScore(objectPoint);
    }

    private void OnEnable()
    {
        Destroyer.OnSwallow += OnSwallow_GM;
    }
    private void OnDisable()
    {
        Destroyer.OnSwallow -= OnSwallow_GM;
    }

}
