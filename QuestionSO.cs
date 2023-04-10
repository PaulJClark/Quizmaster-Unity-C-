using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField]  string[] answers = new string[4];
    [SerializeField] int correctIndex;
    [SerializeField] AudioClip questionSFX;
    [SerializeField] Sprite  questionImage;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectIndex()
    {
        return correctIndex;
    }

    public Sprite GetImage()
    {
        return questionImage;
    }

}
