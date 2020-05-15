using System.Collections.Generic;
using UnityEngine;
using System;

namespace Whilefun.FPEKit
{

    //
    // FPEObjectTypeLookup
    // This class contains all Resource path, string delimiter, and LUT information for 
    // saving and loading Pickup and Inventory Item type objects in the game. 
    //
    // It must be maintained with additions or deletions of Pickup and Inventory Item types prefabs.
    //
    // Copyright 2017 While Fun Games
    // http://whilefun.com
    //
    public class FPEObjectTypeLookup
    {

        public static readonly string InventoryResourcePath = "InventoryItems/";
        public static readonly string PickupResourcePath = "Pickups/";
        public static readonly string AudioDiaryResourcePath = "AudioDiaryAudioClips/";
        public static readonly char[] PickupPrefabDelimiter = { ' ', '(' };

        private Dictionary<FPEInventoryManagerScript.eInventoryItems, string> inventoryItemsLookup;
        public Dictionary<FPEInventoryManagerScript.eInventoryItems, string> InventoryItemsLookup {
            get { return inventoryItemsLookup; }
        }

        public FPEObjectTypeLookup()
        {

            //
            // IMPORTANT: This table must be maintained in order to ensure saving and loading game data can work correctly.
            //
            inventoryItemsLookup = new Dictionary<FPEInventoryManagerScript.eInventoryItems, string>();

            try
            {

                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.APPLE, "demoApple");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.BATTERY, "demoBattery");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.KEYCARD, "demoKeycard");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.PUZZLEBALL, "demoPuzzleBall");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.COLLECTIBLE, "demoCollectible");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.SIMPLEKEY, "demoSimpleKey");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.WEDONTWANTYOUTOKNOWUS, "We don't want you to know us");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.FIFTEENPERCENTMAKEOVER,
                    "Fifteen percent make over...");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.AMPLIFYINGFOOTNOTES,
                    "Amplifying footnotes");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.ANACTOFGOD, "An act of God");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.BASEDINAMHERSTCOUNTY,
                    "Based in Amherst County");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.CITYBEAUTIFUL,
                    "The City Beautiful movement");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.DENYINGEDUCATION,
                    "Denying education");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.FUNDEDBYCONFEDERATEWHITEWOMEN,
                    "Funded by Confederate white women");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.GLORIFICATIONOLDWHITEMEN,
                    "Glorification of old white men");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.KEPTCAREFULRECORDS,
                    "Kept careful records");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.MANYOFYOUDIDNOTKNOW,
                    "Many of you did not know");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.MYBACKGROUNDHASCHANGEDALOT,
                    "My background has changed a lot");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.NONEOFTHEGUIDESAREPAID,
                    "None of the guides are paid");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.PARANOIABLACKMAJORITY,
                    "Paranoia black majority");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.PLRESEARCHREALIZATION,
                    "Phyllis Leffler research realization");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.PRODUCEHORSESHUMANS,
                    "Produce, horses, and humans");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.PUBLICSCHOOLS, "Public schools");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.RECONSTRUCTION1, "Reconstruction 1");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.TAXPAYERCOST, "Taxpayer cost");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.TODAYWEESTABLISHEDCONTROL,
                    "Today we established control");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.WEGIVEOURTHANKS,
                    "We give our thanks");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.MEMORYNOTHISTORY,
                    "Memory not history");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.SLAVEAUCTIONBLOCK,
                    "slave auction block");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.CCINTRODUCTION,
                    "Caro Campos introduction");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.OVERLOOKEDLEGACIESOFSLAVERY, "Overlooked legacies of slavery");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.LAWNISMADEUPOF, "The Lawn is made up of");
                inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.INEQUALITIESBUILTINTOARCHITECTURE, "Inequalities built into architecture");

                // 
                // To add a new item, simply add a new dictionary entry for that eInventoryItems type and 
                // string, where string is the name of the prefab inside the InventoryItems Resources sub-folder
                // For example, if you had a custom inventory item with enum name "MY_CUSTOM_INV_A", with a prefab 
                // named "myCustomInventoryItem.prefab", your entry would be: 
                // inventoryItemsLookup.Add(FPEInventoryManagerScript.eInventoryItems.MY_CUSTOM_INV_A, "myCustomInventoryItem");
                //




            }
            catch (Exception e)
            {
                Debug.LogError("FPEObjectTypeLookup:: Something went wrong when building the inventoryItemsLookup (Null argument or duplicate). Double check dictionary building for these issues. Reason: " +  e.Message);
            }

        }

    }

}