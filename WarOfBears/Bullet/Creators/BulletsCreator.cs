using UnityEngine;
using UnityEngine.Pool;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletsCreator : MonoBehaviour
    {
        [SerializeField] private BulletMovement bulletFirstPlayerPrefab;
        [SerializeField] private BulletMovement bulletSecondPlayerPrefab;

        private static ObjectPool<BulletMovement> poolFirstPlayer;
        private static ObjectPool<BulletMovement> poolSecondPlayer;

        private int firstPlayerID;
        private int secondPlayerID;

        private Transform spawnTransform;

        public static Action<BulletMovement> OnBulletCreated;
        public static Action<int, bool> OnContinueShootingFirstPlayer;
        public static Action<int, bool> OnContinueShootingSecondPlayer;

        private void OnEnable()
        {
            BearsCreator.OnAllBearsCreated += CreatePoolsOfBullets;
            BulletInitializer.OnCreateBulletFirstPlayer += CreateBulletFirstPlayer;
            BulletInitializer.OnCreateBulletSecondPlayer += CreateBulletSecondPlayer;
        }

        private void OnDisable()
        {
            BearsCreator.OnAllBearsCreated -= CreatePoolsOfBullets;
            BulletInitializer.OnCreateBulletFirstPlayer -= CreateBulletFirstPlayer;
            BulletInitializer.OnCreateBulletSecondPlayer -= CreateBulletSecondPlayer;
        }

        private void CreatePoolsOfBullets(int _firstPlayerID, int _secondPlayerID)
        {
            CreatePoolFirstPlayer();
            CreatePoolSecondPlayer();

            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
        }

        private void CreateBulletFirstPlayer(Transform _spawnPoint, Transform _bearsParentSecondPlayer, int _bulletCreatorID)
        {
            var bullet = poolFirstPlayer.Get();

            bullet.transform.SetParent(_spawnPoint.transform);
            bullet.transform.position = _spawnPoint.transform.position;

            TransmitData(bullet, poolFirstPlayer, _bearsParentSecondPlayer, _bulletCreatorID, firstPlayerID, secondPlayerID);

            OnBulletCreated?.Invoke(bullet);
            OnContinueShootingFirstPlayer?.Invoke(_bulletCreatorID, false);

            bullet.Move();
        }

        private void CreateBulletSecondPlayer(Transform _spawnPoint, Transform _bearsParentFirstPlayer, int _bulletCreatorID)
        {
            var bullet = poolSecondPlayer.Get();

            bullet.transform.SetParent(_spawnPoint.transform);
            bullet.transform.position = _spawnPoint.transform.position;

            TransmitData(bullet, poolSecondPlayer, _bearsParentFirstPlayer, _bulletCreatorID, firstPlayerID, secondPlayerID);

            OnBulletCreated?.Invoke(bullet);
            OnContinueShootingSecondPlayer?.Invoke(_bulletCreatorID, false);

            bullet.Move();
        }

        private void TransmitData(BulletMovement _bullet, ObjectPool<BulletMovement> _pool, Transform _bearsTargetParent, int _bulletCreatorID, int _firstPlayerID, int _secondPlayerID)
        {
            _bullet.InitializeDatas(_bulletCreatorID, _firstPlayerID, _secondPlayerID, _bearsTargetParent.GetComponentsInChildren<Bear>());

            if (_bullet.TryGetComponent<BulletCollisions>(out var bulletCollisions))
            {
                bulletCollisions.InitializeDatas(_bulletCreatorID, _firstPlayerID, _secondPlayerID);
                bulletCollisions.InitializPool(_pool);
            }

            if (_bullet.TryGetComponent<FlyingBullet>(out var flyingBullet))
            {
                flyingBullet.InitializeDatas(_bulletCreatorID, _firstPlayerID, _secondPlayerID, true);
                flyingBullet.InitializPool(_pool);
            }
        }

        public void CreatePoolFirstPlayer()
        {
            if (poolFirstPlayer != null)
                return;
            spawnTransform = gameObject.transform;
            poolFirstPlayer = new ObjectPool<BulletMovement>(PoolBulletFirstPlayerCreationFunc, ActionOnGet, ActionOnRelease, null, true, 10, 5);
        }

        public void CreatePoolSecondPlayer()
        {
            if (poolSecondPlayer != null)
                return;
            spawnTransform = gameObject.transform;
            poolSecondPlayer = new ObjectPool<BulletMovement>(PoolBulletSecondPlayerCreationFunc, ActionOnGet, ActionOnRelease, null, true, 10, 5);
        }

        private BulletMovement PoolBulletFirstPlayerCreationFunc()
        {
            var result = Instantiate(bulletFirstPlayerPrefab, spawnTransform);
            result.InitializePoolBulletFirstPlayer(poolFirstPlayer);
            result.gameObject.SetActive(false);
            return result;
        }

        private BulletMovement PoolBulletSecondPlayerCreationFunc()
        {
            var result = Instantiate(bulletSecondPlayerPrefab, spawnTransform);
            result.InitializePoolBulletSecondPlayer(poolSecondPlayer);
            result.gameObject.SetActive(false);
            return result;
        }

        private void ActionOnGet(BulletMovement bullet) => bullet.gameObject.SetActive(true);

        private void ActionOnRelease(BulletMovement bullet) => bullet.gameObject.SetActive(false);

        private void OnDestroy()
        {
            poolFirstPlayer.Clear();
            poolFirstPlayer = null;

            poolSecondPlayer.Clear();
            poolSecondPlayer = null;
        }
    }
}
