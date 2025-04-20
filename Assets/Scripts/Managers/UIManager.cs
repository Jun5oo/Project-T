using DG.Tweening;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private CardManager cardManager; 

    [Header("Phase UI")]
    [SerializeField] PhaseUI phaseUI;

    [Header("Network UI")]
    [SerializeField] WaitingUI waitingUI;

    [Header("DeckPile UI")] 
    [SerializeField] RectTransform deckPileRectTransform;
    [SerializeField] private Vector2 deckPileOnScreenPos;
    [SerializeField] private Vector2 deckPileOffScreenPos;

    [Header("DiscardPile UI")]
    [SerializeField] RectTransform discardPileRectTransform;
    [SerializeField] private Vector2 discardPileOnScreenPos;
    [SerializeField] private Vector2 discardPileOffScreenPos;

    [Header("TurnEnd UI")]
    [SerializeField] RectTransform turnEndRectTransform;
    [SerializeField] private Vector2 turnEndOnScreenPos;
    [SerializeField] private Vector2 turnEndOffScreenPos;

    public Action OnSelectionUIOnScreenComplete;
    public Action OnSelectionUIOffScreenComplete; 

    void OnEnable()
    {
        Init(); 
    }

    public void Init()
    {
        // this.phaseManager = phaseManager;

        phaseManager.OnPhaseChanged += OnUpdatePhaseUI;
        cardManager.OnDiscardComplete += HideSelectionPhaseUI; 

        deckPileOnScreenPos = new Vector2(750f, -335f);
        deckPileOffScreenPos = new Vector2(1150f, -335f);

        discardPileOnScreenPos = new Vector2(-750, -335f);
        discardPileOffScreenPos = new Vector2(-1150f, -335f);

        turnEndOnScreenPos = new Vector2(750f, -175f);
        turnEndOffScreenPos = new Vector2(1150f, -175f); 
    }

    public void OnEnableUI(IUIElement ui)
    {
        ui.Show(); 
    }

    public void OnDisableUI(IUIElement ui)
    {
        ui.Hide(); 
    }

    public void ShowSelectionPhaseUI()
    {

        deckPileRectTransform.DOAnchorPos(deckPileOnScreenPos, 0.5f);
        discardPileRectTransform.DOAnchorPos(discardPileOnScreenPos, 0.5f);
        turnEndRectTransform.DOAnchorPos(turnEndOnScreenPos, 0.5f)
            .OnComplete(() => OnSelectionUIOnScreenComplete?.Invoke()); 
    }

    public void HideSelectionPhaseUI()
    {
        // SetEase(Ease.InBack): 움직이기 전에 약간 뒤로 이동했다가 가속하는 효과 
        deckPileRectTransform.DOAnchorPos(deckPileOffScreenPos, 0.5f).SetEase(Ease.InBack);
        discardPileRectTransform.DOAnchorPos(discardPileOffScreenPos, 0.5f).SetEase(Ease.InBack);
        turnEndRectTransform.DOAnchorPos(turnEndOffScreenPos, 0.5f).SetEase(Ease.InBack)
            .OnComplete(() => OnSelectionUIOffScreenComplete?.Invoke());
    }

    public void OnUpdatePhaseUI()
    {
        phaseUI.OnUpdateText(phaseManager.GetCurrentPhase());
        OnEnableUI(phaseUI); 
    }
}
