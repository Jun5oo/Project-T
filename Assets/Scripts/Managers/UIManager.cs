using DG.Tweening;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] CardManager cardManager; 

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

    void Awake()
    {
        deckPileOnScreenPos = new Vector2(750f, -335f);
        deckPileOffScreenPos = new Vector2(1150f, -335f);

        discardPileOnScreenPos = new Vector2(-750, -335f);
        discardPileOffScreenPos = new Vector2(-1150f, -335f);

        turnEndOnScreenPos = new Vector2(750f, -175f);
        turnEndOffScreenPos = new Vector2(1150f, -175f);
    }
    public void Init(CardManager cardManager)
    {
        this.cardManager = cardManager;
    }
    public void OnEnableUI(IUIElement ui)
    {
        ui.Show(); 
    }
    public void OnDisableUI(IUIElement ui)
    {
        ui.Hide(); 
    }
    public void ShowSelectionPhaseUI(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence(); 

        sequence.Join(deckPileRectTransform.DOAnchorPos(deckPileOnScreenPos, 0.5f));
        sequence.Join(discardPileRectTransform.DOAnchorPos(discardPileOnScreenPos, 0.5f));
        sequence.Join(turnEndRectTransform.DOAnchorPos(turnEndOnScreenPos, 0.5f)); 
        sequence.OnComplete(()=>callback?.Invoke());
    }
    public void HideSelectionPhaseUI(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();

        // SetEase(Ease.InBack): 움직이기 전에 약간 뒤로 이동했다가 가속하는 효과 
        sequence.Join(deckPileRectTransform.DOAnchorPos(deckPileOffScreenPos, 0.5f).SetEase(Ease.InBack));
        sequence.Join(discardPileRectTransform.DOAnchorPos(discardPileOffScreenPos, 0.5f).SetEase(Ease.InBack));
        sequence.Join(turnEndRectTransform.DOAnchorPos(turnEndOffScreenPos, 0.5f).SetEase(Ease.InBack));
        sequence.OnComplete(() => callback?.Invoke());
    }
    public void OnUpdatePhaseUI(IPhase currentPhase)
    {
        phaseUI.OnUpdateText(currentPhase);
        OnEnableUI(phaseUI); 
    }
}
