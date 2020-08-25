using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public sealed class LevelSelectManager : Manager<LevelSelectManager>
{
    [Tooltip("List of levels")]
    [SerializeField] private LevelRules[] levelRules;

    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvas;

    [SerializeField] private Transform cardHolder;
    [SerializeField] private Button cardPrefab;

    private readonly List<Button> cards = new List<Button>();

    private int levelIndex;

    public override void Awake()
    {
        base.Awake();

        SpawnLevelCards();
        LoadProgress();
    }

    private void SpawnLevelCards()
    {
        for (int i = 0; i < levelRules.Length; i++)
        {
            Button card = Instantiate(cardPrefab, cardHolder);
            cards.Add(card);

            LevelRules rules = levelRules[i];

            cards[i].onClick.AddListener(() =>
            {
                StartLevel(rules);
            });

            cards[i].GetComponentInChildren<TextMeshProUGUI>().text = rules.Index.ToString();
        }
    }

    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey(Constants.LEVEL_PROGRESS_KEY))
        {
            levelIndex = PlayerPrefs.GetInt(Constants.LEVEL_PROGRESS_KEY, 0);
        }
        else
        {
            PlayerPrefs.SetInt(Constants.LEVEL_PROGRESS_KEY, 0);
        }

        bool locked;

        for (int i = 0; i < cards.Count; i++)
        {
            locked = i > levelIndex;

            cards[i].interactable = !locked;
            cards[i].transform.GetChild(0).gameObject.SetActive(locked);
            cards[i].transform.GetChild(1).gameObject.SetActive(!locked);
        }
    }

    public void ShowLevelSelect()
    {
        LoadProgress();

        cam.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    public void HideLevelSelect()
    {
        cam.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    public void StartLevel(LevelRules rules)
    {
        if (rules.Index - 1 > levelIndex)
            return;

        GameManager.Instance.Configure(rules);

        HideLevelSelect();
    }
}