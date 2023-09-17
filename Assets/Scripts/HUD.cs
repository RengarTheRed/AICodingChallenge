using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    public void UpdateScore(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
