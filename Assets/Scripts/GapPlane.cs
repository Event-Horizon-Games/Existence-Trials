using UnityEngine;

public class GapPlane : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider trigger) 
    {
        controller.enabled = false;
        player.position = new Vector3(5.0f, 2.0f, player.position.z);
        controller.enabled = true;
        Debug.Log("Change position");
    }
}
