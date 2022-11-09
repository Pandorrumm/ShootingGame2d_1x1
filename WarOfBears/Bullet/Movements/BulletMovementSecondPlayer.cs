using UnityEngine;
using DG.Tweening;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletMovementSecondPlayer : Bullet
    {
        private float durationOfMovement = 1f;
        private Transform bulletTransform;
        private Bear[] targets;

        public BulletMovementSecondPlayer(float _durationOfMovement, Bear[] _targets, Transform _bulletTransform)
        {
            durationOfMovement = _durationOfMovement;
            targets = _targets;
            bulletTransform = _bulletTransform;
        }

        public override void Move()
        {
            Bear _closestFirstPlayer = null;

            float distanceToClosestTarget = Mathf.Infinity;

            foreach (Bear target in targets)
            {
                float distanceToTarget = (target.transform.position - bulletTransform.position).sqrMagnitude;

                if (distanceToTarget < distanceToClosestTarget)
                {
                    distanceToClosestTarget = distanceToTarget;
                    _closestFirstPlayer = target;
                }
            }

            if (_closestFirstPlayer.transform != null)
            {
                bulletTransform.DOMove(_closestFirstPlayer.transform.position, durationOfMovement);
            }
        }
    }
}
