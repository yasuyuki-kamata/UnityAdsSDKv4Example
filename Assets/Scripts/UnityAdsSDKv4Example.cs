using UnityEngine;
using UnityEngine.Advertisements;
/// <summary>
/// Unity Ads SDK v4.x Example 
/// シンプルにUnity Adsだけを表示するサンプル (Unity Ads SDK 4.0対応)
/// Created by Yasuyuki Kamata (yasuyuki@unity3d.com)
/// 
/// Unity Adsで動画広告を表示するために必要なステップは次の3つだけ
///  1. 初期化する
///  2. 広告をロードする
///  3. 広告を表示する
/// </summary>
public class UnityAdsSDKv4Example : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("Game ID/ゲームID")]
    [SerializeField, Tooltip("Android用のGame ID")] private string gameIdGooglePlay = "4582757";
    [SerializeField, Tooltip("iOS用のGame ID")] private string gameIdAppleAppStore = "4582756";

    [Header("Test Mode/テストモード")]
    [SerializeField, Tooltip("テストモードのON/OFF")] private bool isTestMode;
    
    [Header("Ad Unit ID/広告ユニットID")]
    // Unity Dashboard上でUnity Adsを有効化したときにデフォルトで作成される広告ユニットは以下のとおり
    // Android用
    //  - スキップ可能な広告ユニット: Interstitial_Android
    //  - スキップできない広告ユニット: Rewarded_Android
    // iOS用
    //  - スキップ可能な広告ユニット: Interstitial_iOS
    //  - スキップできない広告ユニット: Rewarded_iOS
    [SerializeField, Tooltip("Android用の広告ユニットID")] private string adUnitIdGooglePlay = "Interstitial_Android";
    [SerializeField, Tooltip("iOS用の広告ユニットID")] private string adUnitIdAppleAppStore = "Interstitial_iOS";

    [Header("Advanced Option/追加オプション")]
    [SerializeField, Tooltip("自動的にロードを行うかどうかのオプション")] private bool isAutoLoad;
    
    private string _gameId;
    private string _adUnitId;
    
    private void Start()
    {
        InitUnityAds();
    }

    /// <summary>
    /// Unity Ads SDKの初期化 (STEP 1)
    /// </summary>
    public void InitUnityAds()
    {
#if UNITY_ANDROID
        _gameId = gameIdGooglePlay;
        _adUnitId = adUnitIdGooglePlay;
#elif UNITY_IOS
        _gameId = gameIdAppleAppStore;
        _adUnitId = adUnitIdAppleAppStore;
#endif
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, isTestMode, this);
        }
    }

    /// <summary>
    /// 動画広告をロードする (STEP 2)
    /// </summary>
    public void LoadUnityAds()
    {
        Advertisement.Load(_adUnitId, this);
    }

    /// <summary>
    /// 動画広告を表示する (STEP 3)
    /// </summary>
    public void ShowUnityAds()
    {
        Advertisement.Show(_adUnitId, this);
    }
    
    /// <summary>
    /// 初期化が完了したら呼ばれる
    /// </summary>
    public void OnInitializationComplete()
    {
        if (isAutoLoad) LoadUnityAds();
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
    /// 広告のロードが完了したら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
    }

    /// <summary>
    /// 広告のロードが失敗したら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    /// <param name="error">エラーの種類</param>
    /// <param name="message">エラーメッセージ</param>
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
    }

    /// <summary>
    /// 表示が失敗したら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    /// <param name="error">エラーの種類</param>
    /// <param name="message">エラーメッセージ</param>
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        if (isAutoLoad) LoadUnityAds();
    }

    /// <summary>
    /// 表示が開始されたら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    public void OnUnityAdsShowStart(string adUnitId)
    {
    }

    /// <summary>
    /// 広告内でクリックされたら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    public void OnUnityAdsShowClick(string adUnitId)
    {
    }

    /// <summary>
    /// 表示が完了したら呼ばれる
    /// </summary>
    /// <param name="adUnitId">広告ユニットID</param>
    /// <param name="showCompletionState">完了ステータス</param>
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (isAutoLoad) LoadUnityAds();

        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                // ユーザーが広告を最後まで見たとき
                // ここにリワード付与の処理などを書く
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                // ユーザーが広告をスキップして見たとき
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
        }
    }
}
