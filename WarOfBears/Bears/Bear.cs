using UnityEngine;
using System;

namespace MultiGames.WarOfMarmaladeBears
{
    public class Bear : MonoBehaviour
    {
        public int bearID { get; private set; }

        [SerializeField] private Sprite bloodSprite;

        private SpriteRenderer bearSpriteRenderer;
        private BoxCollider2D boxColliderBear;
        private FlyingBullet flyingBullet;

        public static Action<int> OnBearDestroyed;
        public static Action<int, bool> OnBearDead;

        private void OnEnable()
        {
            BulletsCreator.OnBulletCreated += GetBulletComponent;
        }

        private void OnDisable()
        {
            BulletsCreator.OnBulletCreated -= GetBulletComponent;
        }

        private void Awake()
        {
            bearSpriteRenderer = GetComponent<SpriteRenderer>();
            boxColliderBear = GetComponent<BoxCollider2D>();
        }

        private void GetBulletComponent(BulletMovement _bullet)
        {
            if (GetComponentInChildren<BulletMovement>() == _bullet)
            {
                flyingBullet = _bullet.GetComponent<FlyingBullet>();
            }            
        }

        public void DisableObject()
        {
            bearSpriteRenderer.sprite = bloodSprite;
            bearSpriteRenderer.sortingOrder = 0;
            boxColliderBear.enabled = false;

            OnBearDestroyed?.Invoke(bearID);

            if (flyingBullet != null)
            {
                if (flyingBullet.isBearCreatorIsAlive.Value)
                {
                    flyingBullet.isBearCreatorIsAlive.Value = false;
                    OnBearDead?.Invoke(bearID, true);
                }                
            }

            Destroy(this.GetComponent<Bear>());
        }

        public void InitializeBearID(int _id)
        {
            bearID = _id;
        }
    }
}
