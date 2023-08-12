using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] int score;
    public int Score {
        get => score;  
        set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    public System.Action<int> OnScoreChanged;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        OnScoreChanged += newScore => Debug.Log($"newScore is {newScore}");
    }
}
