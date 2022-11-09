using UnityEngine;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BearSecondPlayerFactory : AbstractFactory
    {
        private GameObject bearSecondPlayerPrefab;
        private Transform parent;

        public BearSecondPlayerFactory(GameObject _bearSecondPlayer, Transform _parent)
        {
            bearSecondPlayerPrefab = _bearSecondPlayer;
            parent = _parent;
        }

        public override GameObject CreateObject()
        {
            GameObject bear = GameObject.Instantiate(bearSecondPlayerPrefab, parent);
            return bear;
        }
    }
}
