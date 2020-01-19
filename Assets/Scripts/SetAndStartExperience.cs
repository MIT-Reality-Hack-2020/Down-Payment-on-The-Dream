using UnityEngine;

namespace NRKernal.NRExamples
{
    /**
    * @brief  Controls the HelloAR example.
    */
    public class SetAndStartExperience : MonoBehaviour
    {
        /**
        * @brief  A model to place when a raycast from a user touch hits a plane.
        */
        public GameObject House;
        public bool housePlaced = false;
        public bool houseStart = false;
        private PlaneDetector planeDetect;

        private void Start()
        {
            planeDetect = GetComponent<PlaneDetector>();
        }

        void Update()
        {
            if (houseStart)
            {
                Instantiate(House, new Vector3(0f, 0f, 0f), Quaternion.identity);
                houseStart = false;
            }

            if (!housePlaced)
            {
                // If the player doesn't click the trigger button, we are done with this update.
                if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
                {
                    return;
                }

                // Get controller laser origin.
                Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(ControllerAnchorEnum.RightLaserAnchor);

                RaycastHit hitResult;
                if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
                {
                    if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                    {
                        var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                        if (behaviour.Trackable.GetTrackableType() != TrackableType.TRACKABLE_PLANE)
                        {
                            return;
                        }

                        // Instantiate Andy model at the hit point / compensate for the hit point rotation.
                        Vector3 newInstantePoint = new Vector3(hitResult.point.x, hitResult.point.y + 0.25f, hitResult.point.z);
                        Instantiate(House, newInstantePoint, Quaternion.identity, behaviour.transform);
                        //House.transform.position = new Vector3 (hitResult.point.x, hitResult.point.y + .25f, hitResult.point.z);
                        //House.transform.localRotation = Quaternion.identity;
                        //House.transform.SetParent(behaviour.transform);
                        housePlaced = true;
                    }
                }
            }
        }
    }
}
