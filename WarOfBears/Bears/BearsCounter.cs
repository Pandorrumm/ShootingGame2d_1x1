using UnityEngine;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BearsCounter : MonoBehaviour
    {
        private int firstPlayerID;
        private int secondPlayerID;

        private int bearsOfFirstPlayer;
        private int bearsOfSecondPlayer;

        public static Action OnDestroyedAllBearsFirstPlayer;
        public static Action OnDestroyedAllBearsSecondPlayer;

        private void OnEnable()
        {
            BearsCreator.OnAllBearsCreated += GetPlayersID;
            BearsCreator.OnCreatedBears += GetNumberOfBears;
            Bear.OnBearDestroyed += CountNumberBears;
        }

        private void OnDisable()
        {
            BearsCreator.OnAllBearsCreated -= GetPlayersID;
            BearsCreator.OnCreatedBears -= GetNumberOfBears;
            Bear.OnBearDestroyed -= CountNumberBears;
        }

        private void GetNumberOfBears(int _value)
        {
            bearsOfFirstPlayer = _value;
            bearsOfSecondPlayer = _value;
        }

        private void CountNumberBears(int _id)
        {
            if (firstPlayerID == _id)
            {
                bearsOfFirstPlayer--;

                if (bearsOfFirstPlayer == 0)
                {
                    OnDestroyedAllBearsFirstPlayer?.Invoke();
                }
            }
            else if (secondPlayerID == _id)
            {
                bearsOfSecondPlayer--;

                if (bearsOfSecondPlayer == 0)
                {
                    OnDestroyedAllBearsSecondPlayer?.Invoke();
                }
            }          
        }

        private void GetPlayersID(int _firstPlayerID, int _secondPlayerID)
        {
            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
        }
    }
}
