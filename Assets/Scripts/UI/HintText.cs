using TMPro;
using UnityEngine;

namespace NinjaJump.UI
{
    public class HintText : MonoBehaviour, IHintText
    {
        [SerializeField] private TextMeshProUGUI _textField;

        public void Show(string value) => _textField.SetText(value);

        public void Hide() => _textField.SetText(string.Empty);
    }
}