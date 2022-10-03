using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static int CHOICE_FONT_SIZE = 18;
    public static string CHEATCODE_PROFESSOR = "GOODSTUDENT";

    List<char> recentKeys = new List<char>();

    List<KeyCode> allowedKeysCodes = new List<KeyCode>();
    public enum GameState { 
        StartingScreen, InitialStoryPoint, ShowWalkthrough,

        NetflixMain, NetflixSearch, NetflixWatch,

        GoogleMain, GoogleVisitWebsite, GoogleInstall, GoogleVirus,

        MoodleMain, MoodleForum, MoodleProfessor, MoodleProfessorGood,

        AmazonMain, AmazonSearch, AmazonBuyPills, AmazonPrimeMain,
        AmazonPrimeSearch, AmazonPrimeBuyPills, AmazonPrimeTakePills
    }
    public GameState currentState = GameState.StartingScreen;
    public GameState prevState = GameState.StartingScreen;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject walkthroughScreen;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] TextMeshProUGUI storyChoicePrefab;
    [SerializeField] GameObject storyChoicesContainer;

    [SerializeField] List<GameObject> decorationObjects;
    [SerializeField] Sprite amazonSprite;
    [SerializeField] Sprite googleSprite;
    [SerializeField] Sprite moodleSprite;
    [SerializeField] Sprite netflixSprite;

    List<Sprite> originalDecorationSprites = new List<Sprite>();

    bool hasAmazonPrime = false;

    private void Start()
    {
        EnterState(GameState.StartingScreen);
        currentState = GameState.StartingScreen;
        // we only save alphabetic chars
        allowedKeysCodes.AddRange(Enumerable.Range((int)KeyCode.A, (int)KeyCode.Z)
            .ToList().Select(x => (KeyCode)x));
        foreach (GameObject go in decorationObjects)
        {
            originalDecorationSprites.Add(go.GetComponent<Image>().sprite);
        }
    }

    void ClearChoicesList()
    {
        foreach (Transform child in storyChoicesContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void UpdateChoices(List<string> choices)
    {
        ClearChoicesList();
        for (int i = 0; i < choices.Count; i++)
        {
            TextMeshProUGUI child = Instantiate(
                storyChoicePrefab, 
                storyChoicesContainer.transform.position, 
                Quaternion.identity
            );
            child.text = $"({i + 1}) {choices[i]}";
            child.fontSize = CHOICE_FONT_SIZE;
            child.transform.SetParent(storyChoicesContainer.transform);
        }
    }

    void ChangeState(GameState from, GameState to)
    {
        LeaveState(from);
        EnterState(to);
        prevState = from;
        currentState = to;
    }

    void SetOriginalDecrotaionSprites()
    {
        for (int i = 0; i < decorationObjects.Count; i++)
        {
            decorationObjects[i].GetComponent<Image>().sprite = originalDecorationSprites[i];
        }
    }

    void SetDecorationSprites(Sprite sprite)
    {
        for (int i = 0; i < decorationObjects.Count; i++)
        {
            decorationObjects[i].GetComponent<Image>().sprite = sprite;
        }
    }

    void LeaveState(GameState state)
    {
        switch (state)
        {
            case GameState.StartingScreen:
                titleScreen.SetActive(false);
                break;
            case GameState.MoodleProfessor:
                recentKeys = new List<char>();
                break;
            case GameState.AmazonPrimeTakePills:
                hasAmazonPrime = false;
                break;
            case GameState.ShowWalkthrough:
                walkthroughScreen.SetActive(false);
                break;
            case GameState.MoodleMain:
            case GameState.GoogleMain:
            case GameState.NetflixMain:
            case GameState.AmazonMain:
            case GameState.AmazonPrimeMain:
                SetOriginalDecrotaionSprites();
                break;
        }
        
    }

    void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.ShowWalkthrough:
                walkthroughScreen.SetActive(true);
                break;
            case GameState.StartingScreen:
                gameScreen.SetActive(false);
                titleScreen.SetActive(true);
                break;
            case GameState.InitialStoryPoint:
                gameScreen.SetActive(true);

                storyText.text =
                    "You are writing your bachelor thesis and it is a real struggle. " +
                    "There is not much time left and " +
                    "you have no will to to anything about it. " +
                    "You decide to <i>copy</i> it from somewhere else and " +
                    "search for options online.";

                UpdateChoices(new List<string>
                {
                    "Visit Netflix",
                    "Visit Google",
                    "Visit RUB Moodle",
                    "Visit Amazon",
                });
                break;

            case GameState.NetflixMain:
                storyText.text =
                    "You don't have much hope for finding something " + 
                    "useful for your thesis on Netflix. Still you open " +
                    "the Netflix app and think of what you can do.";

                UpdateChoices(new List<string>
                {
                    "Search Netflix for thesis topic",
                    "Watch an episode of your favorite series",
                    "Go back"
                });

                SetDecorationSprites(netflixSprite);
                break;

            case GameState.NetflixSearch:
                storyText.text =
                    "Unfortunatelly Netflix does not even have a documentary " +
                    "related to your thesis topic " + 
                    "(you do some really complicated stuff)";

                UpdateChoices(new List<string>
                {
                    "Go back"
                });
                break;

            case GameState.NetflixWatch:
                storyText.text =
                    "You watch one episode. And then another. And another one. " +
                    "You watch the whole 6436 seasons of your favorite series and " + 
                    "<color=red>miss the thesis submission deadline</color>.";

                UpdateChoices(new List<string>
                {
                    "Try Again"
                });
                break;

            case GameState.GoogleMain:
                storyText.text =
                    "Obviously you had to try to google your thesis topic. " + 
                    "You find one particular interesting website during your search.";

                UpdateChoices(new List<string>
                {
                    "Visit website",
                    "Go Back"
                });

                SetDecorationSprites(googleSprite);
                break;

            case GameState.GoogleVisitWebsite:
                storyText.text =
                    "The website seems to have the exact thesis you need. " +
                    "You locate the necessary file on the website and download it. " + 
                    "It is a program that automatically generates your thesis.";

                UpdateChoices(new List<string>
                {
                    "Install the program",
                    "Go Back"
                });
                break;

            case GameState.GoogleInstall:
                storyText.text =
                    "The program is installed on your computer. " + 
                    "Howeber now you have doubts about the safety of the program.";

                UpdateChoices(new List<string>
                {
                    "Open the program",
                    "Go Back"
                });
                break;

            case GameState.GoogleVirus:
                storyText.text =
                    "The program was a virus. " + 
                    "<color=red>It destroys your computer</color>. " + 
                    "Now you can't write your thesis.";

                UpdateChoices(new List<string>
                {
                    "Try Again"
                });
                break;

            case GameState.MoodleMain:
                storyText.text =
                    "You log in to the RUB Moodle page. You think of what you " + 
                    "can do here to copy a thesis.";

                UpdateChoices(new List<string>
                {
                    "Ask for someone's thesis in a forum",
                    "Contact your professor",
                    "Go Back"
                });

                SetDecorationSprites(moodleSprite);
                break;

            case GameState.MoodleForum:
                storyText.text =
                    "The forum admin notices your post. " + 
                    "They reply to you that it is not allowed to " +
                    "copy another person's thesis. They report you to " +
                    "to the university authorities and " + 
                    "<color=red>you get expelled from your university</color>.";

                UpdateChoices(new List<string>
                {
                    "Try Again"
                });
                break;

            case GameState.MoodleProfessor:
                storyText.text =
                    "You send your professor an email. As usual, he doesn't respond. " + 
                    "<i>Hint: type the cheatcode <b>goodstudent</b></i>";

                UpdateChoices(new List<string>
                {
                    "Go Back"
                });
                break;

            case GameState.MoodleProfessorGood:
                storyText.text =
                    "You write a better email and tell them that you are a good student." + 
                    "<color=green>They reply immediately and send " + 
                    "you the complete thesis </color> (wow!).";

                UpdateChoices(new List<string>
                {
                    "Play again"
                });
                break;

            case GameState.AmazonMain:
                storyText.text =
                    "You want to explore the huge world of Amazon!" + 
                    "You also notice some 'magic pills' in your " + 
                    "Amazon Recommendations.";

                UpdateChoices(new List<string>
                {
                    "Search Amazon for your thesis topic",
                    "Buy 'magic pills'",
                    "Buy Amazon Prime",
                    "Go Back"
                });

                SetDecorationSprites(amazonSprite);
                break;

            case GameState.AmazonSearch:
                storyText.text =
                    "Your search hasn't lead you to any good results";

                UpdateChoices(new List<string>
                {
                    "Go Back"
                });
                break;

            case GameState.AmazonBuyPills:
                storyText.text =
                    "The pills will arrive after the thesis deadline. " +
                    "They can't make you any good now...";

                UpdateChoices(new List<string>
                {
                    "Go Back"
                });
                break;

            case GameState.AmazonPrimeMain:
                storyText.text =
                    "You want to explore the huge world of Amazon!" +
                    "You also notice some 'magic pills' in your " +
                    "Amazon Recommendations. Maybe your Prime " + 
                    "subscription can help?";

                UpdateChoices(new List<string>
                {
                    "Search Amazon for your thesis topic",
                    "Buy 'magic pills'",
                    "Go Back"
                });

                SetDecorationSprites(amazonSprite);
                break;

            case GameState.AmazonPrimeSearch:
                storyText.text =
                    "Your extended search with Amazon Prime " +
                    "hasn't lead you to any good results";

                UpdateChoices(new List<string>
                {
                    "Go Back"
                });
                break;

            case GameState.AmazonPrimeBuyPills:
                storyText.text =
                    "You buy the magic pills and they arrive in a few minutes!";

                UpdateChoices(new List<string>
                {
                    "Take the pills",
                    "Go Back"
                });
                break;

            case GameState.AmazonPrimeTakePills:
                storyText.text =
                    "You take the pills and your intelligence is boosted! " + 
                    "<color=green>You write your thesis in just a " + 
                    "few hours and submit it successfully</color>";

                UpdateChoices(new List<string>
                {
                    "Play again"
                });
                break;
        }
        
    }

    void Update()
    {
        if (GameState.ShowWalkthrough != currentState && Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeState(currentState, GameState.ShowWalkthrough);
            return;
        }
        switch (currentState)
        {
            case GameState.ShowWalkthrough:
                if (Input.anyKeyDown)
                {
                    ChangeState(currentState, prevState);
                }
                break;

            case GameState.StartingScreen:
                if (Input.anyKeyDown)
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.InitialStoryPoint:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.NetflixMain);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.GoogleMain);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(currentState, GameState.MoodleMain);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    GameState state = hasAmazonPrime ? GameState.AmazonPrimeMain : GameState.AmazonMain;
                    ChangeState(currentState, state);
                }
                break;

            case GameState.NetflixMain:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.NetflixSearch);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.NetflixWatch);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;


            case GameState.NetflixSearch:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.NetflixMain);
                }
                break;

            case GameState.NetflixWatch:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.GoogleMain:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.GoogleVisitWebsite);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.GoogleVisitWebsite:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.GoogleInstall);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.GoogleMain);
                }
                break;

            case GameState.GoogleInstall:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.GoogleVirus);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.GoogleVisitWebsite);
                }
                break;

            case GameState.GoogleVirus:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.MoodleMain:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.MoodleForum);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.MoodleProfessor);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.MoodleForum:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.MoodleProfessor:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.MoodleMain);
                }
                foreach (KeyCode keyCode in allowedKeysCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (recentKeys.Count >= 20)
                        {
                            recentKeys.RemoveAt(0);
                        }
                        recentKeys.AddRange(keyCode.ToString().ToCharArray());

                        string recentKeysStr = new string(recentKeys.ToArray());

                        if (recentKeysStr.Contains(CHEATCODE_PROFESSOR))
                        {
                            Debug.Log("GOODSTUDENT CHEATCODE ACTIVATED");
                            recentKeys = new List<char>();
                            ChangeState(currentState, GameState.MoodleProfessorGood);
                        }
                    }
                }
                break;

            case GameState.MoodleProfessorGood:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.AmazonMain:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GameState state = hasAmazonPrime ? GameState.AmazonPrimeSearch : GameState.AmazonSearch;
                    ChangeState(currentState, state);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    GameState state = hasAmazonPrime ? GameState.AmazonPrimeBuyPills : GameState.AmazonBuyPills;
                    ChangeState(currentState, state);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(currentState, GameState.AmazonPrimeMain);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.AmazonSearch:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GameState state = hasAmazonPrime ? GameState.AmazonPrimeMain : GameState.AmazonMain;
                    ChangeState(currentState, state);
                }
                
                break;

            case GameState.AmazonBuyPills:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.AmazonMain);
                }

                break;

            case GameState.AmazonPrimeMain:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.AmazonPrimeSearch);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.AmazonPrimeBuyPills);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }
                break;

            case GameState.AmazonPrimeSearch:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.AmazonPrimeMain);
                }

                break;

            case GameState.AmazonPrimeBuyPills:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.AmazonPrimeTakePills);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(currentState, GameState.AmazonPrimeMain);
                }

                break;

            case GameState.AmazonPrimeTakePills:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(currentState, GameState.InitialStoryPoint);
                }

                break;
        }
    }


}
