using UnityEngine;

public class Menu : MonoBehaviour
{
    protected void SetActivePanel(bool isActive) => transform.GetChild(0).gameObject.SetActive(isActive);
}
