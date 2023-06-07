using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButtonNew : MonoBehaviour
{
    private Vector3[] startingPositions;
    private GameObject[] gameObjects;
    private StopwatchCounter stopwatch;
    public GameObject targetStopwatch;
    public CarController CarController;
    public Button button;
    private void Start()
    {
        // Step 1: Store starting positions
        StoreStartingPositions();


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnClickReset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToHome();
        }
    }
    
    private void StoreStartingPositions()
    {
        
        gameObjects = GameObject.FindGameObjectsWithTag("Objects");
        startingPositions = new Vector3[gameObjects.Length];
        
        for (int i = 0; i < gameObjects.Length; i++)
        {
            startingPositions[i] = gameObjects[i].transform.position;
        }
    }
    
    private void ResetPositions()
    {
        Transform currentObject;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            currentObject = gameObjects[i].transform;
            currentObject.position = startingPositions[i];
            
            gameObjects[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        Debug.Log("Done moving");
        
        

    }

    public void BackToHome()
    {
        SceneManager.LoadScene(0);
    }
    
    public void OnClickReset()
    {
        
        // Reset Timer
        targetStopwatch = GameObject.FindWithTag("Stopwatch");
        stopwatch = targetStopwatch.GetComponent<StopwatchCounter>();
        
        stopwatch.ResetCountUp();
        
        CarController.SetRotation(0);
        
        ResetPositions();

        
    }
}