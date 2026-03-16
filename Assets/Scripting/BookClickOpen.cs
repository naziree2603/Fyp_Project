using UnityEngine;
using UnityEngine.SceneManagement;

public class BookClickOpen : MonoBehaviour
{
    [Header("Book Objects")]
    public GameObject CloseBook;
    public GameObject OpenBook;

    [Header("Magic Effects")]
    public GameObject MEffect1;
    public GameObject MEffect2;
    public float magicDelay = 0.5f;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Transform cameraFocus;
    public float cameraMoveSpeed = 2f;
    public float cameraHoldTime = 1.5f;

    [Header("Scene Load")]
    public string gameSceneName;

    private bool opened = false;

    void Update()
    {
        if (opened) return;


        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject == CloseBook)
                {

                    opened = true;
                    StartCoroutine(PlayBookCutscene());
                }
            }
        }
    }

    private System.Collections.IEnumerator PlayBookCutscene()
    {

        OpenBook.SetActive(true);
        CloseBook.SetActive(false);


        if (MEffect1) MEffect1.SetActive(false);
        if (MEffect2) MEffect2.SetActive(false);


        yield return new WaitForSeconds(magicDelay);

        if(MEffect1)
        {
            MEffect1.SetActive(true);
            MEffect1.GetComponent<ParticleSystem>()?.Play();
        }

        if(MEffect2)
        {
            MEffect2.SetActive(true);
            MEffect2.GetComponent<ParticleSystem>()?.Play();
        }


        yield return StartCoroutine(MoveCameraToBook());


        yield return new WaitForSeconds(cameraHoldTime);


        if (!string.IsNullOrEmpty(gameSceneName)) ;
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private System.Collections.IEnumerator MoveCameraToBook()
    {
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        Vector3 endPos = cameraFocus.position;
        Quaternion endRot = Quaternion.LookRotation(OpenBook.transform.position - endPos);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * cameraMoveSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}