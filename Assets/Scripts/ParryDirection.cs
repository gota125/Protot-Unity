using UnityEngine;

public class ParryDirection : MonoBehaviour
{ 
    [SerializeField] private GameObject player ;
    void Update()
    {
      Parrydirection();  
    }

    public void Parrydirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseToWorld = Camera.main .ScreenToWorldPoint(mousePos);
        mouseToWorld.z = 0;
        
        transform.position = player.transform.position   ;
       
        
    }
}
