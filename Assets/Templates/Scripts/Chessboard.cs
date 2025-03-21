﻿using CrazyPawn;
using UnityEngine;

namespace Templates.Scripts
{
    public class Chessboard : MonoBehaviour
    {
        private CrazyPawnSettings settings;
        private Material chessboardMaterial;

        public void Start()
        {
            settings = GameManager.Instance.Settings;
            
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
            if ((settings.CheckerboardSize % 2) == 1)
            {
                chessboardMaterial.SetFloat("_Offset", cellScale / 2f);
            }
            float chessboardSize = settings.CheckerboardSize * cellScale;
            float localHeight = transform.localScale.y;
            
            transform.localScale = new Vector3(chessboardSize, localHeight, chessboardSize);
            transform.position = new Vector3(0f, -localHeight, 0f);
        }
        
        private void SetColors()
        {
            chessboardMaterial.SetColor("_Color1", settings.BlackCellColor);
            chessboardMaterial.SetColor("_Color2", settings.WhiteCellColor);
        }

        public float GetChessboardLimit()
        {
            return transform.localScale.x / 2f;
        }
    }
}