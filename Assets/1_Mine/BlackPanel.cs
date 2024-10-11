using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackPanel : MonoBehaviour
{
    private bool isFaded = false;
    private float duration = 0.4f;

    public GameObject lookAroundJoystick;
    public GameObject moveAroundJoystick;
    public GameObject sitButton;

    // Reference to loading screen UI
    public GameObject loadingScreen;
    public Slider loadingBar; // This will be the progress bar

    public string sceneToLoad = "NextSceneName"; // Scene name or index to load

    public void FadeAndLoadScene()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        if (canvGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing!");
            return;
        }

        // Disable joystick input during the fade
        SetJoysticksActive(false);

        // Start fading and then load the scene after the panel goes black
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1, () => StartLoadingScene()));
    }

    private IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, System.Action onFadeComplete = null)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }

        // Ensure that the final value is set properly
        canvGroup.alpha = end;

        // Call the action after fade completes (i.e., start loading)
        onFadeComplete?.Invoke();
    }

    // Method to activate/deactivate joystick input
    private void SetJoysticksActive(bool isActive)
    {
        if (lookAroundJoystick != null)
            lookAroundJoystick.SetActive(isActive);

        if (moveAroundJoystick != null)
            moveAroundJoystick.SetActive(isActive);

        if (sitButton != null)
            sitButton.SetActive(isActive);
    }

    // Method to start loading the scene asynchronously
    private void StartLoadingScene()
    {
        // Activate loading screen and begin loading process
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Update the loading bar
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            // If loading is complete, activate the new scene
            if (operation.progress >= 0.9f)
            {
                // Activate scene when progress is full and the panel is faded out
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
