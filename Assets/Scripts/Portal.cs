using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private IntroManager gameManager;
    [SerializeField] private FakeGapManager gapManager;

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name == "Introduction")
            {
                Debug.Log("entered intro portal");
                gameManager.PlayerEnterPortal();
            }
            else if(SceneManager.GetActiveScene().name == "FakeGapLevel")
            {
                Debug.Log("entered gap portal");
                gapManager.PlayerEnterPortal();
            }
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(SceneManager.GetActiveScene().name == "Introduction")
            {
                gameManager.PlayerExitPortal();
            }
            else if(SceneManager.GetActiveScene().name == "FakeGapLevel")
            {
                //gapManager.PlayerExitPortal();
            }
    }
}
