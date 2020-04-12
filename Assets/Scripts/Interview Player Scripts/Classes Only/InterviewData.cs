using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/InterviewData")]
public class InterviewData : ScriptableObject
{
    public AudioClip specificClip;
    public InterviewTagEnum.InterviewTag InterviewTags;
}
