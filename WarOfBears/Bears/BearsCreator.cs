using UnityEngine;
using System.Collections.Generic;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BearsCreator : MonoBehaviour
    {
        [SerializeField] private GameObject bearFirstPlayerPrefab;
        [SerializeField] private GameObject bearSecondPlayerPrefab;

        [Space]
        [SerializeField] private int firstPlayerID;
        [SerializeField] private int secondPlayerID;

        [Space]
        [SerializeField] private BearPlacementSettings bearPlacementSettings;

        [Space]
        [SerializeField] private ShotBlocker shotBlocker;

        [Space]
        [SerializeField] private Transform containerWithBearsFirstPlayer;
        [SerializeField] private Transform containerWithBearsSecondPlayer;

        [Space]
        [SerializeField] private Transform startPositionFirstPlayer;
        [SerializeField] private Transform startPositionSecondPlayer;

        [Space]
        [SerializeField] private int numberBearsToRemove = 5;

        private AbstractFactory abstractFactoryFirstPlayer;
        private AbstractFactory abstractFactorySecondPlayer;

        private List<int> indexPlayer1 = new List<int>();
        private List<int> indexPlayer2 = new List<int>();

        public static Action<int> OnCreatedBears;
        public static Action<int, int> OnAllBearsCreated;

        private void Start()
        {
            abstractFactoryFirstPlayer = new BearFirstPlayerFactory(bearFirstPlayerPrefab, containerWithBearsFirstPlayer);
            abstractFactorySecondPlayer = new BearSecondPlayerFactory(bearSecondPlayerPrefab, containerWithBearsSecondPlayer);

            CreatingBears();            
        }

        private void CreatingBears()
        {
            for (int i = 0; i < bearPlacementSettings.NumberOfRows; i++)
            {
                for (int y = 0; y < bearPlacementSettings.ObjectsPerRow; y++)
                {
                    Vector3 _startingPosition1 = new Vector3(startPositionFirstPlayer.position.x + y * bearPlacementSettings.Spacing,
                                                             startPositionFirstPlayer.position.y - i * bearPlacementSettings.Spacing,
                                                             startPositionFirstPlayer.position.z);

                    Vector3 _startingPosition2 = new Vector3(startPositionSecondPlayer.position.x + y * bearPlacementSettings.Spacing,
                                                             startPositionSecondPlayer.position.y - i * bearPlacementSettings.Spacing,
                                                             startPositionSecondPlayer.position.z);

                    Vector3 pos1 = Camera.main.WorldToScreenPoint(_startingPosition1);
                    Vector3 pos2 = Camera.main.WorldToScreenPoint(_startingPosition2);

                    if (pos1.x > bearPlacementSettings.IndentFromEdgeOfScreen)
                    {
                        CreateFirstPlayerBear(_startingPosition1);
                    }

                    if (pos2.x < Screen.width - bearPlacementSettings.IndentFromEdgeOfScreen)
                    {
                        CreateSecondPlayerBear(_startingPosition2);
                    }
                }

                if (i == bearPlacementSettings.NumberOfRows - 1)
                {
                    DisablingNonBattleBears();
                }
            }

            OnAllBearsCreated?.Invoke(firstPlayerID, secondPlayerID);
        }

        private void CreateFirstPlayerBear(Vector3 _position)
        {
            var obj = abstractFactoryFirstPlayer.CreateObject();
            obj.transform.position = _position;

            TransmitData(obj, firstPlayerID);
        }

        private void CreateSecondPlayerBear(Vector3 _position)
        {
            var obj = abstractFactorySecondPlayer.CreateObject();
            obj.transform.position = _position;

            TransmitData(obj, secondPlayerID);
        }

        private void TransmitData(GameObject _obj, int _playerID)
        {
            if (_obj.TryGetComponent<BulletInitializer>(out var bulletInitializer))
            {
                bulletInitializer.InitializeDatas(_playerID, firstPlayerID, secondPlayerID, containerWithBearsFirstPlayer, containerWithBearsSecondPlayer, shotBlocker);
            }

            if (_obj.TryGetComponent<Bear>(out var bear))
            {
                bear.InitializeBearID(_playerID);
            }
        }

        private void DisablingNonBattleBears()
        {
            int numberBearsCreated = containerWithBearsFirstPlayer.childCount;

            OnCreatedBears?.Invoke(containerWithBearsFirstPlayer.childCount - numberBearsToRemove); 

            for (int i = 0; i < numberBearsToRemove; i++)
            {
                int indexFirstPlayer = UnityEngine.Random.Range(0, numberBearsCreated);
                int indexSecondPlayer = UnityEngine.Random.Range(0, numberBearsCreated);

                if (!indexPlayer1.Contains(indexFirstPlayer) && !indexPlayer2.Contains(indexSecondPlayer))
                {
                    indexPlayer1.Add(indexFirstPlayer);
                    indexPlayer2.Add(indexSecondPlayer);

                    containerWithBearsFirstPlayer.GetChild(indexFirstPlayer).gameObject.SetActive(false);
                    containerWithBearsSecondPlayer.GetChild(indexSecondPlayer).gameObject.SetActive(false);
                }
                else
                {
                    i--;
                }
            }
        }
    }
}
