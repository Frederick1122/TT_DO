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
}
