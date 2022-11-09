using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletInitializer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject spawnPoint;

        private ShotBlocker shotBlocker;

        private Transform bearsParentFirstPlayer;
        private Transform bearsParentSecondPlayer;

        private int bulletCreatorID;

        private int firstPlayerID;
        private int secondPlayerID;

        public static Action<Transform, Transform, int> OnCreateBulletFirstPlayer;
        public static Action<Transform, Transform, int> OnCreateBulletSecondPlayer;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (shotBlocker.isCanShotFirstPlayer && bulletCreatorID == firstPlayerID)
            {
                OnCreateBulletFirstPlayer?.Invoke(spawnPoint.transform, bearsParentSecondPlayer, bulletCreatorID);
            }

            if (shotBlocker.isCanShotSecondPlayer && bulletCreatorID == secondPlayerID)
            {
                OnCreateBulletSecondPlayer?.Invoke(spawnPoint.transform, bearsParentFirstPlayer, bulletCreatorID);
            }
        }

        public void InitializeDatas(int _bulletCreatorID, int _firstPlayerID, int _secondPlayerID, Transform _bearsParentFirstPlayer, Transform _bearsParentSecondPlayer, ShotBlocker _shotBlocker)
        {
            bulletCreatorID = _bulletCreatorID;
            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
            bearsParentFirstPlayer = _bearsParentFirstPlayer;
            bearsParentSecondPlayer = _bearsParentSecondPlayer;
            shotBlocker = _shotBlocker;
        }
    }
}

