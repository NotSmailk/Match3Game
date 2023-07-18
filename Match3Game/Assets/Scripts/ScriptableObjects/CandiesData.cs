using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Candies Data", fileName = "New Candy Data")]
public class CandiesData : ScriptableObject, ICandiesData<CandyData>
{
    [field: SerializeField] private CandyData[] _candyData;

    public CandyData GetData(int index)
    {
        if (index < 0 || index >= _candyData.Length)
            throw new System.ArgumentOutOfRangeException();

        return _candyData[index];
    }

    public CandyData GetRandom()
    {
        int index = Random.Range(0, _candyData.Length);

        return _candyData[index];
    }
}

[System.Serializable]
public class CandyData : ICandyData
{
    public string Name;
    public CandyType CandyType;
    public Sprite Sprite;
}

public enum CandyType
{
    None,
    Red,
    Blue,
    Green,
    Purple,
    Orange,
    Yellow
}

public static class CandyTypeExtension
{
    public static Color GetColor(this CandyType type)
    {
        switch (type)
        {
            case CandyType.None: return new Color(0f, 0f, 0f, 0.5f);
            case CandyType.Red: return new Color(1f, 0f, 0f, 0.5f);
            case CandyType.Blue: return new Color(0f, 0f, 1f, 0.5f);
            case CandyType.Green: return new Color(0f, 1f, 0f, 0.5f);
            case CandyType.Purple: return new Color(1f, 0f, 1f, 0.5f);
            case CandyType.Orange: return new Color(1f, 0.5f, 0f, 0.5f);
            case CandyType.Yellow: return new Color(1f, 1f, 0f, 0.5f);
        }

        return Color.white;
    }
}
