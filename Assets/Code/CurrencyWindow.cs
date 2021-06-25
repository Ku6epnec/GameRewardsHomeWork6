using UnityEngine;
using TMPro;


public class CurrencyWindow : MonoBehaviour
{
    #region Fields

    private const string DiamondKey = nameof(DiamondKey);
    private const string GoldKey = nameof(GoldKey);

    public static CurrencyWindow Instance;

    [SerializeField]
    private TMP_Text _currencyCountDiamond;

    [SerializeField]
    private TMP_Text _currencyCountGold;

    #endregion

    #region Properties

    private int Diamond
    {
        get => PlayerPrefs.GetInt(DiamondKey, 0);
        set => PlayerPrefs.SetInt(DiamondKey, value);
    }

    private int Gold
    {
        get => PlayerPrefs.GetInt(GoldKey, 0);
        set => PlayerPrefs.SetInt(GoldKey, value);
    }

    #endregion

    #region UnityMethods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    #endregion

    #region OtherMethods

    public void AddDiamond(int value)
    {
        Diamond += value;

        RefreshText();
    }

    public void AddGold(int value)
    {
        Gold += value;

        RefreshText();
    }

    public void RefreshText()
    {
        _currencyCountDiamond.text = Diamond.ToString();
        _currencyCountGold.text = Gold.ToString();
    }

    #endregion
}
