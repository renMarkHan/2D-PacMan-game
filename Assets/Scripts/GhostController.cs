using UnityEngine;
using Random = UnityEngine.Random;

public class GhostController : MonoBehaviour
{
    private Vector3 velocity;

    private SpriteRenderer rend;
    private Animator anim;
    public float speed = 50.0f;
    private int nextUpdate=1;

    // Use this for initialization
    void Start()
    {
        velocity = new Vector3(50f*Time.deltaTime, 0f, 0f);
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

        //1% of the time, switch the direction: 
        int change = Random.Range(0, 100);
        if (change == 0)
        {
            if (-velocity.x < 0f && -velocity.y == 0f)
                anim.Play("GhostLeft");
            if (-velocity.x > 0f && -velocity.y == 0f)
                anim.Play("GhostRight");
            if (-velocity.y < 0f && -velocity.x == 0f)
                anim.Play("GhostDown");
            if (-velocity.y > 0f && -velocity.x == 0f)
                anim.Play("GhostTop");
            velocity = new Vector3(-velocity.x, -velocity.y, 0);
            
        }
        //1% of the time, switch from horizontal to vertical, or vice-versa: 
        else if (change == 1)
        {
            if (velocity.x != 0f)
            {
                if (velocity.x > 0f)
                    anim.Play("GhostTop");
                if (velocity.x < 0f)
                    anim.Play("GhostDown");
                velocity = new Vector3(0f, velocity.x, 0f);
                
            }

            else
            {
                if (velocity.y < 0f)
                    anim.Play("GhostLeft");
                if (velocity.y > 0f)
                    anim.Play("GhostRight");
                velocity = new Vector3(velocity.y, 0f, 0f);
            }
                
            
        }
        
        else if (change > 1 && change < 25)
        {
            if (Time.time >= nextUpdate)
            {
                velocity = new Vector3(-50f, 0f, 0f);
                anim.Play("GhostLeft");
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
            
        }
        else if (change >= 25 && change < 50)
        {
            if (Time.time >= nextUpdate)
            {
                velocity = new Vector3(50f, 0f, 0f);
                anim.Play("GhostRight");
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
                
        }
        else if (change >= 50 && change < 75)
        {
            if (Time.time >= nextUpdate)
            {
                velocity = new Vector3(0f, -50f, 0f);
                anim.Play("GhostDown");
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
                
        }
        else if (change >= 75 && change < 100)
        {
            if (Time.time >= nextUpdate)
            {
                velocity = new Vector3(0f, 50f, 0f);
                anim.Play("GhostTop");
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
                
        }


        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(-velocity.x, 0f, 0f);
            anim.Play("GhostRight");
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(-velocity.x, 0f, 0f);
            anim.Play("GhostLeft");
        }
        if ((transform.position.y <= bottomBorder + height / 2.0) && velocity.y < 0f)
        {
            velocity = new Vector3(0f, -velocity.y, 0f);
            anim.Play("GhostTop");
        }
        if ((transform.position.y >= topBorder - height / 2.0) && velocity.y > 0f)
        {
            velocity = new Vector3(0f, -velocity.y, 0f);
            anim.Play("GhostDown");
        }
        transform.position = transform.position + velocity * Time.deltaTime * speed;
    }
}
