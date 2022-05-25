
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject redGhost;
    public TextMeshProUGUI countTimer;
    public GameObject loseObject;
    private int timer;
    private float count;
    private int interval = 5;
    private float nexTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        Instantiate(redGhost, new Vector3(500f, 250f, 0f), Quaternion.identity);
        Instantiate(redGhost, new Vector3(500f, 250f, 0f), Quaternion.identity);
        Instantiate(redGhost, new Vector3(500f, 250f, 0f), Quaternion.identity);
        
        loseObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        timer = (int)count;
        if (Time.time>=nexTime)
        {
            Instantiate(redGhost, new Vector3(500f, 250f, 0f), Quaternion.identity);
            nexTime += interval;
        }
        if (count >= 0f)
        {
            
            if (!loseObject.activeSelf)
            {
                //print(" timer" + count);
                countTimer.text = "Timer: " + timer.ToString();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void GameOver()
    {
        loseObject.SetActive(true);
    }
}
