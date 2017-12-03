using UnityEngine;
using UnityEngine.UI;

public class CatUi : MonoBehaviour
{
    public Cat Cat;

    [SerializeField] private Slider _hangingBar;

    private void Update()
    {
        var hangingCat = Cat.State as Cat.Hanging;

        _hangingBar.gameObject.SetActive(hangingCat != null);

        if (hangingCat != null) {
            _hangingBar.value = 1f - Mathf.InverseLerp(hangingCat.StartTime, hangingCat.StartTime + Cat.HangingTime, Time.time);
            _hangingBar.transform.position = Camera.main.WorldToScreenPoint(Cat.transform.position) + new Vector3(20f, 100f);
        }
    }
}
