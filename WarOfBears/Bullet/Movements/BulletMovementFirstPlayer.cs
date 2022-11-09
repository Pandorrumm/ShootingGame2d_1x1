using UnityEngine;
using DG.Tweening;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BulletMovementFirstPlayer : Bullet
    {
        private float durationOfMovement;
        private Transform bulletTransform;
        private Bear[] targets;

        public BulletMovementFirstPlayer(float _durationOfMovement, Bear[] _targets, Transform _bulletTransform)
        {
            durationOfMovement = _durationOfMovement;
            targets = _targets;
            bulletTransform = _bulletTransform;
        }

        public override void Move()
        {
            Bear _closestSecondPlayer = null;

            float distanceToClosestTarget = Mathf.Infinity;

            foreach (Bear target in targets)
            {
                float distanceToTarget = (target.transform.position - bulletTransform.position).sqrMagnitude;

                if (distanceToTarget < distanceToClosestTarget)
                {
                    distanceToClosestTarget = distanceToTarget;
                    _closestSecondPlayer = target;
                }
            }

            if (_closestSecondPlayer.transform != null)
            {
                bulletTransform.DOMove(_closestSecondPlayer.transform.position, durationOfMovement);
            }
        }
    }
}
