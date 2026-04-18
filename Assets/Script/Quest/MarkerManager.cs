using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public static MarkerManager Instance;

    public Transform Player;

    //prefabs:
    //public RectTransform minimapPrefab;
    public RectTransform navbarPrefab;

    //UI parent
    //public RectTransform minimapPanel;
    public RectTransform navbarPanel;

    public void SetPlayer(Transform target)
    {
        Player = target;
    }

    private void Awake()
    {
        Instance = this;
    }
}


