using UnityEngine;

namespace MultiGames.WarOfMarmaladeBears
{
    [CreateAssetMenu(menuName = "MultiGames/WarOfMarmaladeBears/Settings/BearsPlacement")]
    public class BearPlacementSettings : ScriptableObject
    {
        [SerializeField] private int numberOfRows;
        public float NumberOfRows => numberOfRows;

        [SerializeField] private int objectsPerRow;
        public int ObjectsPerRow => objectsPerRow;

        [SerializeField] private float spacing;
        public float Spacing => spacing;

        [SerializeField] private float indentFromEdgeOfScreen;
        public float IndentFromEdgeOfScreen => indentFromEdgeOfScreen;
    }
}
