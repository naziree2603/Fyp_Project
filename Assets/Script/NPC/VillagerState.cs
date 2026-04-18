using UnityEngine;

public class VillagerState : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSafe()
    {
        Debug.Log("Villager is now safe!");
        animator.SetBool("IsSafe", true);
    }
}