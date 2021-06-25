using UnityEngine;

public class RootView : MonoBehaviour
{
    [SerializeField] private DailyRewardView _dailyRewardView;

    private void Awake()
    {
        var _dailyRewardController = new DailyRewardController(_dailyRewardView);
        _dailyRewardController.RefreshView();
    }
}
