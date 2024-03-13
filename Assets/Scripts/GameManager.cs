using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject controlsScreen;
    [SerializeField] GameObject startScreen;
    [SerializeField] Text controlsText;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayDemo()
    {
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowControls()
    {
        #if UNITY_IPHONE || UNITY_ANDROID
            controlsText.text = "Switch Gravity - Tap/Touch\n\nRun Forward - Automatic";
        #elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            controlsText.text = "Switch Gravity - Spacebar\n\nRun Forward - Automatic";
        #endif

        startScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void HideControls()
    {
        startScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}