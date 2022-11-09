using UnityEngine;

namespace MultiGames.WarOfMarmaladeBears
{
    public class BearFirstPlayerFactory : AbstractFactory
    {
        private GameObject bearFirstPlayerPrefab;
        private Transform parent;

        public BearFirstPlayerFactory(GameObject _bearFirstPlayer, Transform _parent)
        {
            bearFirstPlayerPrefab = _bearFirstPlayer;
            parent = _parent;
        }

        public override GameObject CreateObject()
        {
            GameObject bear = GameObject.Instantiate(bearFirstPlayerPrefab, parent);
            return bear;
        }
    }
}
