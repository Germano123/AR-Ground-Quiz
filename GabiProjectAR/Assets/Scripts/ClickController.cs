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
    private string[] answers = { "Siltoso", "Arenoso", "Siltoso", "Argiloso", "Arenoso"};
    private string[] userAnswers = new string[5];

    // reference to the buttons
    public GameObject buttons;

    // variable to control the question element
    public GameObject questionCtrl;
    // reference to the questions
    public TextMeshProUGUI question;
    // questions
    private string[] questions = { "1. Que nome se dar ao solo que ao entrar em contato com a água transforma-se em lama, assim dificultando construções sobre ele?",
    "2. Qual solo não possui grande índice de coesão, isto é, se movimenta facilmente e é altamente permeável?",
    "3. Qual solo tem grande possibilidade de ser vítima de erosão e desagregação natural, um tipo de solo que demanda muitos cuidados e manutenção?",
    "4. Qual solo é altamente denso quando não há umidade ou presença de água e ao umidificar-se, torna-se viscoso?",
    "5. Qual solo requer fundações profundas com estacas, geralmente de aço ou concreto armado?"};

    // variable to store the button that was pressed
    private string buttonPressed;

    private void Update()
    {
        // every frame an object can be selected
        selectObject();

        // if confirm controller is true, shows the confirm menu
        if (confirmCtrl && !confirm.activeSelf)
            changeState(confirm);
        
    }

    // check if the given answer is right among the correct answers saved in an array
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

        // when click to confirm, if the question is show, should hide it
        if(questionCtrl.activeSelf)
            changeState(questionCtrl);
    }

    public void confirmAnswer()
    {
        // if no button was pressed, just return
        if (buttonPressed == null)
            return;
        // switch between button options
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
        }

        // NOT WORKING
        // it should turn off the right or wrong message if they are active
        if(right.activeSelf || wrong.activeSelf)
        {
            RemoveAfterSeconds(5, right);
            RemoveAfterSeconds(5, wrong);
        }
    }

    public void selectQuestion()
    {
        // get the last pressed button name
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        if (!confirmCtrl)
            confirmCtrl = (!confirmCtrl);
        // switch between the question options
        switch (buttonPressed)
        {
            case "Question1":
                question.text = questions[0]; // quest text recieves the text
                answer.text = "Answer 1: "; // answer text recieves the first answer
                userAnswers[0] = userAnswer.text; // user answer first question recieves the last object selected
                break; // end case
            case "Question2":
                question.text = questions[1];
                answer.text = "Answer 2: ";
                userAnswers[1] = userAnswer.text;
                break;
            case "Question3":
                question.text = questions[2];
                answer.text = "Answer 3: ";
                userAnswers[2] = userAnswer.text;
                break;
            case "Question4":
                question.text = questions[3];
                answer.text = "Answer 4: ";
                userAnswers[3] = userAnswer.text;
                break;
            case "Question5":
                question.text = questions[4];
                answer.text = "Answer 5: ";
                userAnswers[4] = userAnswer.text;
                break;
        }
        // shows the question
        if(!questionCtrl.activeSelf)
            changeState(questionCtrl);
    }

    // using a Raycast from Unity, select an object and save its name into the option text
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

    // to clear more the code, a function to change a bool state
    private void changeState(GameObject any)
    {
        any.SetActive(!any.activeSelf);
    }

    // NOT WORKING
    // a function to set active a game object after a given seconds
    private IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}