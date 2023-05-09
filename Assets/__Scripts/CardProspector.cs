using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this enum defines the variable type eCardState with four named values.
public enum eCardState {drawpile, mine, target, discard}
public class CardProspector : MonoBehaviour
{
    [Header("Dynamic: CardProspector")] 
    public eCardState state = new eCardState().drawpile;
    //hiddenby list stores which other cards will keep this one face down
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    //LayoutID matches this card to the tablea JSON if it's a tableau card
    public int                  layoutID;
    //JsonLayoutSlot class Stores info pulled in from JSON_Layout
    public JsonLayoutSlot       layoutSlot;
}
