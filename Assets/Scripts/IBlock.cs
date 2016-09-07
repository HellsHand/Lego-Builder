using UnityEngine;

public interface IBlock
{
    void Awake();
    int GetRows();
    int GetColumns();
    float GetHeight();
    void SetPositionInParent();
    Vector3 GetPositionInParent();
    void SetColor(Color color, Material material);
}
