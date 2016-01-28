using UnityEngine;
using System.Collections;
using TapjoyUnity;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
	public TJPlacement directPlayPlacement;
	public TJPlacement offerwallPlacement;
	public Text txtCurrency;

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnEnable ()
	{
		// Placement Delegates
		TJPlacement.OnRequestSuccess += HandlePlacementRequestSuccess;
		TJPlacement.OnRequestFailure += HandlePlacementRequestFailure;
		TJPlacement.OnContentReady += HandlePlacementContentReady;
		TJPlacement.OnContentShow += HandlePlacementContentShow;
		TJPlacement.OnContentDismiss += HandlePlacementContentDismiss;
		TJPlacement.OnPurchaseRequest += HandleOnPurchaseRequest;
		TJPlacement.OnRewardRequest += HandleOnRewardRequest;
		
		// Currency Delegates
		Tapjoy.OnAwardCurrencyResponse += HandleAwardCurrencyResponse;
		Tapjoy.OnAwardCurrencyResponseFailure += HandleAwardCurrencyResponseFailure;
		Tapjoy.OnSpendCurrencyResponse += HandleSpendCurrencyResponse;
		Tapjoy.OnSpendCurrencyResponseFailure += HandleSpendCurrencyResponseFailure;
		Tapjoy.OnGetCurrencyBalanceResponse += HandleGetCurrencyBalanceResponse;
		Tapjoy.OnGetCurrencyBalanceResponseFailure += HandleGetCurrencyBalanceResponseFailure;
		Tapjoy.OnEarnedCurrency += HandleEarnedCurrency;
		
		// Tapjoy Video Delegates
		Tapjoy.OnVideoStart += HandleVideoStart;
		Tapjoy.OnVideoError += HandleVideoError;
		Tapjoy.OnVideoComplete += HandleVideoComplete;

		Tapjoy.GetCurrencyBalance ();
	}

	void OnDisable ()
	{
		// Placement delegates
		TJPlacement.OnRequestSuccess -= HandlePlacementRequestSuccess;
		TJPlacement.OnRequestFailure -= HandlePlacementRequestFailure;
		TJPlacement.OnContentReady -= HandlePlacementContentReady;
		TJPlacement.OnContentShow -= HandlePlacementContentShow;
		TJPlacement.OnContentDismiss -= HandlePlacementContentDismiss;
		TJPlacement.OnPurchaseRequest -= HandleOnPurchaseRequest;
		TJPlacement.OnRewardRequest -= HandleOnRewardRequest;
		
		// Currency Delegates
		Tapjoy.OnAwardCurrencyResponse -= HandleAwardCurrencyResponse;
		Tapjoy.OnAwardCurrencyResponseFailure -= HandleAwardCurrencyResponseFailure;
		Tapjoy.OnSpendCurrencyResponse -= HandleSpendCurrencyResponse;
		Tapjoy.OnSpendCurrencyResponseFailure -= HandleSpendCurrencyResponseFailure;
		Tapjoy.OnGetCurrencyBalanceResponse -= HandleGetCurrencyBalanceResponse;
		Tapjoy.OnGetCurrencyBalanceResponseFailure -= HandleGetCurrencyBalanceResponseFailure;
		Tapjoy.OnEarnedCurrency -= HandleEarnedCurrency;
		
		// Tapjoy Video Delegates
		Tapjoy.OnVideoStart -= HandleVideoStart;
		Tapjoy.OnVideoError -= HandleVideoError;
		Tapjoy.OnVideoComplete -= HandleVideoComplete;
	}


	#region PUBLIC_METHODS

	public void OnCreateOfferwallTapped()
	{
		// Create offerwall placement
		if (offerwallPlacement == null) {
			offerwallPlacement = TJPlacement.CreatePlacement ("offerwall_unit");
		}
	}

	public void OnShowOfferwallTapped()
	{
		// Show offerwall placement
		if (offerwallPlacement != null) {
			offerwallPlacement.RequestContent ();
		}
	}

	public void OnCreateDirectPlayTapped()
	{
		// Create direct play placement
		if (directPlayPlacement == null) {
			directPlayPlacement = TJPlacement.CreatePlacement ("video_unit");
			if (directPlayPlacement != null) {
				directPlayPlacement.RequestContent ();
			}
		}
	}

	public void OnShowDirectPlayTapped()
	{
		// Show direct play placement
		if (directPlayPlacement.IsContentAvailable ()) {
			if (directPlayPlacement.IsContentReady ()) {
				directPlayPlacement.ShowContent ();
			} else {
				Debug.Log("Direct play video not ready to show.");
			}
		} else {
			Debug.Log ("No direct play video to show.");
		}
	}

	public void OnSpendCurrencyTapped()
	{
		// Spend currency
		Tapjoy.SpendCurrency (10);
	}

	public void OnAwardCurrencyTapped()
	{
		// Award currency
		Tapjoy.AwardCurrency (10);
	}

	public void OnGetCurrencyTapped()
	{
		// Get currency
		Tapjoy.GetCurrencyBalance ();
	}


	#endregion

	#region Tapjoy Delegate Handlers
	
	#region Placement Delegate Handlers
	public void HandlePlacementRequestSuccess (TJPlacement placement)
	{
		if (placement.IsContentAvailable ()) {
			Debug.Log ("C#: Content available for " + placement.GetName ());
		} else {
			Debug.Log ("C#: No content available for " + placement.GetName ());
		}
	}
	
	public void HandlePlacementRequestFailure (TJPlacement placement, string error)
	{
		Debug.Log ("C#: HandlePlacementRequestFailure");
		Debug.Log ("C#: Request for " + placement.GetName () + " has failed because: " + error);
	}
	
	public void HandlePlacementContentReady (TJPlacement placement)
	{
		Debug.Log ("C#: HandlePlacementContentReady");
		if (placement.IsContentAvailable ()) {
			//placement.ShowContent();
		} else {
			Debug.Log ("C#: no content");
		}
	}
	
	public void HandlePlacementContentShow (TJPlacement placement)
	{
		Debug.Log ("C#: HandlePlacementContentShow");
	}
	
	public void HandlePlacementContentDismiss (TJPlacement placement)
	{
		Debug.Log ("C#: HandlePlacementContentDismiss");
	}
	
	void HandleOnPurchaseRequest (TJPlacement placement, TJActionRequest request, string productId)
	{
		Debug.Log ("C#: HandleOnPurchaseRequest");
		request.Completed ();
	}
	
	void HandleOnRewardRequest (TJPlacement placement, TJActionRequest request, string itemId, int quantity)
	{
		Debug.Log ("C#: HandleOnRewardRequest");
		request.Completed ();
	}
	
	#endregion
	
	#region Currency Delegate Handlers
	public void HandleAwardCurrencyResponse (string currencyName, int balance)
	{
		Debug.Log ("C#: HandleAwardCurrencySucceeded: currencyName: " + currencyName + ", balance: " + balance);
		txtCurrency.text = "Currency  : " + balance;
	}
	
	public void HandleAwardCurrencyResponseFailure (string error)
	{
		Debug.Log ("C#: HandleAwardCurrencyResponseFailure: " + error);
	}
	
	public void HandleGetCurrencyBalanceResponse (string currencyName, int balance)
	{
		Debug.Log ("C#: HandleGetCurrencyBalanceResponse: currencyName: " + currencyName + ", balance: " + balance);
		txtCurrency.text = "Currency  : " + balance;
	}
	
	public void HandleGetCurrencyBalanceResponseFailure (string error)
	{
		Debug.Log ("C#: HandleGetCurrencyBalanceResponseFailure: " + error);
	}
	
	public void HandleSpendCurrencyResponse (string currencyName, int balance)
	{
		Debug.Log ("C#: HandleSpendCurrencyResponse: currencyName: " + currencyName + ", balance: " + balance);
		txtCurrency.text = "Currency  : " + balance;
	}
	
	public void HandleSpendCurrencyResponseFailure (string error)
	{
		Debug.Log ("C#: HandleSpendCurrencyResponseFailure: " + error);
	}
	
	public void HandleEarnedCurrency (string currencyName, int amount)
	{
		Debug.Log ("C#: HandleEarnedCurrency: currencyName: " + currencyName + ", amount: " + amount);
		
		Tapjoy.ShowDefaultEarnedCurrencyAlert ();
	}
	#endregion
	
	#region Video Delegate Handlers
	public void HandleVideoStart ()
	{
		Debug.Log ("C#: HandleVideoStarted");
	}
	
	public void HandleVideoError (string status)
	{
		Debug.Log ("C#: HandleVideoError, status: " + status);
	}
	
	public void HandleVideoComplete ()
	{
		Debug.Log ("C#: HandleVideoComplete");
	}
	#endregion
	#endregion
}