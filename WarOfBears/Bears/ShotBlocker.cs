using UnityEngine;

namespace MultiGames.WarOfMarmaladeBears
{
    public class ShotBlocker : MonoBehaviour
    {
        private int firstPlayerID;
        private int secondPlayerID;
         
        public bool isCanShotFirstPlayer { get; private set; } = true;
        public bool isCanShotSecondPlayer { get; private set; } = true;

        private void OnEnable()
        {
            BearsCreator.OnAllBearsCreated += GetPlayersID;
            BulletsCreator.OnContinueShootingFirstPlayer += ShotStatuses;
            BulletsCreator.OnContinueShootingSecondPlayer += ShotStatuses;
            BulletCollisions.OnBulletWasDestroyed += ShotStatuses;
            Bear.OnBearDead += ShotStatuses;
            EndGame.OnStoppingShots += StoppingShots;
        }

        private void OnDisable()
        {
            BearsCreator.OnAllBearsCreated -= GetPlayersID;
            BulletsCreator.OnContinueShootingFirstPlayer -= ShotStatuses;
            BulletsCreator.OnContinueShootingSecondPlayer -= ShotStatuses;
            BulletCollisions.OnBulletWasDestroyed -= ShotStatuses;
            Bear.OnBearDead -= ShotStatuses;
            EndGame.OnStoppingShots -= StoppingShots;
        }

        private void ShotStatuses(int _id, bool _enable)
        {
            if (firstPlayerID == _id)
            {
                isCanShotFirstPlayer = _enable;
            }
            else if (secondPlayerID == _id)
            {
                isCanShotSecondPlayer = _enable;
            }
        }

        private void StoppingShots()
        {
            isCanShotFirstPlayer = false;
            isCanShotSecondPlayer = false;
        }

        private void GetPlayersID(int _firstPlayerID, int _secondPlayerID)
        {
            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
        }
    }
}
