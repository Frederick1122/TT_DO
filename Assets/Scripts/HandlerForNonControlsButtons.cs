using UnityEngine;
using UnityEngine.SceneManagement;

public class HandlerForNonControlsButtons : MonoBehaviour
{
    private void Update()
    {
        var PIS = PlayerInputSystem.Instance.GetPIS();
        
        if(PIS.IsLaunchEscMenu())
            UIManager.Instance.OpenEscMenu();

        if (PIS.IsPressRestart()) 
            SceneManager.LoadScene(0);
    }
}
