using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptionDataParser : MonoBehaviour
{
    public static TranscriptionDataParser instance;

    public Dictionary<string, ConversationInfo> TranscriptionDataDictionary;
    
    private TextAsset transcriptionSpreadsheet;

    public InterviewData[] interviewArray;
    private string filepath;

    public Dictionary<Chunk.speakerName, ConversationInfo> metIntervieweeTracker = new Dictionary<Chunk.speakerName, ConversationInfo>();
    

    private void Awake()
    {
        if (instance!=null){
            Destroy(this.gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }

        interviewArray = Resources.LoadAll<InterviewData>("InterviewObjects");
        
        transcriptionSpreadsheet = Resources.Load<TextAsset>("TranscriptionInformation");
        TranscriptionDataDictionary = new Dictionary<string, ConversationInfo>();

       string[] rowEntries = transcriptionSpreadsheet.text.Split('\n');
       foreach (string row in rowEntries)
       {
           string[] rowCells = row.Split('\t');
           int cellCount = 0;
           for (int i = 1; i < rowCells.Length; i++)
           {
               if (rowCells[i].Trim() != "")
               {
                   cellCount++;
               }
           }
           Chunk[] chunkClassArray = new Chunk[cellCount];
           for (int i = 0; i < cellCount; i++)
           {
               Chunk thisChunk = new Chunk();
               string[] cellContents = rowCells[i + 1].Split('|');
               // call the function that return a specific enum value based on switch statement
               thisChunk.Speaker = getSpeakerNameFromString(cellContents[0]);
               //check if something went wrong and the name did not get assigned
               Debug.Assert(thisChunk.Speaker != Chunk.speakerName.none);
               //parse the timestamp from the spreadsheet and make it a float
               float myTimestamp;
               bool successfulParse = float.TryParse(cellContents[1], out myTimestamp);
               //this will only log an error if it returns false
               Debug.Assert(successfulParse);
               thisChunk.speakerTimestamp = myTimestamp;
               //assign the actual text to be printed as transcription
               thisChunk.speakerText = cellContents[2];
               chunkClassArray[i] = thisChunk;
           }
           ConversationInfo thisConversationInfo = new ConversationInfo();
           thisConversationInfo.chunks = chunkClassArray;
           string conversationName = rowCells[0].Trim().ToLower();
           thisConversationInfo.interviewData = getCorrectInterviewData(conversationName);
           TranscriptionDataDictionary.Add(conversationName, thisConversationInfo);
       }
    }

    // Start is called before the first frame update
    void Start()
    {
        metIntervieweeTracker.Add(Chunk.speakerName.AD, TranscriptionDataDictionary["js_ad_introduction"]);
        metIntervieweeTracker.Add(Chunk.speakerName.JH, TranscriptionDataDictionary["jh_introduction"]);
        metIntervieweeTracker.Add(Chunk.speakerName.JS, TranscriptionDataDictionary["js_ad_introduction"]);
        metIntervieweeTracker.Add(Chunk.speakerName.PL, TranscriptionDataDictionary["pl_introduction"]);
        foreach (var entry in metIntervieweeTracker)
        {
            Debug.Log(entry.Key);
            Debug.Log(entry.Value.chunks[0].speakerText);
        }
    }
    
    private Chunk.speakerName getSpeakerNameFromString(string cellString)
    {
        //edit out typos
        string cleanString = cellString.Trim().ToUpper();
        switch (cleanString)
        {
            case "JS":
                return Chunk.speakerName.JS;
            case "AD":
                return Chunk.speakerName.AD;
            case "PL":
                return Chunk.speakerName.PL;
            case "JH":
                return Chunk.speakerName.JH;
            case "EB":
                return Chunk.speakerName.EB;
            case "CC":
                return Chunk.speakerName.CC;
        }

        return Chunk.speakerName.none;
    }

    private InterviewData getCorrectInterviewData(string conversationName)
    {
        foreach (InterviewData interview in interviewArray)
        {
            if (interview.name.ToLower() == conversationName)
            {
                return interview;
            }
        }
        Debug.Log("InterviewData was not found for " + conversationName);
        return null;
    }

    public void playIntroClip()
    {
        
    }
    
}
