using UnityEngine.UI;
using UnityEngine;

public class WhiteBallController : MonoBehaviour {
    public Camera mainCamera;
    public Camera camera2;
    public Slider mainSlider;
    public Vector3 LastPosition { get; set; }

    private float force;
    private GameController gameController;
    private bool isFirstPersonView;
    private LineRenderer lineRenderer1;
    private LineRenderer lineRenderer2;

    // Use this for initialization
    void Start ()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        force = mainSlider.value;
        isFirstPersonView = true;
        gameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();

        lineRenderer1 = GetComponent<LineRenderer>();
        lineRenderer2 = GameObject.FindGameObjectWithTag("LineRenderer").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //print("space key was pressed");
            if (isFirstPersonView)
            {
                ShowOverheadView();
            }
            else
            {
                ShowFirstPersonView();
            }
        }

        if (gameController.allBallsStopped)
        {
            lineRenderer1.SetPosition(0, transform.position);
            //Ray ray1 = mainCamera.ScreenPointToRay(
            Vector3 direction = mainCamera.transform.forward;
            direction.y = 0;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody)
                {
                    lineRenderer1.SetPosition(1, hit.point);
                    lineRenderer1.SetPosition(2, hit.point);

                    if (hit.collider.gameObject.tag == "Ball")
                    {
                        lineRenderer2.enabled = true;

                        lineRenderer2.SetPosition(0, hit.point);
                        lineRenderer2.SetPosition(1, hit.transform.position);
                        Ray r = new Ray(hit.transform.position, (hit.point - hit.transform.position).normalized);
                        lineRenderer2.SetPosition(2, hit.transform.position - 10 * r.direction);
                    }
                    else
                    {
                        lineRenderer2.enabled = false;
                    }
                }
            }
        }
    }

    public void TurnOffLineRenderers()
    {
        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
    }

    public void TurnOnLineRenderers()
    {
        lineRenderer1.enabled = true;
        lineRenderer2.enabled = true;
    }

    public void HitBall()
    {
        if (gameController.allBallsStopped)
        {
            LastPosition = transform.position;
            GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * force, ForceMode.Impulse);
            TurnOffLineRenderers();
            ShowOverheadView();
        }
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        force = mainSlider.value;
    }

    public void ShowOverheadView()
    {
        isFirstPersonView = false;
        mainCamera.enabled = false;
        camera2.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        isFirstPersonView = true;
        mainCamera.enabled = true;
        camera2.enabled = false;
    }
}