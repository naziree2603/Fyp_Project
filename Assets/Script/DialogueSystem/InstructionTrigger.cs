using System.Collections;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public GameObject instructionPanel;
    private bool hasShown = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasShown)
        {
            hasShown = true;

            instructionPanel.SetActive(true);

            // Optional auto hide
            StartCoroutine(HideAfterTime());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructionPanel.SetActive(false);
        }
    }

    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(5f);
        instructionPanel.SetActive(false);
    }
}