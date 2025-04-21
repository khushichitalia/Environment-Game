using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class TriviaManager : MonoBehaviour
{
    
    public Text q;
        
    public  List<int> usedEquestions = new List<int>();
    public  List<int> usedMquestions = new List<int>();
    public  List<int> usedHquestions = new List<int>();

    static Unity.Mathematics.Random rnd = new Unity.Mathematics.Random((uint)System.DateTime.Now.Ticks.GetHashCode());

    public Button a1, a2, a3, a4;
    
    private TextMeshProUGUI a1T, a2T, a3T, a4T;
    
    private bool allEQuestions = false;
    private bool allMQuestions = true;
    private bool allHQuestions = true;
    private bool done = false;

    public int score = 0;
    
    private string correctAnswer = "";

    public GameTimerManager GameTimerManager;
    
    void Start()
    {
        q.text = "Let's learn some environment TRIVIA! You will have 1 minute to anwser as many questions as you can right! The questions will get harder as you go so don't feel bad if you cant answer them all! Click any of the buttons bellow to start!";
        a1T = a1.GetComponentInChildren<TextMeshProUGUI>();
        a2T = a2.GetComponentInChildren<TextMeshProUGUI>();
        a3T = a3.GetComponentInChildren<TextMeshProUGUI>();
        a4T = a4.GetComponentInChildren<TextMeshProUGUI>();
        a1T.text = "A";
        a2T.text = "B";
        a3T.text = "C";
        a4T.text = "D";
    }
    
    public void OnAnswerClicked()
    {
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;

        GameTimerManager.enabled = true;
        
        if (clickedObj == a1.gameObject)
            StartCoroutine(IncreaseScore("A"));
        else if (clickedObj == a2.gameObject)
            StartCoroutine(IncreaseScore("B"));
        else if (clickedObj == a3.gameObject)
            StartCoroutine(IncreaseScore("C"));
        else if (clickedObj == a4.gameObject)
            StartCoroutine(IncreaseScore("D"));
    }

    IEnumerator IncreaseScore(string answerLabel)
    {
        Debug.Log("Button clicked: " + answerLabel);

        if (answerLabel == correctAnswer)
        {
            if (!allEQuestions)
            {
                ScoreManager.Instance?.AddPoints(5);
            }
            if (!allMQuestions)
            {
                ScoreManager.Instance?.AddPoints(10);
            }
            if (!allHQuestions)
            {
                ScoreManager.Instance?.AddPoints(15);
            }
            Debug.Log("Correct! Score is now: " + score);
        }
        else
        {
            Debug.Log("Wrong! No points.");
        }
        
        yield return new WaitForSeconds(0f);

        Debug.Log("Score is now: " + score);

        LoadQuestion();
    }
    
    public void LoadQuestion(){
        
        GameObject clickedObj;
        
        if (done)
        {
            Debug.Log("Done");
            return;
        }
        if (!allEQuestions)
        {
            int rand = rnd.NextInt(0, 10);

            while (usedEquestions.Contains(rand))
            {
                if (usedEquestions.Count >= 10)
                {
                    Debug.Log("out");
                    allEQuestions = true;
                    allMQuestions = false;
                    return;
                }

                rand = rnd.NextInt(0, 10);
            }
            
            usedEquestions.Add(rand);

            switch (rand)
            {
                case 0:
                    q.text = "Which of the following is a renewable energy source?";
                    a1T.text = "Coal";
                    a2T.text = "Natural Gas";
                    a3T.text = "Solar Power";
                    a4T.text = "Oil";
                    correctAnswer = "C";
                    break;
                case 1:
                    q.text = "What can you do to reduce plastic pollution?";
                    a1T.text = "Use plastic bags";
                    a2T.text = "Recycle plastic bottles"; 
                    a3T.text = "Buy more packaged food";
                    a4T.text = "Burn plastic waste";
                    correctAnswer = "B";
                    break;
                case 2:
                    q.text = "Which gas is most responsible for global warming?";
                    a1T.text = "Oxygen";
                    a2T.text = "Carbon Dioxide"; 
                    a3T.text = "Nitrogen";
                    a4T.text = "Hydrogen";
                    correctAnswer = "B";
                    break;
                case 3:
                    q.text = "Which of these is biodegradable?";
                    a1T.text = "Banana Peel"; 
                    a2T.text = "Plastic Straw";
                    a3T.text = "Aluminum Foil";
                    a4T.text = "Glass Bottle";
                    correctAnswer = "A";
                    break;
                case 4:
                    q.text = "What is the main cause of deforestation?";
                    a1T.text = "Recycling";
                    a2T.text = "Planting trees";
                    a3T.text = "Logging and agriculture"; 
                    a4T.text = "Natural rainfall";
                    correctAnswer = "C";
                    break;
                case 5:
                    q.text = "What does the '3 Rs' stand for?";
                    a1T.text = "Reduce, Reuse, Recycle"; 
                    a2T.text = "Repeat, Refill, Remove";
                    a3T.text = "Recover, Replace, Rethink";
                    a4T.text = "Report, Repair, Remove";
                    correctAnswer = "A";
                    break;
                case 6:
                    q.text = "What type of energy comes from the sun?";
                    a1T.text = "Thermal Energy";
                    a2T.text = "Wind Energy";
                    a3T.text = "Nuclear Energy";
                    a4T.text = "Solar Energy"; 
                    correctAnswer = "D";
                    break;
                case 7:
                    q.text = "Which one of these actions saves water?";
                    a1T.text = "Taking long showers";
                    a2T.text = "Leaving taps running";
                    a3T.text = "Fixing leaky faucets"; 
                    a4T.text = "Watering lawn at noon";
                    correctAnswer = "C";
                    break;
                case 8:
                    q.text = "What is the greenhouse effect?";
                    a1T.text = "The growth of plants in a glass house";
                    a2T.text = "A farming technique";
                    a3T.text = "The trapping of heat in Earth's atmosphere"; 
                    a4T.text = "UV rays bouncing off snow";
                    correctAnswer = "C";
                    break;
                case 9:
                    q.text = "Which of the following is a major source of ocean pollution?";
                    a1T.text = "Composting";
                    a2T.text = "Sewage and plastic waste"; 
                    a3T.text = "Solar panels";
                    a4T.text = "Wind turbines";
                    correctAnswer = "B";
                    break;
            }
        }
        
        if (!allMQuestions)
        {
            int rand = rnd.NextInt(0, 10);

            while (usedMquestions.Contains(rand))
            {
                if (usedMquestions.Count >= 10)
                {
                    Debug.Log("out");
                    allMQuestions = true;
                    allHQuestions = false;
                    return;
                }

                rand = rnd.NextInt(0, 10);
            }
            
            usedMquestions.Add(rand);

            switch (rand)
            {
                case 0:
                q.text = "What is the most common greenhouse gas produced by human activities?";
                a1T.text = "Methane";
                a2T.text = "Carbon Dioxide"; 
                a3T.text = "Ozone";
                a4T.text = "Nitrous Oxide";
                correctAnswer = "B";
                break;
            case 1:
                q.text = "Which practice helps prevent soil erosion?";
                a1T.text = "Deforestation";
                a2T.text = "Monoculture farming";
                a3T.text = "Planting cover crops"; 
                a4T.text = "Overgrazing";
                correctAnswer = "C";
                break;
            case 2:
                q.text = "Which type of energy is generated by moving water?";
                a1T.text = "Geothermal";
                a2T.text = "Wind";
                a3T.text = "Hydroelectric"; 
                a4T.text = "Solar";
                correctAnswer = "C";
                break;
            case 3:
                q.text = "Which of these materials takes the longest to decompose?";
                a1T.text = "Paper";
                a2T.text = "Cardboard";
                a3T.text = "Plastic bottle"; 
                a4T.text = "Orange peel";
                correctAnswer = "C";
                break;
            case 4:
                q.text = "Which country emits the most carbon dioxide annually (as of recent data)?";
                a1T.text = "India";
                a2T.text = "Russia";
                a3T.text = "China"; 
                a4T.text = "USA";
                correctAnswer = "C";
                break;
            case 5:
                q.text = "What is 'phantom power' or 'vampire energy'?";
                a1T.text = "Power lost in electric cars";
                a2T.text = "Energy used by idle electronics"; 
                a3T.text = "Electricity in unused power lines";
                a4T.text = "Energy stored in batteries";
                correctAnswer = "B";
                break;
            case 6:
                q.text = "Which of the following best describes biodiversity?";
                a1T.text = "Number of plants in a garden";
                a2T.text = "Variety of life in an ecosystem"; 
                a3T.text = "Amount of rainfall in a year";
                a4T.text = "Air quality level";
                correctAnswer = "B";
                break;
            case 7:
                q.text = "Which transportation method produces the most CO₂ per passenger?";
                a1T.text = "Bicycle";
                a2T.text = "Train";
                a3T.text = "Airplane"; 
                a4T.text = "Electric car";
                correctAnswer = "C";
                break;
            case 8:
                q.text = "What is the goal of sustainable development?";
                a1T.text = "Economic growth only";
                a2T.text = "Use resources faster";
                a3T.text = "Meet current needs without harming future generations"; 
                a4T.text = "Ban technology";
                correctAnswer = "C";
                break;
            case 9:
                q.text = "Which of these is a major source of indoor air pollution?";
                a1T.text = "Houseplants";
                a2T.text = "Burning wood or coal indoors"; 
                a3T.text = "Electric stoves";
                a4T.text = "Open windows";
                correctAnswer = "B";
                break;
            }
        }
        
        if (!allHQuestions)
        {
            int rand = rnd.NextInt(0, 10);

            while (usedHquestions.Contains(rand))
            {
                if (usedHquestions.Count >= 10)
                {
                    Debug.Log("out");
                    done = true;
                    return;
                }

                rand = rnd.NextInt(0, 10);
            }
            
            usedHquestions.Add(rand);

            switch (rand) 
            {
                case 0:
                    q.text = "Which environmental agreement aims to limit global warming to below 2°C?";
                    a1T.text = "Kyoto Protocol";
                    a2T.text = "Montreal Agreement";
                    a3T.text = "Paris Agreement"; 
                    a4T.text = "Stockholm Declaration";
                    correctAnswer = "C";
                    break;
                case 1:
                    q.text = "What does the IPCC stand for?";
                    a1T.text = "International Panel for Climate Cooperation";
                    a2T.text = "Intergovernmental Panel on Climate Change"; 
                    a3T.text = "Institute for Planetary Climate Control";
                    a4T.text = "International Pollution Control Council";
                    correctAnswer = "B";
                    break;
                case 2:
                    q.text = "Which ecosystem stores the most carbon?";
                    a1T.text = "Coral reefs";
                    a2T.text = "Grasslands";
                    a3T.text = "Mangroves";
                    a4T.text = "Peatlands"; 
                    correctAnswer = "D";
                    break;
                case 3:
                    q.text = "Which of these species is considered a keystone species?";
                    a1T.text = "Rabbit";
                    a2T.text = "Otter";
                    a3T.text = "Wolf"; 
                    a4T.text = "Antelope";
                    correctAnswer = "C";
                    break;
                case 4:
                    q.text = "Which gas has the highest global warming potential (GWP) per molecule?";
                    a1T.text = "Carbon Dioxide";
                    a2T.text = "Methane";
                    a3T.text = "Nitrous Oxide";
                    a4T.text = "Sulfur Hexafluoride"; 
                    correctAnswer = "D";
                    break;
                case 5:
                    q.text = "What is environmental justice primarily concerned with?";
                    a1T.text = "Building more parks";
                    a2T.text = "Protecting endangered species";
                    a3T.text = "Fair treatment in environmental policy"; 
                    a4T.text = "Banning fossil fuels";
                    correctAnswer = "C";
                    break;
                case 6:
                    q.text = "Which country was the first to declare a climate emergency?";
                    a1T.text = "Canada";
                    a2T.text = "Germany";
                    a3T.text = "United Kingdom"; 
                    a4T.text = "Sweden";
                    correctAnswer = "C";
                    break;
                case 7:
                    q.text = "Which technology removes CO₂ directly from the atmosphere?";
                    a1T.text = "Geothermal capture";
                    a2T.text = "Direct air capture"; 
                    a3T.text = "Hydropower cooling";
                    a4T.text = "Photosynthesis enhancement";
                    correctAnswer = "B";
                    break;
                case 8:
                    q.text = "What is a major drawback of lithium-ion batteries used in EVs?";
                    a1T.text = "Too heavy to transport";
                    a2T.text = "They emit CO₂";
                    a3T.text = "Mining causes environmental harm"; 
                    a4T.text = "They are illegal in some countries";
                    correctAnswer = "C";
                    break;
                case 9:
                    q.text = "What is the term for species dying off at an unusually fast rate due to human activity?";
                    a1T.text = "Natural selection";
                    a2T.text = "Background extinction";
                    a3T.text = "Sixth mass extinction"; 
                    a4T.text = "Evolutionary delay";
                    correctAnswer = "C";
                    break;
            }

        }
    }
}
