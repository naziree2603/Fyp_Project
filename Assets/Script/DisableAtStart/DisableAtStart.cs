using UnityEngine;

public class DisableAtStart : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log(gameObject.name + " ENABLED by something!");
    }
}