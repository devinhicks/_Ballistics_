using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    int points = 0; // points scored
    int shotsTaken = 0; // number of times player has shot
    Vector3 shotPosition; // where ball is placed to take shot
    GameObject ball;

    public GameObject ballTarget; // backboard of hoop
    public GameObject spotMarker; // where player chooses to take shot from
    public Slider shotForceSlider;

    public Text shotForceValue;
    public Text youScored;
    public Canvas startCanvas;
    public Canvas courtLines;
    public Camera cam;

    public int shotForce = 20; // how much force is applied to ball
    public Vector3 shotChoosingVector = new Vector3(0, 24, -8); // camera angle while player chooses shot
    public Vector3 shotViewVector = new Vector3(-2, 3, -13); // camera angle while shot is taken

    // Start is called before the first frame update
    void Start()
    {
        cam.transform.position = shotViewVector;
        cam.transform.LookAt(ballTarget.transform);
        startCanvas.gameObject.SetActive(true);
        courtLines.gameObject.SetActive(false);
        youScored.text = "";
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //marker movement
        if (Input.GetKey(KeyCode.D))
        {
            // move marker right
            spotMarker.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // move marker left
            spotMarker.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // move marker up
            spotMarker.transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // move marker down
            spotMarker.transform.position += new Vector3(0, 0, -0.1f);
        }

        // set shotPosition to marker position
        shotPosition = new Vector3(spotMarker.transform.position.x,
            8, spotMarker.transform.position.z);

        // update points if collider triggered
        //points = FindObjectOfType<Score>().score;
        //numPoints.text = points.ToString();

        shotForce = (int)shotForceSlider.value;
        shotForceValue.text = shotForce.ToString();

        ball = GameObject.FindGameObjectWithTag("ball");
        // if ball hits ground, reset scene
        if (ball != null && ball.transform.position.y <= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetUpShot()
    {
        cam.transform.position = shotChoosingVector; // set camera to shot taking position
        cam.transform.LookAt(new Vector3(0, 0, -8));
        startCanvas.gameObject.SetActive(false);
        courtLines.gameObject.SetActive(true);
        Cursor.visible = true;
        spotMarker.SetActive(true);
    }

    public void setForce()
    {
        // set the shotForce to value indicated on the slider
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        float shotForce = shotForceSlider.value;
        ball.gameObject.GetComponent<FrogBoy>().launchForce = shotForce;
    }

    public void ShootYourShot()
    {
        cam.transform.position = shotViewVector; // set camera to shot view angle
        cam.transform.LookAt(ballTarget.transform); // look at the hoop
        courtLines.gameObject.SetActive(false);
        Cursor.visible = false;
        spotMarker.SetActive(false); // remove marker from scene

        ball = (GameObject)Resources.Load("basketball"); // load the ball
        ball.GetComponent<FrogBoy>().target = ballTarget; // set balls target
        ball.GetComponent<FrogBoy>().launchForce = shotForce; // set shot force
        Instantiate(ball, shotPosition, new Quaternion(0, 0, 0, 1)); // instantiate ball
    }
}
