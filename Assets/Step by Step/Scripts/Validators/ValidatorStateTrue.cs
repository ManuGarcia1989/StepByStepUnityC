using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[DisallowMultipleComponent]
public class ValidatorStateTrue : MonoBehaviour {


    [SerializeField]
    private bool _isAudioValidator;


    private AudioClip audioActivator;

    public bool IsAudioValidator
    {
        get { return _isAudioValidator; }
        set
        {
            _isAudioValidator = value;
            Activate(_isAudioValidator);
        }
    }

    public AudioClip AudioActivator
    {
        get { return audioActivator;  }
        set {
            audioActivator = value;
        }
    }

    private void Activate(bool interactable)
    {
        Debug.Log(interactable);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ValidatorStateTrue))]
public class ExampleScriptEditor : Editor
{
    public ValidatorStateTrue script;
    public void OnEnable()
    {
        script = (ValidatorStateTrue)target;
        //alejo_ = script.IsInteractive;
    }

    public override void OnInspectorGUI()
    {
        bool is_interactive_target = !script.IsAudioValidator;
        
        
        GUI.backgroundColor = (is_interactive_target) ? Color.red : Color.green;
        if (GUILayout.Button("Activar Por Audio " + script.IsAudioValidator + " (Click para hacer " + is_interactive_target + ")"))
        {
            script.IsAudioValidator = is_interactive_target;
            

        }
        GUI.backgroundColor = Color.white;
        base.OnInspectorGUI();
    }
}

#endif
