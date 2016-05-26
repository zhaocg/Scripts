using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class SpellsContainer
{
    [XmlArray("Spells")]
    public List<Spells> spells = new List<Spells>();

    public Spells GetSpellsByName(string name)
    {
        foreach (var item in spells)
        {
            if(item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    public Spells GetSpellsByType(SpellsType type)
    {
        foreach (var item in spells)
        {
            if (item.spellType == type)
            {
                return item;
            }
        }
        return null;
    }
}