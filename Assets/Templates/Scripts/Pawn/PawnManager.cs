﻿using CrazyPawn;
using Templates.Scripts.Factory;
using UnityEngine;

namespace Templates.Scripts.Pawn
{
    public class PawnManager : MonoBehaviour
    {
        [SerializeField] private Pawn pawn;
        
        private CrazyPawnSettings settings;
        private SpawnFactory<Pawn> factory;
        
        public void Start()
        {
            this.settings = GameManager.Instance.Settings;

            factory = new SpawnFactory<Pawn>(pawn.gameObject);
            pawn.SetChessBoardLimit(GameManager.Instance.Chessboard.GetChessboardLimit());
            SpawnPawnObjects();
        }

        private void SpawnPawnObjects()
        {
            if (pawn == null) return;
            for (int i = 0; i < settings.InitialPawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPointInCircle();
                factory.Create(spawnPosition, Quaternion.identity);
            }
        }
        
        private Vector3 GetRandomPointInCircle()
        {
            float angle = Random.Range(0f, Mathf.PI * 2); 
            float radius = Mathf.Sqrt(Random.Range(0f, 1f)) * settings.InitialZoneRadius; 

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(x, 0, z);
        }
    }
}