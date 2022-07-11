using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private int _bestResult;

    public void Init(int bestResult)
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _bestResult = bestResult;

        UpdateBestResultText();
    }

    private void UpdateBestResultText()
    {
        _textMesh.text = _bestResult.ToString();
    }
}
