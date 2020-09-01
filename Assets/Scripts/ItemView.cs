using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public GameObject targetImageObj;
    public GameObject targetNameObj;
    public GameObject targetTableNameObj;
    public Texture2D finalResult;

    public void SetupItem(ObjectTable item)
    {
        int tempID;
        int.TryParse(item._id, out tempID);
        UpdateItemTexture(tempID);
        UpdateItemName(item._displayName);
        UpdateItemTableName(item.lootTableNum);
    }

	public void UpdateItemTexture(int itemID)
    {
        try
        {
            if (System.IO.File.OpenRead(Application.streamingAssetsPath + "/ItemIcons/" + itemID + ".dds") != null)
            {
                //Debug.Log("Image found!");
                var itemTexture = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + "/ItemIcons/" + itemID + ".dds");
                finalResult = TextureTool.LoadTextureDXT(itemTexture, TextureFormat.DXT5);
                var transform = targetImageObj.transform.localScale;
                targetImageObj.transform.localScale = new Vector3(transform.x ,transform.y * -1, transform.z);
                targetImageObj.GetComponent<RawImage>().texture = finalResult;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: No image found for object " + itemID + ", defaulting to ? icon."); //TODO: swap out LU DDS for something else, in case LEGO doesn't like us using it
        }
    }

    public void UpdateItemName(string itemName)
    {
        targetNameObj.GetComponent<Text>().text = itemName;
    }

    public void UpdateItemTableName(int tableName)
    {
        targetTableNameObj.GetComponent<Text>().text = tableName.ToString();
    }
}
