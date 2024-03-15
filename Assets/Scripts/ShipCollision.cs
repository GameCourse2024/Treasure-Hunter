using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipCollision : MonoBehaviour
{
    [SerializeField] private float timer = 0.8f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a tag
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManagerGamePlay.Instance.Play("ShipCrash");

            // Print the tag of the object that hit the ship
            Debug.Log("Hit by: " + collision.gameObject.tag);
            StartCoroutine(ReloadSceneAfterDelay());
        }
    }

    IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(timer);

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
