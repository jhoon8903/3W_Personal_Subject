using System;
using _1.Script;
using UnityEngine;
using static _1.Script.GameManager.Character;

public class NameChangedEventArgs : EventArgs
{
    public string NewName { get; set; }
    public string OldName { get; set; }

    public NameChangedEventArgs(string oldName, string newName)
    {
        OldName = oldName;
        NewName = newName;
    }
}

public class CreateNPCEventArgs : EventArgs
{
    public string NPCName { get; set; }
    public Sprite NPCSprite { get; set; }
    public CharacterTypes NPCType { get; set; }

    public CreateNPCEventArgs(string npcName, Sprite npcSprite, CharacterTypes npcType )
    {
        NPCName = npcName;
        NPCSprite = npcSprite;
        NPCType = npcType;
    }
}
