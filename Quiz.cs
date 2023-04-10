using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    [SerializeField] float numberOfQuestions = 5;
    float remainder;
    QuestionSO currentQuestion;
    BackgroundManager backgroundManager;
    [SerializeField] Sprite questionImage;
    

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctIndex;
    bool hasAnsweredEarly = true;

    [Header("Buttons")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctSprite;
    [SerializeField] Sprite incorrectSprite;
   

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("Sound")]
    SoundManager soundManager;

    public bool isComplete;

    void Awake()
    {
        timer =FindObjectOfType<Timer>();
        scoreKeeper =FindObjectOfType<ScoreKeeper>();
        backgroundManager = FindObjectOfType<BackgroundManager>();
        soundManager = FindObjectOfType<SoundManager>();
        remainder = questions.Count - numberOfQuestions;
        Debug.Log(remainder);
        progressBar.maxValue = numberOfQuestions;
        progressBar.minValue = 0;
        progressBar.value = 0;

    }

    void Update() 
    {
        timerImage.fillAmount =  timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void onAnswerSelected(int index)
    {
        TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = Color.yellow;
        
        soundManager.PlayAnswerSound();
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }
    
    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if(index == currentQuestion.GetCorrectIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }

        else
        {
            correctIndex = currentQuestion.GetCorrectIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctIndex);
            questionText.text = "Sorry, that was incorrect."; //+ correctAnswer;
            buttonImage = answerButtons[correctIndex].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
        }
    }

    //Questions 

    void GetNextQuestion()
    {
        if(questions.Count > remainder)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandonQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
            backgroundManager.SetBackground(questionImage);
            //backgroundManager.GetNextImage();
        }
    }
    
    void GetRandonQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        questionImage = currentQuestion.GetImage();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
            buttonText.color = Color.white;
        }
    }

    //Answer buttons

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i ++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }

    void quit()
    {
        Application.Quit();
    }

    //void GetBackground()
    //{
        //questionImage = currentQuestion.GetImage();
        //backgroundManager(questionImage);
    //}
}
