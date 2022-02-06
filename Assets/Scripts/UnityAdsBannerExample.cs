using UnityEngine;
using UnityEngine.Advertisements;
/// <summary>
/// Unity Ads SDK v4.x Banner Example 
/// シンプルにUnity Adsのバナーだけを表示するサンプル (Unity Ads SDK 4.0対応)
/// Created by Yasuyuki Kamata (yasuyuki@unity3d.com)
/// 
/// Unity Adsでバナー広告を表示するために必要なステップは次の3つだけ
///  1. 初期化する
///  2. 広告をロードする
///  3. 広告を表示する
///
/// 動画広告とはクラスが違うので注意！（Advertisement.Banner）
/// </summary>
public class UnityAdsBannerExample : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Game ID/ゲームID")]
    [SerializeField, Tooltip("Android用のGame ID")] private string gameIdGooglePlay = "4582757";
    [SerializeField, Tooltip("iOS用のGame ID")] private string gameIdAppleAppStore = "4582756";

    [Header("Test Mode/テストモード")]
    [SerializeField, Tooltip("テストモードのON/OFF")] private bool isTestMode;
    
    [Header("Ad Unit ID/広告ユニットID")]
    // Unity Adsのバナー広告はバナー専用のAd Unit（広告ユニット）を作成する必要がある
    // デフォルトで用意されているバナー広告ユニットIDは次のとおり
    //  - Android用のバナー広告ユニットID: Banner_Android
    //  - iOS用のバナー広告ユニットID: Banner_iOS
    [SerializeField, Tooltip("Android用のバナー広告ユニットID")] private string bannerAdUnitIdGooglePlay = "Banner_Android";
    [SerializeField, Tooltip("iOS用のバナー広告ユニットID")] private string bannerAdUnitIdAppleAppStore = "Banner_iOS";

    [Header("Banner position/バナーの配置")]
    [SerializeField, Tooltip("バナー広告の初期表示位置")] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    
    [Header("Advanced Option/追加オプション")]
    [SerializeField, Tooltip("自動的に表示を行うかどうかのオプション")] private bool isAutoLoadAndShow;
    
    private string _gameId;
    private string _bannerAdUnitId;
    
    private void Start()
    {
        InitUnityAds();
    }

    /// <summary>
    /// Unity Ads SDKの初期化 (STEP 1)
    /// </summary>
    public void InitUnityAds()
    {
        if (!Advertisement.isSupported) return;
#if UNITY_ANDROID
        _gameId = gameIdGooglePlay;
        _bannerAdUnitId = bannerAdUnitIdGooglePlay;
#elif UNITY_IOS
        _gameId = gameIdAppleAppStore;
        _bannerAdUnitId = bannerAdUnitIdAppleAppStore;
#endif
        Advertisement.Initialize(_gameId, isTestMode, this);
        Advertisement.Banner.SetPosition(bannerPosition);
    }

    /// <summary>
    /// バナーをロードする（STEP 2）
    /// </summary>
    public void LoadBanner()
    {
        if (Advertisement.Banner.isLoaded) return;
        var options = new BannerLoadOptions()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadError
        };
        Advertisement.Banner.Load(_bannerAdUnitId, options);
    }

    /// <summary>
    /// バナーを表示する（STEP 3）
    /// </summary>
    public void ShowBanner()
    {
        var options = new BannerOptions()
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
        Advertisement.Banner.Show(_bannerAdUnitId, options);
    }

    /// <summary>
    /// バナーを非表示にする
    /// </summary>
    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    /// <summary>
    /// 初期化が完了したら呼ばれる
    /// </summary>
    public void OnInitializationComplete()
    {
        if (isAutoLoadAndShow) LoadBanner();
    }

    /// <summary>
    /// 初期化が失敗したら呼ばれる
    /// </summary>
    /// <param name="error">エラーの種類</param>
    /// <param name="message">エラーメッセージ</param>
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }

    /// <summary>
    /// バナーのロードが完了したら呼ばれる
    /// </summary>
    private void OnBannerLoaded()
    {
        if (isAutoLoadAndShow) ShowBanner();
    }

    /// <summary>
    /// バナーのロードに失敗したら呼ばれる
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    private void OnBannerLoadError(string message)
    {
    }
    
    /// <summary>
    /// バナーをクリックしたら呼ばれる
    /// </summary>
    private void OnBannerClicked()
    {
    }

    /// <summary>
    /// バナーを非表示にしたとき呼ばれる
    /// </summary>
    private void OnBannerHidden()
    {
    }

    /// <summary>
    /// バナーを表示したとき呼ばれる
    /// </summary>
    private void OnBannerShown()
    {
    }
}
