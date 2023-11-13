using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScrn : MonoBehaviour
{
    public float displayTime = 5f; // Time in seconds the text will be displayed
    public TextMeshProUGUI gameStartText; // Make the Text variable public

    private void Start()
    {
        // Check if the Text component was found
        if (gameStartText != null)
        {
            // Set the alpha to fully opaque
            gameStartText.alpha = 1f;

            // Start the coroutine to fade out the text after the specified display time
            StartCoroutine(FadeOutText());
        }
        else
        {
            Debug.LogError("Text component not found. Make sure it exists as a child of the GameStart GameObject.");
        }
    }

    IEnumerator FadeOutText()
    {
        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Gradually decrease the alpha over time
        while (gameStartText.alpha > 0)
        {
            gameStartText.alpha -= Time.deltaTime / 2f; // Adjust the divisor to control the fade speed
            yield return null;
        }

        // Ensure the alpha is fully transparent
        gameStartText.alpha = 0;

        // Disable the Text component
        gameStartText.enabled = false;
    }
}
