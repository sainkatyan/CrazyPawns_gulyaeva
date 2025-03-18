using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class Pawn : MonoBehaviour, ISpawnable, IDraggable
    {
        public event Action OnPositionChanged;
        public event Action OnDestroyed;
        
        public float boardLimit;

        [SerializeField] private CrazyPawnSettings settings;
        [SerializeField] private BoxCollider pawnBodyCollider;

        public Material defaultMaterial;
        private Material deleteMaterial;

        private List<MeshRenderer> renderers;
        private Vector3 targetPosition;

        private bool isDragging = false;
        private bool isOutBounds = false;

        public void OnSpawn()
        {
            SetChildrenAllMaterials();
            InitPawnSettings();
        }

        private void SetChildrenAllMaterials() //should add in inspector changable materials
        {
            renderers = new List<MeshRenderer>();
            foreach (MeshRenderer item in gameObject.GetComponentsInChildren(typeof(MeshRenderer)))
            {
                renderers.Add(item);
            }

            defaultMaterial = new Material(renderers[0].material);
        }

        private void InitPawnSettings()
        {
            deleteMaterial = settings.DeleteMaterial;
            pawnBodyCollider.enabled = true;
        }

        public void SetChessBoardLimit(float boardLimit)
        {
            this.boardLimit = boardLimit;
        }

        public void OnDragStart()
        {
            isDragging = true;
            pawnBodyCollider.enabled = false;
        }

        public void OnDrag(Vector3 position)
        {
            targetPosition = position;
            CheckBounds();
            OnPositionChanged?.Invoke();
        }

        private void Update()
        {
            if (isDragging)
            {
                transform.position = targetPosition;
            }
        }

        public void OnDragEnd()
        {
            pawnBodyCollider.enabled = true;
            if (IsOutside())
            {
                Destroy(gameObject);
                OnDestroyed?.Invoke();
            }
            else
            {
                isDragging = false;
            }
        }

        private void CheckBounds()
        {
            if (IsOutside() && !isOutBounds)
            {
                isOutBounds = true;
                DeleteMaterial();
            }
            else if (!IsOutside() && isOutBounds)
            {
                isOutBounds = false;
                ResetMaterial();
            }
        }

        private bool IsOutside()
        {
            if (Mathf.Abs(transform.position.x) > boardLimit) return true;
            return Mathf.Abs(transform.position.z) > boardLimit;
        }

        private void DeleteMaterial()
        {
            ChangeMaterial(deleteMaterial);
        }

        private void ResetMaterial()
        {
            ChangeMaterial(defaultMaterial);
        }

        private void ChangeMaterial(Material changeMaterial)
        {
            foreach (var item in renderers)
            {
                item.material = changeMaterial;
            }
        }
    }
}