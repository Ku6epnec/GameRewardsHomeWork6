using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DailyRewardController
{
    #region Fields

    public DailyRewardView _dailyRewardView;
    private List<ConteinerSlotView> _slots = new List<ConteinerSlotView>();

    private bool _isGetReward;

    #endregion

    #region OtherMethods

    public DailyRewardController(DailyRewardView dailyRewardView)
    {
        _dailyRewardView = dailyRewardView;
    }

    public void RefreshView()
    {
        InitSlot();

        _dailyRewardView.StartCoroutine(RewardsStateUpdater());
        RefreshUi();

        SubscribeButton();
    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            RefreshRewardsState();
            yield return new WaitForSeconds(1);
        }
    }
    private void RefreshRewardsState()
    {
        _isGetReward = true;

        if (_dailyRewardView.TimerGetReward.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _dailyRewardView.TimerGetReward.Value;

            if (timeSpan.Seconds > _dailyRewardView.TimeDeadLine)
            {
                _dailyRewardView.TimerGetReward = null;
                _dailyRewardView.CurrentActiveSlot = 0;
            }
            else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
            {
                _isGetReward = false;
            }
        }

        RefreshUi();
    }

    private void RefreshUi()
    {
        _dailyRewardView.GetRewardButton.interactable = _isGetReward;

        if (_isGetReward)
        {
            _dailyRewardView.TimerNewReward.text = "The reward is received today";
        }
        else
        {
            if (_dailyRewardView.TimerGetReward != null)
            {
                var nextClaimTime = _dailyRewardView.TimerGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
            }
        }

        for (var i = 0; i < _slots.Count; i++)
            _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentActiveSlot);
    }

    public void InitSlot()
    {
        for (var j = 0; j < _dailyRewardView.Rewards.Count; j++)
        {
            var instanceSlot = UnityEngine.Object.Instantiate(_dailyRewardView.ConteinerSlotView, _dailyRewardView.MountRootSlotsReward, false);
            _slots.Add(instanceSlot);
        }
    }

    public void SubscribeButton()
    {
        _dailyRewardView.GetRewardButton.onClick.AddListener(GetReward);
        _dailyRewardView.ResetTimerButton.onClick.AddListener(ResetTimer);
    }

    private void GetReward()
    {
        if (!_isGetReward)
            return;

        var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentActiveSlot];

        switch (reward._rewardType)
        {
            case RewardType.Diamond:
                CurrencyWindow.Instance.AddDiamond(reward._countCurrency);
                break;
            case RewardType.Gold:
                CurrencyWindow.Instance.AddGold(reward._countCurrency);
                break;
        }

        _dailyRewardView.TimerGetReward = DateTime.UtcNow;
        _dailyRewardView.CurrentActiveSlot = (_dailyRewardView.CurrentActiveSlot + 1) % _dailyRewardView.Rewards.Count;

        RefreshRewardsState();

    }
    private void ResetTimer()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion
}
