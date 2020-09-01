using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    public Activities[] activitiesList;
    public LootMatrix[] lootMatrixList;
    public LootTable[] lootTableList;
    public ObjectTable[] objectTableList;
    public GameObject dropdownObj; // needed to set dropdown options
    public GameObject activityInfoPanel; // needed to transfer activity info

    void Start()
    {
        // Let's load our tables immediately.
        activitiesList = FromJson<Activities>(Resources.Load<TextAsset>("Activities").ToString());
        lootMatrixList = FromJson<LootMatrix>(Resources.Load<TextAsset>("LootMatrixConverted").ToString());
        lootTableList = FromJson<LootTable>(Resources.Load<TextAsset>("LootTable").ToString());
        objectTableList = FromJson<ObjectTable>(Resources.Load<TextAsset>("ObjectTable").ToString());
        PopulateDropdownActivities();
    }

    // code from: https://stackoverflow.com/questions/55017463/trying-to-parse-a-json-array-in-unity-c-sharp-but-it-returns-null
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        JToken jToken = JToken.Parse(newJson);
        Wrapper<T> wrapper = jToken.ToObject<Wrapper<T>>();
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }

    /// <summary>
    /// Populates the dropdown menu with all available activites from the JSON.
    /// </summary>
    public void PopulateDropdownActivities()
    {
        Dropdown dp = dropdownObj.GetComponent<Dropdown>();
        List<string> options = new List<string>();
        foreach (var activity in activitiesList)
        {
            options.Add(activity.Description);
        }
        dp.ClearOptions();
        dp.AddOptions(options);
        dp.onValueChanged.AddListener(delegate { FindItemsFromActivitiy(dp.options[dp.value].text); });
    }

    /// <summary>
    /// Yeah I know, you're gonna say "Well Neal, isn't this going to be super slow?"
    /// "Well yeah, it is, but it's Sunday and 7 PM. I'm trying to enjoy my evening."
    /// </summary>
    /// <param name="itemID"> Integer corresponding to the item ID.</param>
    /// <returns></returns>
    private ObjectTable FindItemByID(int itemID)
    {
        foreach (ObjectTable obj in objectTableList)
        {
            int tempID;
            Int32.TryParse(obj._id, out tempID);
            if (itemID == tempID)
                return obj;
        }
        return null;
    }

    /// <summary>
    /// Provided a string, this returns the activity object (if it exists) from the main array.
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="activityName"></param>
    /// <returns></returns>
    public Activities FindActivityFromString(Activities[] activity, string activityName)
    {
        foreach (Activities act in activity)
        {
            if (act.Description == activityName)
                return act;
        }
        return null;
    }
    /// <summary>
    /// Provided an int, this returns a loot table object for a specified activity.
    /// </summary>
    /// <param name="lootTable"> The array that we should check for a loot table.</param>
    /// <param name="LootMatrixIndex"> The index from the activity we're looking for.</param>
    /// <returns></returns>
    public List<LootTable> FindLootTableFromActivity(LootTable[] lootTable, int LootMatrixIndex)
    {
        List<LootTable> foundTables = new List<LootTable>();
        foreach (LootTable tbl in lootTable)
        {
            if (tbl.LootMatrixIndex == LootMatrixIndex)
                foundTables.Add(tbl);
        }
        if (foundTables != null)
            return foundTables;
        return null;
    }

    public List<ObjectTable> FindLootMatrixFromIndex(LootMatrix[] lootMatrixTable, List<LootTable> LootTableIndex)
    {
        var desiredItems = new List<ObjectTable>();
        foreach (LootTable tbl in LootTableIndex)
        {
            foreach (LootMatrix mtx in lootMatrixTable)
            {
                if (mtx.LootTableIndex == tbl.LootTableIndex)
                {
                    ObjectTable item = FindItemByID(mtx.ItemID);
                    item.lootTableNum = tbl.LootTableIndex;
                    desiredItems.Add(item);
                }
            }
        }
        if (desiredItems != null)
            return desiredItems;
        else
            return null;
    }

    public List<String> PrintItemsFromMatrix(List<LootMatrix> dropsFromAct)
    {
        List<string> itemList = new List<string>();
        itemList.Add("The possible items from this activity are: " + System.Environment.NewLine);
        foreach (LootMatrix mtx in dropsFromAct)
        {
            itemList.Add(mtx.Description + ". Item ID: " + mtx.ItemID);
        }
        return itemList;
    }

    public void FindItemsFromActivitiy(string activityName)
    {
        Activities enteredActivity = FindActivityFromString(activitiesList, activityName);
        List<LootTable> desiredLootTables = FindLootTableFromActivity(lootTableList, enteredActivity.LootMatrixIndex);
        List<ObjectTable> desiredItems = FindLootMatrixFromIndex(lootMatrixList, desiredLootTables);
        
        if (enteredActivity != null)
        {
            activityInfoPanel.GetComponent<ActivityInfo>().PopulateTable(desiredItems, activityName, desiredLootTables);
        }
    }
}
