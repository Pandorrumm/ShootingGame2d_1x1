using UnityEngine;
using UnityEngine.Pool;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private float durationOfMovement = 1f;

        private int firstPlayerID;
        private int secondPlayerID;

        private int bulletID;
        private Bullet bullet;
        private Bear[] bears;

        private ObjectPool<BulletMovement> poolFirstPlayer;
        private ObjectPool<BulletMovement> poolSecondPlayer;

        public void Move()
        {
            if (bulletID == firstPlayerID)
            {
                bullet = new BulletMovementFirstPlayer(durationOfMovement, bears, gameObject.transform);
                bullet.Move();
            }
            else if (bulletID == secondPlayerID)
            {
                bullet = new BulletMovementSecondPlayer(durationOfMovement, bears, gameObject.transform);
                bullet.Move();
            }
        }

        public void InitializeDatas(int _bulletID, int _firstPlayerID, int _secondPlayerID, Bear[] _bears)
        {
            bulletID = _bulletID;
            firstPlayerID = _firstPlayerID;
            secondPlayerID = _secondPlayerID;
            bears = _bears;
        }

        public void InitializePoolBulletFirstPlayer(ObjectPool<BulletMovement> _pool) => poolFirstPlayer ??= _pool;

        public void InitializePoolBulletSecondPlayer(ObjectPool<BulletMovement> _pool) => poolSecondPlayer ??= _pool;
        
    }
}
