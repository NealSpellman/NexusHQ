using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootMatrix
{
    public int LootTableIndex { get; set; }
    public int ItemID { get; set; }
    public int MissionDrop { get; set; }
    public string Description { get; set; }
}
