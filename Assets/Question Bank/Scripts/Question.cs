using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class Question : ScriptableObject
{
    [TextArea]
    public string topicText;
    public string questionText;
    public string[] answerChoices = new string[4];
    public int correctAnswerIndex; // Index of the correct answer (0-3)
}
