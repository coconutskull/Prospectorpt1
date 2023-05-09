using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class stores info about decorator or pip from JSON_Deck
[System.Serializable]
public class JsonPip
{
    public string              type = "pip";
    public Vector3             loc;
    public bool                flip = false;
    public float               scale = 1;
}

[System.Serializable]
public class JsonCard
{
    public int                 rank;
    public string              face;
    public List<JsonPip>       pips = new List<JsonPip>();
}

[System.Serializable]
public class JsonDeck
{
    public List<JsonPip>       decorators = new List<JsonPip>();
    public List<JsonCard>      cards = new List<JsonCard>();
}
public class JsonParseDeck : MonoBehaviour
{
    private static JsonParseDeck S { get; set; }
    [Header("Inscribed")] 
    public TextAsset jsonDeckFile; //ref to json deck text

    [Header("Dynamic")] 
    public JsonDeck deck;

    
    void Awake()
    {
        if (S != null)
        {
            Debug.LogError("JsonParseDeck.S can't be set a 2nd time!");
            return;
        }

        S = this;
        
        deck = JsonUtility.FromJson<JsonDeck>(jsonDeckFile.text);
    }

    ///<summary>
    /// Returns the decorator layout info for all cards.
    /// </summary>
    static public List<JsonPip> DECORATORS
    {
        get {return S.deck.decorators;}
    }

    ///<summary>
    /// Returns the JsonCard matching the rank passed in.
    /// Note: The rank should be 1(Ace) - 13 (King).
    /// </summary>
    /// <param name ="rank">Must be an int in range 1-13</param>
    /// <returns>JsonCard information</returns>
    static public JsonCard GET_CARD_DEF(int rank)
    {
        if ((rank < 1) || (rank > S.deck.cards.Count))
        {
            Debug.LogWarning("Illegal rank argument: " + rank);
            return null;
        }

        return S.deck.cards[rank - 1];
    }
    
    // Update is called once per frame
    //void Update()
    //{

    //}
}
