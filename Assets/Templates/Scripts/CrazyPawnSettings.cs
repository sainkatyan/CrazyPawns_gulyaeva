using UnityEngine;

namespace Templates.Scripts
{
    [CreateAssetMenu(menuName = "CrazyPawn/Settings", fileName = "CrazyPawnSettings")]
    public class CrazyPawnSettings : ScriptableObject
    {
        [SerializeField] public float InitialZoneRadius = 10f;
        [SerializeField] public int InitialPawnCount = 7;

        [SerializeField] public Material DeleteMaterial;
        [SerializeField] public Material ActiveConnectorMaterial;

        [SerializeField] public int CheckerboardSize = 18;
        [SerializeField] public Color BlackCellColor = Color.yellow;
        [SerializeField] public Color WhiteCellColor = Color.green;
    }
}