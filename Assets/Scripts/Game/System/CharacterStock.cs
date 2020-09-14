using System.Collections.Generic;

public class CharacterStock
{
    public CharacterStock()
    {
        probability = 0.0f;
        characterIds = new List<int>();
    }

    public CharacterStock(float probability, List<int> characterIds)
    {
        this.probability = probability;
        this.characterIds = characterIds;
    }

    public float probability { get; set; }
    public List<int> characterIds { get; set; }
}
