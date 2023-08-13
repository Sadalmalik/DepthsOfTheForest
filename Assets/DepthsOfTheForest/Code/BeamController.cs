using UnityEngine;

namespace Sadalmalik.Forest
{
    public class BeamController : MonoBehaviour
    {
        public Rigidbody body;
        public Collider  capsuleCollider; // For physical phase
        public Collider  meshCollider;    // For builded phase

        public float length;

        public void SetPhysical(bool isPhysical)
        {
            body.isKinematic      = isPhysical;
            body.detectCollisions = !isPhysical;
            
            capsuleCollider.enabled = isPhysical;
            meshCollider.enabled    = !isPhysical;
        }

        public void Split()
        {
            
        }
    }
}