using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConteinerSlotView : MonoBehaviour
{
    #region Fields

    public int _countActiveDay;

    [SerializeField] private Slider _timeSlider;

    [SerializeField]
    private Image _selectBackground;

    [SerializeField]
    private Image _iconCurrency;

    [SerializeField]
    private TMP_Text _textDay;

    [SerializeField]
    private TMP_Text _countReward;

    private float _fillSpeed = 0.5f;
    private float _targetProgress = 0;
    #endregion

    private void Awake()
    {
        _timeSlider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        ProgressBar(1 / _countActiveDay);
        if (_timeSlider.value < _targetProgress)
            _timeSlider.value += _fillSpeed * Time.deltaTime;
    }
    public void ProgressBar(float _newProgress)
    {
        _timeSlider.value += _newProgress;
    }

    public void SetData(Reward reward, int countDay, bool isSelected)
    {
        _iconCurrency.sprite = reward._iconCurrency;
        _textDay.text = $"Day {countDay}";
        _countReward.text = reward._countCurrency.ToString();

        _selectBackground.gameObject.SetActive(isSelected);
    }
}
