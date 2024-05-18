using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace App.Code
{
    public class ARPlaceObject : MonoBehaviour
    {
        public ARRaycastManager RayCastManager;

        public GameObject CubeToPlace;                                                                                               
        public GameObject SphereToPlace;                                                                                              

        private List<ARRaycastHit> _hits;
        private List<GameObject> _instances;                                                                                          
    
        public void Awake()                                                                                                                 
        { 
            _instances = new List<GameObject>();                                                                                          
        
            _hits = new List<ARRaycastHit>();
        }

        private void SpawnPrefab(GameObject obj)                                                                                    
        {
            _instances.Add(Instantiate(obj, _hits[0].pose.position, _hits[0].pose.rotation));                                 
        }
    
        private void Update()
        {
            if (Input.touchCount == 0) return;                                                                             

            if (Input.GetTouch(0).phase != TouchPhase.Ended) return;                                                 
 
            var screenPosition = Input.GetTouch(0).position;
            var result = RayCastManager.Raycast(screenPosition, _hits, TrackableType.PlaneWithinBounds);

            if (result == true && _hits[0].trackable is ARPlane plane)                                                      
            {
                if (plane.alignment == PlaneAlignment.Vertical)                                                             
                {
                    SpawnPrefab(CubeToPlace); 
                }
                else
                {
                    SpawnPrefab(SphereToPlace);  
                }
            }
        }
    }
}
