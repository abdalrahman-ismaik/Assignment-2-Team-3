using UnityEngine;
using UnityEngine.UI;

public class MinimapToggle : MonoBehaviour
{
    // Reference to the RawImage (minimap)
    public RawImage minimapImage;

    public Button toggleButton;

    private bool isMinimapVisible = true;

    void Start()
    {
        // If minimapImage is not assigned, attempt to find it
        if (minimapImage == null)
        {
            minimapImage = GameObject.Find("RawImage")?.GetComponent<RawImage>();
        }

        // Ensure the minimap is visible at the start
        if (minimapImage != null)
        {
            minimapImage.gameObject.SetActive(true);
        }

        // If you have a button and want to update its onClick
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleMinimap);
        }
    }

    // Method to toggle the minimap visibility
    public void ToggleMinimap()
    {
        if (minimapImage != null)
        {
            isMinimapVisible = !isMinimapVisible; // Toggle the state

            // Show or hide the minimap based on the toggle state
            minimapImage.gameObject.SetActive(isMinimapVisible);
        }

        // Optionally, change the button text if you want to show "Show Minimap" / "Hide Minimap"
        if (toggleButton != null)
        {
            Text buttonText = toggleButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = isMinimapVisible ? "Hide Minimap" : "Show Minimap";
            }
        }
    }
}
