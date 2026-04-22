using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    private Transform Player;



    ////UI parent
    private RectTransform minimapPanel;
    private RectTransform navbarPanel;

    private RectTransform minimapMarker;
    private RectTransform navbarMarker;

    public static QuestMarker instance;

    void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false); ; // prevent duplicate
            return;
        }

        instance = this;
    }

    public void SetPlayer(Transform target)
    {
        Player = target;
    }

    private void Start()
    {
        var mgr = MarkerManager.Instance;
        
        
        navbarMarker = Instantiate(mgr.navbarPrefab, mgr.navbarPanel);
       
        navbarPanel = mgr.navbarPanel;
    }

    private void LateUpdate()
    {
        if (Player == null)
        {
            Player = MarkerManager.Instance.Player;
            if (Player == null) return;
        }

        Vector3 offset = transform.position - Player.position;

        // ignore height
        offset.y = 0;

       

        //navbar
        float angle = Vector3.SignedAngle(Player.forward, offset, Vector3.up);
        float normalized = Mathf.Clamp(angle / 90f, -1f, 1f);
        float posX = normalized * (navbarPanel.rect.width / 2);
        navbarMarker.localPosition = new Vector3(posX, 0f, 0f);
    }

    private void OnDestroy()
    {
        
        if(navbarMarker != null) Destroy(navbarMarker.gameObject);
    }
}
