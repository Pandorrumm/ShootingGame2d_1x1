using UnityEngine;
using System;
using UnityEngine.Pool;
using DG.Tweening;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletCollisions : MonoBehaviour
    {
        private int bulletID;

        private int firstPlayerID;
        private int secondPlayerID;

        private Bear bear;

        private ObjectPool<BulletMovement> poolFirstPlayer;
        private ObjectPool<BulletMovement> poolSecondPlayer;

        public static Action<int, bool> OnBulletWasDestroyed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Bear>())
            {
                bear = collision.gameObject.GetComponent<Bear>();

                if (bear.bearID != bulletID)
                {
                    OnBulletHitPlayer(bear, bulletID);
                }
            }
        }

        private void OnBulletHitPlayer(Bear _bear, int _id)
        {
            OnBulletWasDestroyed?.Invoke(_id, true);
            _bear.DisableObject();

            if (bulletID == firstPlayerID)
            {
                poolFirstPlayer.Release(GetComponent<BulletMovement>());
            }
            else if (bulletID == secondPlayerID)
            {
                poolSecondPlayer.Release(GetComponent<BulletMovement>());
            }
           
            DOTween.Kill(GetComponent<BulletMovement>().transform);
        }

        public void InitializeDatas(int _bulletID, int _firstPlayerID, int _secondPlayerID)
        {
            bulletID = _bulletID;
            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
        }

        public void InitializPool(ObjectPool<BulletMovement> _pool)
        {
            if (bulletID == firstPlayerID)
            {
                poolFirstPlayer = _pool;
            }
            else if (bulletID == secondPlayerID)
            {
                poolSecondPlayer = _pool;
            }
        }
    }
}
