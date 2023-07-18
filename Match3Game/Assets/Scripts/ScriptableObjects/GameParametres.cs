using UnityEngine;

[CreateAssetMenu(menuName = "Game/Parametres", fileName = "New Game Parametres")]
public class GameParametres : ScriptableObject
{
    [field: SerializeField] private int _columns = 9;
    [field: SerializeField] private int _rows = 11;
    [field: SerializeField] private int _moves = 20;
    [field: SerializeField] private int _candiesCount = 10;
    [field: SerializeField] private float _offsetCoef = 1f;
    [field: SerializeField] ParticleSystem _breakEffectPrefab;

    public int Columns => _columns;
    public int Rows => _rows;
    public int Moves => _moves;
    public int CandiesCount => _candiesCount;
    public float OffsetCoef => _offsetCoef;
    public ParticleSystem BreakEffectPrefab => _breakEffectPrefab;

    public void InstantiateFX(ICandyController candy, GridPosition gp)
    {
        var fx = Instantiate(BreakEffectPrefab);
        fx.gameObject.transform.position = gp.Position;
        fx.startColor = candy.GetModelInfo().CandyType.GetColor();
        GameObject.Destroy(fx.gameObject, 0.3f);
    }
}
