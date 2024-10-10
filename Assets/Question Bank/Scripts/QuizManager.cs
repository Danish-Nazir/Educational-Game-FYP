using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;  // Array to hold multiple Question ScriptableObjects
    public TextMeshProUGUI questionCategoryText; // TextMeshPro for category display
    public TextMeshProUGUI questionText; // TextMeshPro for question display
    public Button[] answerButtons; // Buttons for answer choices

    private Question currentQuestion;
    private int currentQuestionIndex = 0;
    private Color[] originalButtonColors;

    void Start()
    {
        // Initialize original button colors array and store the colors
        originalButtonColors = new Color[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            originalButtonColors[i] = answerButtons[i].GetComponent<Image>().color; // Store the initial color
        }
        LoadQuestion(currentQuestionIndex); // Load the first question
    }

    // Load the question and assign answers to the buttons
    public void LoadQuestion(int index)
    {
        currentQuestion = questions[index];

        // Set the category and question text
        questionCategoryText.text = currentQuestion.topicText; // Display the question category
        questionText.text = currentQuestion.questionText; // Display the question text

        // Set button texts and reset colors
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answerChoices[i]; // Update for TextMeshPro
            answerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            int choiceIndex = i;  // Capture the current index for this loop iteration
            answerButtons[i].onClick.AddListener(() => CheckAnswer(choiceIndex));
            answerButtons[i].GetComponent<Image>().color = originalButtonColors[i]; // Reset button color to original for the next question
        }
    }

    // Check if the chosen answer is correct or wrong
    public void CheckAnswer(int chosenAnswerIndex)
    {
        if (chosenAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            answerButtons[chosenAnswerIndex].GetComponent<Image>().color = Color.green; // Correct answer
        }
        else
        {
            answerButtons[chosenAnswerIndex].GetComponent<Image>().color = Color.red; // Wrong answer
            answerButtons[currentQuestion.correctAnswerIndex].GetComponent<Image>().color = Color.green; // Highlight correct answer
        }

        // Automatically move to the next question after a delay
        Invoke("LoadNextQuestion", 2f); // Delay 2 seconds to let user see the result
    }

    // Load the next question
    public void LoadNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            LoadQuestion(currentQuestionIndex); // Load the next question
        }
        else
        {
            Debug.Log("Quiz finished!");
            EndQuiz();
        }
    }

    // End the quiz
    void EndQuiz()
    {
        questionText.text = "Quiz Finished!"; // Display end message
        questionCategoryText.text = ""; // Clear the category text
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false); // Hide buttons when quiz is over
        }
    }
}
