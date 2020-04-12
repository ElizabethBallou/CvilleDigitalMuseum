using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Whilefun.FPEKit
{

    //
    // FPENoteContentsPanel
    // A simple panel with no real interaction. Just a data display element.
    //
    // Copyright 2017 While Fun Games
    // http://whilefun.com
    //
    public class FPENoteContentsPanel : MonoBehaviour
    {

        private TextMeshProUGUI noteTitle = null;
        private TextMeshProUGUI noteBody = null;

        void Awake()
        {

            noteTitle = transform.Find("NoteTitle").GetComponent<TextMeshProUGUI>();
            noteBody = transform.Find("NoteBody").GetComponent<TextMeshProUGUI>();

            if (!noteTitle || !noteBody)
            {
                Debug.LogError("FPENoteContentsPanel:: NoteTitle (Text) or NoteBody (Text) are missing! Did you rename or remove them?");
            }

        }

        public void displayNoteContents(string title, string body)
        {
            noteTitle.text = title;
            noteBody.text = body;
        }

        public void clearNoteContents()
        {
            noteTitle.text = "";
            noteBody.text = "";
        }

    }

}