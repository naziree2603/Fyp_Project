using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    private Transform Player;



    ////UI parent
    private RectTransform minimapPanel;
    private RectTransform navbarPanel;

    private RectTransform minimapMarker;
    private RectTransform navbarMarker;

    public void SetPlayer(Transform target)
    {
        Player = target;
    }

    private void Start()
    {
        var mgr = MarkerManager.Instance;
        
        //minimapMarker = Instantiate(mgr.minimapPrefab, mgr.minimapPanel);
        navbarMarker = Instantiate(mgr.navbarPrefab, mgr.navbarPanel);
       // minimapPanel = mgr.minimapPanel;
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

        // scale world → minimap
        //float mapScale = 0.05f; // adjust this!

        //minimap
        //Vector2 minimapPos = new Vector2(offset.x, offset.z) * mapScale;

        //// clamp inside map
        //minimapPos = Vector2.ClampMagnitude(minimapPos, minimapPanel.rect.width / 2f - 20f);

        //minimapMarker.anchoredPosition = minimapPos;

        //navbar
        float angle = Vector3.SignedAngle(Player.forward, offset, Vector3.up);
        float normalized = Mathf.Clamp(angle / 90f, -1f, 1f);
        float posX = normalized * (navbarPanel.rect.width / 2);
        navbarMarker.localPosition = new Vector3(posX, 0f, 0f);
    }

    private void OnDestroy()
    {
        if(minimapMarker != null) Destroy(minimapMarker.gameObject);
        if(navbarMarker != null) Destroy(navbarMarker.gameObject);
    }
}
