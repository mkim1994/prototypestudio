using TMPro;
using UnityEngine;

public class TextAnimTest : MonoBehaviour
{
    public string Text;
    public float CharactersPerSecond;

    public bool UseMaxVisibleCharacters;

    private TMP_Text _textMesh;

    private Color32 _textColor;

    private TMP_TextInfo _textInfo;
    private TMP_CharacterInfo[] _characterInfo;
    private Mesh _mesh;
    private Color32[] _vertexColors;

    private CanvasRenderer _canvasRenderer;

    private float _currentTime;

    private int _lastIndex;

    void Start()
    {
        _textMesh = GetComponent<TextMeshPro>();

        if (_textMesh == null)
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _canvasRenderer = GetComponent<CanvasRenderer>();
        }

        _textColor = _textMesh.color;
        _textMesh.text = Text;

        _textMesh.ForceMeshUpdate();

        _textInfo = _textMesh.textInfo;
        _mesh = _textMesh.mesh;
        _vertexColors = _textInfo.meshInfo[0].colors32;
        _characterInfo = _textInfo.characterInfo;

        if (UseMaxVisibleCharacters)
        {
            _textMesh.maxVisibleCharacters = 1;
        }
        else
        {
            _textMesh.color = new Color32(_textColor.r, _textColor.g, _textColor.b, 0);
        }
    }

    void Update()
    {
        var numCharactersTotal = _textInfo.characterCount;
        var numCharactersVisible = _currentTime * CharactersPerSecond;
        var currentIndex = (int)numCharactersVisible;

        if (currentIndex >= numCharactersTotal)
        {
            if (_lastIndex != currentIndex)
            {
                if (UseMaxVisibleCharacters)
                {
                    _textMesh.maxVisibleCharacters = int.MaxValue;
                }
                else
                {
                    _textMesh.color = _textColor;
                }

                _lastIndex = currentIndex;
            }

            return;
        }

        if (_lastIndex != currentIndex)
        {
            if (UseMaxVisibleCharacters)
            {
                _textMesh.maxVisibleCharacters = currentIndex + 1;
                _textMesh.color = _textColor;
            }
            else
            {
                for (var i = _lastIndex; i < currentIndex; i++)
                {
                    var c = _characterInfo[i].color;

                    SetCharacterColor(i, new Color32(c.r, c.g, c.b, _textColor.a));
                }
            }

            _lastIndex = currentIndex;
        }

        if (_characterInfo[currentIndex].isVisible)
        {
            var t = numCharactersVisible - currentIndex;
            var c = _characterInfo[currentIndex].color;
            c = new Color32(c.r, c.g, c.b, (byte)(_textColor.a * t));

            SetCharacterColor(currentIndex, c);
        }

        _mesh.colors32 = _vertexColors;

        if (_canvasRenderer != null)
        {
            _canvasRenderer.SetMesh(_mesh);
        }

        _currentTime += Time.deltaTime;
    }

    private void SetCharacterColor(int index, Color32 color)
    {
        var vi = _characterInfo[index].vertexIndex;

        _vertexColors[vi + 0] = color;
        _vertexColors[vi + 1] = color;
        _vertexColors[vi + 2] = color;
        _vertexColors[vi + 3] = color;
    }
}
