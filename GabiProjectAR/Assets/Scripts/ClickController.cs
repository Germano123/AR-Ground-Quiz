using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickController : MonoBehaviour
{
    // reference to the confirm UI
    public GameObject confirm;
    // confirm UI controller
    private bool confirmCtrl = false;

    // answer text
    public TextMeshProUGUI answer;

    // answer option
    public TextMeshProUGUI userAnswer;

    // variable to reference the correct answer element
    public GameObject right;
    // variable to reference the wrong answer element
    public GameObject wrong;

    // variable to store the correct answers
    private string[] answers = { "Clayey", "Sandy", "Clayey", "Silty", "Clayey", "Sandy", "Clayey", "Silty" };
    private string[] userAnswers = new string[8];

    // variable to store the button that was pressed
    private string buttonPressed;

    private void Update()
    {
        // every frame an object can be selected
        selectObject();

        // if confirm controller it's true, shows the confirm menu
        if (confirmCtrl && !confirm.activeSelf)
            changeState(confirm);
        
    }

    private void checkAnswer(int index)
    {
        if (userAnswers[index] == answers[index])
        {
            changeState(right);
            RemoveAfterSeconds(3, right);
        }
        else
        {
            changeState(wrong);
            RemoveAfterSeconds(3, wrong);
        }
    }

    public void confirmAnswer()
    {
        if (buttonPressed == null)
            return;
        switch (buttonPressed)
        {
            case "Question1":
                checkAnswer(0);
                break;
            case "Question2":
                checkAnswer(1);
                break;
            case "Question3":
                checkAnswer(2);
                break;
            case "Question4":
                checkAnswer(3);
                break;
            case "Question5":
                checkAnswer(4);
                break;
            case "Question6":
                checkAnswer(5);
                break;
            case "Question7":
                checkAnswer(6);
                break;
            case "Question8":
                checkAnswer(7);
                break;
        }
    }

    public void selectQuestion()
    {
        // get the last pressed button name
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        if (!confirmCtrl)
            confirmCtrl = (!confirmCtrl);
        switch (buttonPressed)
        {
            case "Question1":
                answer.text = "Answer 1: ";
                userAnswers[0] = userAnswer.text;
                break;
            case "Question2":
                answer.text = "Answer 2: ";
                userAnswers[1] = userAnswer.text;
                break;
            case "Question3":
                answer.text = "Answer 3: ";
                userAnswers[2] = userAnswer.text;
                break;
            case "Question4":
                answer.text = "Answer 4: ";
                userAnswers[3] = userAnswer.text;
                break;
            case "Question5":
                answer.text = "Answer 5: ";
                userAnswers[4] = userAnswer.text;
                break;
            case "Question6":
                answer.text = "Answer 6: ";
                userAnswers[5] = userAnswer.text;
                break;
            case "Question7":
                answer.text = "Answer 7: ";
                userAnswers[6] = userAnswer.text;
                break;
            case "Question8":
                answer.text = "Answer 8: ";
                userAnswers[7] = userAnswer.text;
                break;
        }
    }

    private void selectObject()
    {
        if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                userAnswer.text = hit.transform.name;
            }
        }
    }

    private void changeState(GameObject any)
    {
        any.SetActive(!any.activeSelf);
    }

    private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}