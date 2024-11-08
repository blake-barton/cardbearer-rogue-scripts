using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an entity that uses cards.
/// </summary>
[System.Serializable]
public class ECCards : EntityComponent
{
    [Header("Runtime Hand")]
    [SerializeField] GameCardDeck hand = new();

    [Header("Runtime Decks")]
    [SerializeField] GameCardDeck cardStorage = new();
    [SerializeField] GameCardDeck encounterDeck = new();

    [Header("Runtime Piles")]
    [SerializeField] GameCardDeck drawPile = new();
    [SerializeField] GameCardDeck discardPile = new();
    [SerializeField] GameCardDeck exhaustedPile = new();

    public ECCards(Entity _entity) : base(_entity)
    {
    }

    public ECCards(Entity _entity, JToken token) : base(_entity, token)
    {
        // Add cards to specified decks
        JArray handArray = (JArray)token["hand"];
        JArray cardStorageArray = (JArray)token["cardStorage"];
        JArray encounterDeckArray = (JArray)token["encounterDeck"];
        JArray drawPileArray = (JArray)token["drawPile"];
        JArray discardPileArray = (JArray)token["discardPile"];
        JArray exhaustedPileArray = (JArray)token["exhaustedPile"];

        AddCardToDeck(handArray, hand);
        AddCardToDeck(cardStorageArray, cardStorage);
        AddCardToDeck(encounterDeckArray, encounterDeck);
        AddCardToDeck(drawPileArray, drawPile);
        AddCardToDeck(discardPileArray, discardPile);
        AddCardToDeck(exhaustedPileArray, exhaustedPile);
    }

    private void AddCardToDeck(JArray jArray, GameCardDeck deck)
    {
        // Load card abilities to ECAbility (if it exists)
        if (jArray != null)
        {
            foreach (JToken cardCountToken in jArray)
            {
                // Add the specified number of cards
                int numCards = (int)cardCountToken["count"];
                for (int i = 0; i < numCards; i++)
                {
                    // Get the name of the card to add
                    string cardName = (string)cardCountToken["name"];

                    // Get the card from the card manager
                    GameCard card = CardManager.Instance.AllCardsDeck.FindCardWithName(cardName);
                    if (card != null)
                    {
                        deck.AddCardToTopOfDeck(card);
                    }
                    else
                    {
                        Debug.LogError("ECCards::AddCardToDeck: Got null card from CardManager with name: " + cardName);
                    }
                }
            }
        }
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
