using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern to access the manager from other scripts
    public GameObject driver;
    public GameObject trunk;
    public GameObject leftHand;
    public Transform location;
    public TextMeshProUGUI timeText;
    private Animator Opening;
    private bool gameOver = false;
    private float time = 0;
    [HideInInspector]
    public bool opened = false;
    private void Awake()
    {
        // Implement the Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Opening = trunk.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject); // Keep the manager alive between scenes
    }

    private void Update()
    {
        if (time >= 180.0f && !gameOver)
        {
            gameOver = true;
            GameOver();
        }
        else if (opened && leftHand.activeInHierarchy)
        {
            timeText.text = "Total time: " + time.ToString();
        }
        else if (opened)
        {
            timeText.text = "Your left hand is still chained to the car!";
        }
        else
        {
            CountTime();
        }
    }

    public void CountTime()
    {
        time += Time.deltaTime;
    }

    private void GameOver()
    {
        if (Opening.GetInteger ("EtatAnim") == 1) {
            Opening.SetInteger("EtatAnim",2);
        } else {
            Opening.SetInteger("EtatAnim",1);
        };
        driver.transform.SetPositionAndRotation(location.position, location.rotation);
        Invoke(nameof(RestartGame), 5);
    }

    private void RestartGame()
    {
        // Example: Reset game variables and start a new game
        time = 0;
        // Load the first scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
