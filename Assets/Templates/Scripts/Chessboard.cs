using UnityEngine;

namespace CrazyPawn
{
    public class Chessboard : MonoBehaviour
    {
        [SerializeField] private CrazyPawnSettings settings;
        private Material chessboardMaterial;

        private void Awake()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                chessboardMaterial = renderer.material;
            }

            ApplySettings();
        }

        private void ApplySettings()
        {
            if (chessboardMaterial != null && settings != null)
            {
                ChangeSize();
                SetColors();
            }
        }

        private void ChangeSize()
        {
            float cellScale = chessboardMaterial.GetFloat("_Scale") / 2f;
            float chessboardSize = settings.CheckerboardSize * cellScale;
            
            transform.localScale = new Vector3(chessboardSize, 0.1f, chessboardSize);
        }
        
        private void SetColors()
        {
            chessboardMaterial.SetColor("_Color1", settings.BlackCellColor);
            chessboardMaterial.SetColor("_Color2", settings.WhiteCellColor);
        }
    }
}