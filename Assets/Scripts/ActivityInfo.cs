using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Moving a couple functions over that were in ItemSearch, since we're doing more than we should in that class.
/// </summary>
public class ActivityInfo : MonoBehaviour
{
    private List<GameObject> listedObjs;
    private List<GameObject> listedActivityObjs;
    public GameObject itemListObj;
    public GameObject itemObjectPrefab;
    public GameObject activityInfoPrefab;
    public GameObject activityName;

	void Start ()
    {
        listedObjs = new List<GameObject>();
        listedActivityObjs = new List<GameObject>();
	}


    /// <summary>
    /// Populates the list of items available for the user on a table.
    /// </summary>
    public void PopulateTable(List<ObjectTable> desiredItems, string activity, List<LootTable> desiredLootTable)
    {
        // Check if we've already got results showing.
        // If so, destroy 'em all
        if (listedObjs != null)
        {
            foreach (GameObject obj in listedObjs)
                DestroyImmediate(obj);
            listedObjs.Clear();
        }
        // Make sure we have a new accurate count of how many things we'll be displaying
        itemListObj.GetComponent<ContentStopper>().numberOfButtons = desiredItems.Count;
        // Update the activity specific UI
        UpdateActivityUI(activity, desiredLootTable);
        // TODO: Rework the item prefabs so we don't have such silly stuff happening here
        foreach (ObjectTable obj in desiredItems)
        {
            GameObject newItem = Instantiate(itemObjectPrefab) as GameObject;
            listedObjs.Add(newItem);
            newItem.GetComponent<ItemView>().SetupItem(obj);
            newItem.transform.SetParent(itemListObj.transform);
            Vector3 scale = new Vector3(0.2201701f, 0.1431104f, 0.2201701f);
            newItem.transform.localScale = (scale * .7f);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)itemListObj.transform);
        itemListObj.GetComponent<ContentSizeFitter>().SetLayoutVertical();
    }

    /// <summary>
    /// Simple function to update our activity UI to match whatever activity the user is viewing.
    /// </summary>
    /// <param name="activity"> String for the activity name.</param>
    /// <param name="desiredLootTable">LootTable list object that we should be taking min/max drop rates from, plus percentage.</param>
    private void UpdateActivityUI(string activity, List<LootTable> desiredLootTable)
    {
        activityName.GetComponent<Text>().text = activity;
        // First, confirm we don't already have some activities displaying
        // and, if we do, destroy 'em
        if (listedActivityObjs != null)
        {
            foreach (GameObject obj in listedActivityObjs)
                DestroyImmediate(obj);
            listedActivityObjs.Clear();
        }
        foreach (LootTable tbl in desiredLootTable)
        {
            GameObject lootTableEntry = Instantiate(activityInfoPrefab);
            listedActivityObjs.Add(lootTableEntry);
            lootTableEntry.transform.SetParent(this.transform);
            lootTableEntry.transform.localScale = new Vector3(1, 1, 1);
            Text activityDrops = lootTableEntry.GetComponent<ActivityInfoEntry>().activityDrops.GetComponent<Text>();
            Text activityPercent = lootTableEntry.GetComponent<ActivityInfoEntry>().activityPercent.GetComponent<Text>();
            Text activityTableEntry = lootTableEntry.GetComponent<ActivityInfoEntry>().activityTableEntry.GetComponent<Text>();
            if (tbl.MinDrop != tbl.MaxDrop)
                activityDrops.text = ("Drop Amount: " + tbl.MinDrop + " - " + tbl.MaxDrop);
            else
                activityDrops.text = ("Drop Amount: " + tbl.MinDrop);
            activityPercent.text = ("Drop Percent: " + (tbl.Percent * 100f) + "%");
            activityTableEntry.text = tbl.LootTableIndex.ToString(); // should we update this so the user understands what the number is?
        }
    }
}
