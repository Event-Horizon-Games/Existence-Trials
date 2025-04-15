using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool canInteract = false;

    private void Start() 
    {
        transform.parent = GameObject.Find("Environment").transform;
    }

    private void OnTriggerEnter(Collider trigger) 
    {
        if(trigger.gameObject.tag == "Player")
        {
            //Player can interact with object
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider trigger) 
    {
        if(trigger.gameObject.tag == "Player")
        {
            //Player can no longer interact with this object
            canInteract = false;
        }
    }

    public void PlayerInteract()
    {
        if(canInteract)
        {
            FindFirstObjectByType<BlueSquareManager>().LootCube();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Player is too far away!");
        }
    }
}
