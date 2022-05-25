using UnityEngine;

public class MirrorCollider : MonoBehaviour
{
    [SerializeField] private MirrorManager mirror;

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            mirror.PlayerEnterMirrorCollider();
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            mirror.PlayerLeaveMirrorCollider();
        }
    }
}
