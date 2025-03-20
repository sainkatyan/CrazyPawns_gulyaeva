using System;
using System.Collections.Generic;
using Templates.Scripts.Factory;
using UnityEngine;

namespace Templates.Scripts.Pawn
{
    public class Pawn : MonoBehaviour, ISpawnable, IDraggable
    {
        [SerializeField] private CrazyPawnSettings settings;
        [SerializeField] private BoxCollider pawnBodyCollider;
        [SerializeField] private List<Socket.Socket> currentSockets;
        public event Action OnPositionChanged;
        public event Action OnDestroyed;

        public float BoardLimit;

        private List<MeshRenderer> renderers;
        private Material deleteMaterial;
        private Material defaultMaterial;

        private Vector3 targetPosition;

        private bool isDragging = false;
        private bool isOutBounds = false;

        public void OnSpawn()
        {
            SetChildrenAllMaterials();
            InitPawnSettings();
        }

        private void SetChildrenAllMaterials() 
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

        private void Start()
        {
            SendCurrentSockets();
        }

        public void AddSocket(Socket.Socket socket)
        {
            if (currentSockets.Contains(socket)) return;
            currentSockets.Add(socket);
        }

        private void SendCurrentSockets()
        {
            foreach (Socket.Socket item in currentSockets)
            {
                GameManager.Instance.SocketManager.AddSocket(item);
            }
        }

        private void RemoveCurrentSockets()
        {
            foreach (Socket.Socket item in currentSockets)
            {
                GameManager.Instance.SocketManager.RemoveSocket(item);
            }
        }

        public void SetChessBoardLimit(float boardLimit)
        {
            this.BoardLimit = boardLimit;
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
                OnDestroyed?.Invoke();
                RemoveCurrentSockets();

                Destroy(gameObject);
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
            if (Mathf.Abs(transform.position.x) >= BoardLimit) return true;
            return Mathf.Abs(transform.position.z) >= BoardLimit;
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