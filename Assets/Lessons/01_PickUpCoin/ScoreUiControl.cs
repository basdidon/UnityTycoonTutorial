using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUiControl : MonoBehaviour
{
    [field: SerializeField] UIDocument ScoreUiDoc { get; set; }
    VisualElement root;

    Label coinTxt;

    private void Awake()
    {
        root = ScoreUiDoc.rootVisualElement;
        coinTxt = root.Q<Label>("Coin");

        if(coinTxt != null)
        {
            LevelManager.Instance.OnScoreChanged += newScore => coinTxt.text = $"{newScore}";
            coinTxt.text = $"{LevelManager.Instance.Score}";
        }
        else
        {
            Debug.Log("not founded coinText");
        }

    }
}
