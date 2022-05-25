using UnityEngine;
//using gameController = GameController;

public class PacManController : MonoBehaviour
{
    private Vector3 velocity;

    private SpriteRenderer rend;
    private Animator anim;
    public float speed = 50.0f;
    public GameController gameController;
    

    // Use this for initialization
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // calculate location of screen borders
        // this will make more sense after we discuss vectors and 3D
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //get the width of the object
        float width = rend.bounds.size.x;
        float height = rend.bounds.size.y;

        //set the direction based on  input
        if (Input.GetKey("left"))
        {
            velocity = new Vector3(-50f, 0f, 0f);
            anim.Play("PacManLeft");
        }
        if (Input.GetKey("right"))
        {
            velocity = new Vector3(50f, 0f, 0f);
            anim.Play("PacManRight");
        }
        if (Input.GetKey("down"))
        {
            velocity = new Vector3(0f, -50f, 0f);
            anim.Play("PacManDown");
        }
        if (Input.GetKey("up"))
        {
            velocity = new Vector3(0f, 50f, 0f);
            anim.Play("PacManTop");
        }

        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y <= bottomBorder + height / 2.0) && velocity.y < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.y >= topBorder - height / 2.0) && velocity.y > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        transform.position = transform.position + velocity * Time.deltaTime * speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        //uncomment this in the next step
        //to notify the GameController that the game is over
        gameController.GameOver();

        //this gets rid of your PacMan
        Destroy(gameObject);
        //other.gameObject.SetActive(false);
    }
}
