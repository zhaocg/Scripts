using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class SpellsSystem : MonoBehaviour {

    static public SpellsSystem instance;
    public string spellsPrefabsFolderPath = "Resources/Prefabs/Spells"; 
    public SpellsContainer spellsContainer;
    

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        //SaveSpells();
        LoadSpells();
	}

    public void SaveSpells()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpellsContainer));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/spells_data.xml", FileMode.Create);
        serializer.Serialize(stream, spellsContainer);
        stream.Close();
    }

    public void LoadSpells()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpellsContainer));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/spells_data.xml", FileMode.Open);
        spellsContainer = serializer.Deserialize(stream) as SpellsContainer;
        stream.Close();
    }
}
