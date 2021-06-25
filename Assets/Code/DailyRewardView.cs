using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class DailyRewardView : MonoBehaviour
{
    #region Fields

    private const string CountSlotInActiveKey = nameof(CountSlotInActiveKey);
    private const string TimeGetRewardKey = nameof(TimeGetRewardKey);
    
    [SerializeField] private TMP_Text _timerNewReward;
    [SerializeField] private Button _getRewardButton;
    [SerializeField] private Button _resetTimerButton;

    [SerializeField] private Transform _mountRootSlotsReward;
    [SerializeField] private List<Reward> _rewards;
    [SerializeField] private ConteinerSlotView _conteinerSlotView;

    [SerializeField] private float _timeCooldawn = 86400f;
    [SerializeField] private float _timeDeadLine = 172800f;

    #endregion

    #region EasyProperties

    public float TimeCooldown => _timeCooldawn;
    public float TimeDeadLine => _timeDeadLine;
    public List<Reward> Rewards => _rewards;
    public Transform MountRootSlotsReward => _mountRootSlotsReward;
    public TMP_Text TimerNewReward => _timerNewReward;
    public Button GetRewardButton => _getRewardButton;
    public Button ResetTimerButton => _resetTimerButton;
    public ConteinerSlotView ConteinerSlotView => _conteinerSlotView;

    #endregion

    #region HardProperties

    public int CurrentActiveSlot
    {
        get => PlayerPrefs.GetInt(CountSlotInActiveKey, 0);
        set => PlayerPrefs.SetInt(CountSlotInActiveKey, value);
    }    

    public DateTime? TimerGetReward
    {
        get
        {
            var data = PlayerPrefs.GetString(TimeGetRewardKey, null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }

        set
        {
            if (value != null)
            {
                PlayerPrefs.GetString(TimeGetRewardKey, value.ToString());
            }
            else
            {
                PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }

    #endregion

    private void OnDestroy()
    {
        _getRewardButton.onClick.RemoveAllListeners();
        _resetTimerButton.onClick.RemoveAllListeners();
    }
}
