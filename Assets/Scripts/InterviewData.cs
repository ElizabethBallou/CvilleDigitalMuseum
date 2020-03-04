using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/InterviewData")]
public class InterviewData : ScriptableObject
{
    public AudioClip clip;
    public string description;
    public string inkKnot;
    public string inkStitch;
    public string speakerPortrait;
    public InterviewTagEnum.InterviewTag InterviewTags;
}
