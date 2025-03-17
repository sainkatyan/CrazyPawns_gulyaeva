using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class Pawn : MonoBehaviour, ISpawnable
    {
        [SerializeField] private CrazyPawnSettings settings;
        
        private Material activeConnectMaterial;
        private Material deleteMaterial;
        
        private List<MeshRenderer> renderers;
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
        }
        
        private void InitPawnSettings()
        {
            activeConnectMaterial = settings.ActiveConnectorMaterial;
            deleteMaterial = settings.DeleteMaterial;
        }

        private void ActivateConnection()
        {
            ChangeMaterial(activeConnectMaterial);
        }

        private void DeleteMaterial()
        {
            ChangeMaterial(deleteMaterial);
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