using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In another case of "Neal names things a way that makes sense, almost"...
/// This is a class for an object from the object table in LU.
/// </summary>
[System.Serializable]
public class ObjectTable
{
    public string _id { get; set; }
    public string _name { get; set; }
    public string _description { get; set; }
    public string _displayName { get; set; }
    public string _gate_version { get; set; }
    public int lootTableNum { get; set; }
}
