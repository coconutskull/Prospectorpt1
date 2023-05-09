using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Dynamic")]
    public char suit;
    public int rank;
    public Color color = Color.black;
    public string colS = "Black";
    public GameObject back;
    public JsonCard def;

    public List<GameObject> decoGOs = new List<GameObject>();
    public List<GameObject> pipGOs = new List<GameObject>();

    /// <summary>
    /// Creates this Card's visuals based on suit and rank.
    /// Note that this method assumes it will be passed as a valid suit and rank.
    /// </summary>
    /// <param name="eSuit">The suit of the card (e.g., 'C')</param>
    /// <param name="eRank">The rank from 1 to 13</param>
    /// <returns></returns>
    public void Init(char eSuit, int eRank, bool startFaceUp = true)
    {
        gameObject.name = name = eSuit.ToString() + eRank;
        suit = eSuit;
        rank = eRank;

        if (suit == 'D' || suit == 'H')
        {
            colS = "Red";
            color = Color.red;
        }

        def = JsonParseDeck.GET_CARD_DEF(rank);

        AddDecorators();
        AddPips();
        AddFace();
    }

    /// <summary>
    /// Shortcut for setting transform.localPosition.
    /// </summary>
    /// <param name="v"></param>
    public virtual void SetLocalPos(Vector3 v)
    {
        transform.localPosition = v;
    }

    private Sprite _tSprite = null;
    private GameObject _tGO = null;
    private SpriteRenderer _tSRend = null;

    private Quaternion _flipRot = Quaternion.Euler(0, 0, 180);

    /// <summary>
    /// Adds the decorators to the top-left and bottom-right of each card.
    ///     Decorators are the suit and rank in the corners of each card.
    /// </summary>
    private void AddDecorators()
    {
        foreach (JsonPip pip in JsonParseDeck.DECORATORS)
        {
            if (pip.type == "suit")
            {
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                _tSRend.sprite = CardSpritesSO.SUITS[suit];
            }
            else
            {
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                _tSRend.sprite = CardSpritesSO.RANKS[rank];
                _tSRend.color = color;
            }

            _tSRend.sortingOrder = 1;
            _tGO.transform.localPosition = pip.loc;
            if (pip.flip) _tGO.transform.rotation = _flipRot;
            if (pip.scale != 1)
            {
                _tGO.transform.localScale = Vector3.one * pip.scale;
            }
            _tGO.name = pip.type;
            decoGOs.Add(_tGO);
        }
    }

    /// <summary>
    /// Adds pips to the front of all cards from A to 10
    /// </summary>
    private void AddPips()
    {
        int pipNum = 0;
        foreach (JsonPip pip in def.pips)
        {
            _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);
            _tGO.transform.localPosition = pip.loc;
            if (pip.flip) _tGO.transform.rotation = _flipRot;
            if (pip.scale != 1)
            {
                _tGO.transform.localScale = Vector3.one * pip.scale;
            }
            _tGO.name = "pip_" + pipNum++;
            _tSRend = _tGO.GetComponent<SpriteRenderer>();
            _tSRend.sprite = CardSpritesSO.SUITS[suit];
            _tSRend.sortingOrder = 1;
            pipGOs.Add(_tGO);
        }
    }

    /// <summary>
    /// Adds the face sprite for the card ranks 11 to 13
    /// </summary>
    private void AddFace()
    {
        if (def.face == "") return;

        string faceName = def.face + suit;
        _tSprite = CardSpritesSO.GET_FACE(faceName);
        if (_tSprite == null)
        {
            Debug.LogError("Face sprite " + faceName + " not found.");
            return;
        }

        _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);
        _tSRend = _tGO.GetComponent<SpriteRenderer>();
        _tSRend.sprite = _tSprite;
        _tSRend.sortingOrder = 1;
        _tGO.transform.localPosition = Vector3.zero;
        _tGO.name = faceName;
    }
}