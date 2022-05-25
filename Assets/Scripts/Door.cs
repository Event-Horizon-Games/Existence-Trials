using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool playerInRange;
    private bool doorIsOpen;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerInRange)
            {
                if(doorIsOpen)
                {
                    animator.Play("DoorClose");
                    doorIsOpen = false;
                }
                else
                {
                    animator.Play("DoorOpen");
                    doorIsOpen = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
