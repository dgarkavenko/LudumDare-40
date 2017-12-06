using UnityEngine;
using UnityEngine.UI;

public class CatUi : MonoBehaviour
{
    public Cat Cat;

    [SerializeField] private Image _hangingBar;
    [SerializeField] private Color _startingColor;
    [SerializeField] private Color _endingColor;
    [SerializeField] private Text _text;
    
    private void Start()
    {
    }
    
    private void LateUpdate()
    {
        if (Cat == null) return;

        var hangingCat = Cat.State as Cat.Hanging;

        _hangingBar.transform.parent.gameObject.SetActive(hangingCat != null);

        if (hangingCat != null)
        {
            _text.text = hangingCat.Word;
            _hangingBar.fillAmount = 1f - Mathf.InverseLerp(hangingCat.StartTime, hangingCat.StartTime + Cat.HangingTime, Time.time);
            _hangingBar.transform.parent.position = Camera.main.WorldToScreenPoint(Cat.transform.position);
            _hangingBar.color = Color.Lerp(_endingColor, _startingColor, _hangingBar.fillAmount);
        }
    }
}
