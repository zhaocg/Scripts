using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XMLEditor))]
public class XMLInspector : Editor {
    XMLEditor component;
    void OnEnable()
    {
        component = (XMLEditor)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save"))
        {
            component.SaveSpells();
        }


        if (GUILayout.Button("Load"))
        {
            component.spellsContainer.spells.Clear();
            component.LoadSpells();
        }
    }
}
