using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Deck))]
[RequireComponent(typeof(JsonParseLayout))]
public class Prospector : MonoBehaviour
{
    private static Prospector S; //private singleton for Prospector

    [Header("Dynamic")] 
    public List<CardProspector> drawPile;

    private Deck                deck;
    private JsonLayout          jsonLayout;
    
    void Start()
    {
        //Set the private Singleton. We'll use later
        if (S != null) Debug.LogError("Attempted to set S more than once!");
        S = this;

        jsonLayout = GetComponent<JsonParseLayout>().layout;

        deck = GetComponent<Deck>();
        //two lines replaced the start() call 
        deck.InitDeck();
        Deck.Shuggle(ref deck.cards);

        drawPile = ConvertCardsToCardProspectors(deck.cards);
    }

    ///<summary>
    /// Converts each card in a List(card) into a List(CardProspector) so that
    /// it can be used in the prospector game
    /// </summary>
    /// <param name="listCard">A List(Card) to be converted</param>
    /// <returns>A List(CardProspector) of the converted cards </returns>
    List<CardProspector> ConvertCardsToCardProspectors(List<Card> listCard)
    {
        List<CardProspector> listCP = new List<CardProspector>();
        CardProspector cp;
        foreach (Card card in listCard)
        {
            cp = card as CardProspector;
            listCP.Add(cp);
        }

        return (listCP);
    }
}
