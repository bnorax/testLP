using UnityEngine;
public class Misc : MonoBehaviour
{
    private void OnEnable()
    {
        TouchScreenKeyboard.Android.consumesOutsideTouches = false; // disable additional click when typing with keyboard
    }
    public void ExitApp() //quitting app
    {
        Application.Quit();
    }
}
