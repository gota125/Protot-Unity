using UnityEngine;
       public class CameraFollow2D : MonoBehaviour
       {
           public Transform target;     // Le joueur
           public float smoothSpeed = 5f;
           public Vector3 offset;
       
           void LateUpdate()
           {
               if (target == null) return;
       
               Vector3 desiredPosition = target.position + offset;
               Vector3 smoothedPosition = Vector3.Lerp(
                   transform.position,
                   new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z),
                   smoothSpeed * Time.deltaTime
               );
       
               transform.position = smoothedPosition;
           }
       }

