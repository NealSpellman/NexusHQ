using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A more fitting name for this is LootTableEntry.
/// This is one entry from the master Loot table.
/// </summary>
[System.Serializable]
public class LootTable
{
    public int LootMatrixIndex { get; set; }
    public int LootTableIndex { get; set; }
    public int Rarity { get; set; }
    public double Percent { get; set; }
    public int MinDrop { get; set; }
    public int MaxDrop { get; set; }
}
