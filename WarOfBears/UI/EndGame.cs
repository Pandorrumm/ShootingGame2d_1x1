using UnityEngine;
using TMPro;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject victoryScreen;

        [Space]
        [SerializeField] private TMP_Text winningText;
        [SerializeField] private string winningTextFirstPlayer;
        [SerializeField] private string winningTextSecondPlayer;

        public static Action OnStoppingShots;

        private void OnEnable()
        {
            BearsCounter.OnDestroyedAllBearsFirstPlayer += SecondPlayerWon;
            BearsCounter.OnDestroyedAllBearsSecondPlayer += FirstPlayerWon;
        }

        private void OnDisable()
        {
            BearsCounter.OnDestroyedAllBearsFirstPlayer -= SecondPlayerWon;
            BearsCounter.OnDestroyedAllBearsSecondPlayer -= FirstPlayerWon;
        }

        private void FirstPlayerWon()
        {
            OpenVictoryScreen();

            winningText.text = winningTextFirstPlayer;
        }

        private void SecondPlayerWon()
        {
            OpenVictoryScreen();

            winningText.text = winningTextSecondPlayer;
        }

        private void OpenVictoryScreen()
        {
            victoryScreen.SetActive(true);

            OnStoppingShots?.Invoke();
        }
    }
}
