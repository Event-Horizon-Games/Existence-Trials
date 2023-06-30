using UnityEngine;

public class GlassFloorEndPlatform : MonoBehaviour
{
    [SerializeField] private GlassFloorManager glassFloorManager;

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("player on end platform");
            glassFloorManager.LoadNextLevel();
        }
    }
}
