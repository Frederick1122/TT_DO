using System;
using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    /// <summary>
    /// Work ONLY in Editor.
    /// </summary>
    protected virtual void OnEditorValidate()
    {

    }

#if UNITY_EDITOR
    [Obsolete("Use OnEditorValidate instead")]
    private void OnValidate()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            return;

        OnEditorValidate();
    }
#endif
    
    protected void FillInField<T>(ref T component)
    {
        if (component.IsNullOrDefault())
            component = GetComponent<T>();
    }
}
