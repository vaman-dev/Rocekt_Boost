using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour 
{
    private AudioSource audioSource;

    [Header("Audio Settings")]
    public AudioClip crashSound; 
   
    [Header("Particle Effects")]
    public ParticleSystem crashParticle; 

    [Header("Restart Settings")]
    public float restartDelay = 2f; // Delay before restarting (customizable in Inspector)

    [Header("Next Level Settings")]
    public float LoadDelay = 2f; // Delay before loading next level (customizable in Inspector)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); 
        Debug.Log("Audio started playing.");
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        switch (other.gameObject.tag) 
        {
            case "Collider": 
                Debug.Log("Restarting level after delay...");
                RestartLevel(); // Starts coroutine
                break;

            case "NextLevel":
                Debug.Log("Loading next level...");
                LoadNextLevel();
                break;

            default:
                Debug.Log("Unhandled collision type.");
                break;
    }

    void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine(restartDelay));
    }

    IEnumerator RestartLevelCoroutine(float delay)
    {
          crashParticle.Play(); // Play crash particle effect
        audioSource.PlayOneShot(crashSound); // Play crash sound effect
        Debug.Log("Playing crash sound effect.");
        yield return new WaitForSeconds(delay);
        Debug.Log("Restarting level now...");
      
        //audioSource.Play(); // Play crash sound effect
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
                StartCoroutine(LoadNextLevelCoroutine(LoadDelay));

    }

     IEnumerator LoadNextLevelCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
     int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels available!");
        }
}
       
    }



    
}










