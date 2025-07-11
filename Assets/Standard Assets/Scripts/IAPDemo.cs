using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
/*
[AddComponentMenu("Unity IAP/Demo")]
public class IAPDemo : MonoBehaviour, IStoreListener
{
	private IStoreController m_Controller;

	private IAppleExtensions m_AppleExtensions;

	private IMoolahExtension m_MoolahExtensions;

	private ISamsungAppsExtensions m_SamsungExtensions;

	private IMicrosoftExtensions m_MicrosoftExtensions;

	private bool m_IsGooglePlayStoreSelected;

	private bool m_IsSamsungAppsStoreSelected;

	private bool m_IsCloudMoolahStoreSelected;

	private string m_LastTransationID;

	private string m_LastReceipt;

	private string m_CloudMoolahUserName;

	private bool m_IsLoggedIn;

	private int m_SelectedItemIndex = -1;

	private bool m_PurchaseInProgress;

	private Selectable m_InteractableSelectable;

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		m_Controller = controller;
		m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
		m_SamsungExtensions = extensions.GetExtension<ISamsungAppsExtensions>();
		m_MoolahExtensions = extensions.GetExtension<IMoolahExtension>();
		m_MicrosoftExtensions = extensions.GetExtension<IMicrosoftExtensions>();
		InitUI(controller.products.all);
		m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);
		UnityEngine.Debug.Log("Available items:");
		Product[] all = controller.products.all;
		foreach (Product product in all)
		{
			if (product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Join(" - ", new string[7]
				{
					product.metadata.localizedTitle,
					product.metadata.localizedDescription,
					product.metadata.isoCurrencyCode,
					product.metadata.localizedPrice.ToString(),
					product.metadata.localizedPriceString,
					product.transactionID,
					product.receipt
				}));
			}
		}
		if (m_Controller.products.all.Length > 0)
		{
			m_SelectedItemIndex = 0;
		}
		for (int j = 0; j < m_Controller.products.all.Length; j++)
		{
			Product product2 = m_Controller.products.all[j];
			string text = $"{product2.metadata.localizedTitle} | {product2.metadata.localizedPriceString} => {product2.metadata.localizedPrice}";
			GetDropdown().options[j] = new Dropdown.OptionData(text);
		}
		GetDropdown().RefreshShownValue();
		UpdateHistoryUI();
		LogProductDefinitions();
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		UnityEngine.Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
		UnityEngine.Debug.Log("Receipt: " + e.purchasedProduct.receipt);
		m_LastTransationID = e.purchasedProduct.transactionID;
		m_LastReceipt = e.purchasedProduct.receipt;
		m_PurchaseInProgress = false;
		if (m_IsCloudMoolahStoreSelected)
		{
			m_MoolahExtensions.RequestPayOut(e.purchasedProduct.transactionID, delegate(string transactionID, RequestPayOutState state, string message)
			{
				if (state == RequestPayOutState.RequestPayOutSucceed)
				{
					m_Controller.ConfirmPendingPurchase(e.purchasedProduct);
				}
				else
				{
					UnityEngine.Debug.Log("RequestPayOut: failed. transactionID: " + transactionID + ", state: " + state + ", message: " + message);
				}
			});
		}
		UpdateHistoryUI();
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
	{
		UnityEngine.Debug.Log("Purchase failed: " + item.definition.id);
		UnityEngine.Debug.Log(r);
		m_PurchaseInProgress = false;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("Billing failed to initialize!");
		switch (error)
		{
		case InitializationFailureReason.AppNotKnown:
			UnityEngine.Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
			break;
		case InitializationFailureReason.PurchasingUnavailable:
			UnityEngine.Debug.Log("Billing disabled!");
			break;
		case InitializationFailureReason.NoProductsAvailable:
			UnityEngine.Debug.Log("No products available for purchase!");
			break;
		}
	}

	public void Awake()
	{
		StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
		standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(standardPurchasingModule);
		configurationBuilder.Configure<IMicrosoftConfiguration>().useMockBillingSystem = true;
		configurationBuilder.Configure<IGooglePlayConfiguration>().SetPublicKey("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2O/9/H7jYjOsLFT/uSy3ZEk5KaNg1xx60RN7yWJaoQZ7qMeLy4hsVB3IpgMXgiYFiKELkBaUEkObiPDlCxcHnWVlhnzJBvTfeCPrYNVOOSJFZrXdotp5L0iS2NVHjnllM+HA1M0W2eSNjdYzdLmZl1bxTpXa4th+dVli9lZu7B7C2ly79i/hGTmvaClzPBNyX+Rtj7Bmo336zh2lYbRdpD5glozUq+10u91PMDPH+jqhx10eyZpiapr8dFqXl5diMiobknw9CgcjxqMTVBQHK6hS0qYKPmUDONquJn280fBs1PTeA6NMG03gb9FLESKFclcuEZtvM8ZwMMRxSLA9GwIDAQAB");
		m_IsGooglePlayStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.androidStore == AndroidStore.GooglePlay);
		configurationBuilder.Configure<IMoolahConfiguration>().appKey = "d93f4564c41d463ed3d3cd207594ee1b";
		configurationBuilder.Configure<IMoolahConfiguration>().hashKey = "cc";
		configurationBuilder.Configure<IMoolahConfiguration>().SetMode(CloudMoolahMode.AlwaysSucceed);
		m_IsCloudMoolahStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.androidStore == AndroidStore.CloudMoolah);
		configurationBuilder.AddProduct("100.gold.coins", ProductType.Consumable, new IDs
		{
			{
				"100.gold.coins.mac",
				"MacAppStore"
			},
			{
				"000000596586",
				"TizenStore"
			},
			{
				"com.ff",
				"MoolahAppStore"
			}
		});
		configurationBuilder.AddProduct("500.gold.coins", ProductType.Consumable, new IDs
		{
			{
				"500.gold.coins.mac",
				"MacAppStore"
			},
			{
				"000000596581",
				"TizenStore"
			},
			{
				"com.ee",
				"MoolahAppStore"
			}
		});
		configurationBuilder.AddProduct("sword", ProductType.NonConsumable, new IDs
		{
			{
				"sword.mac",
				"MacAppStore"
			},
			{
				"000000596583",
				"TizenStore"
			}
		});
		configurationBuilder.AddProduct("subscription", ProductType.Subscription, new IDs
		{
			{
				"subscription.mac",
				"MacAppStore"
			}
		});
		configurationBuilder.Configure<IAmazonConfiguration>().WriteSandboxJSON(configurationBuilder.products);
		configurationBuilder.Configure<ISamsungAppsConfiguration>().SetMode(SamsungAppsMode.AlwaysSucceed);
		m_IsSamsungAppsStoreSelected = (Application.platform == RuntimePlatform.Android && standardPurchasingModule.androidStore == AndroidStore.SamsungApps);
		configurationBuilder.Configure<ITizenStoreConfiguration>().SetGroupId("100000085616");
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	private void OnTransactionsRestored(bool success)
	{
		UnityEngine.Debug.Log("Transactions restored.");
	}

	private void OnDeferred(Product item)
	{
		UnityEngine.Debug.Log("Purchase deferred: " + item.definition.id);
	}

	private void InitUI(IEnumerable<Product> items)
	{
		m_InteractableSelectable = GetDropdown();
		if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.tvOS && Application.platform != RuntimePlatform.MetroPlayerX86 && Application.platform != RuntimePlatform.MetroPlayerX64 && Application.platform != RuntimePlatform.MetroPlayerARM && !m_IsSamsungAppsStoreSelected && !m_IsCloudMoolahStoreSelected)
		{
			GetRestoreButton().gameObject.SetActive(value: false);
		}
		GetRegisterButton().gameObject.SetActive(m_IsCloudMoolahStoreSelected);
		GetLoginButton().gameObject.SetActive(m_IsCloudMoolahStoreSelected);
		GetValidateButton().gameObject.SetActive(m_IsCloudMoolahStoreSelected);
		foreach (Product item in items)
		{
			string text = $"{item.definition.id} - {item.definition.type}";
			GetDropdown().options.Add(new Dropdown.OptionData(text));
		}
		GetDropdown().RefreshShownValue();
		GetDropdown().onValueChanged.AddListener(delegate(int selectedItem)
		{
			UnityEngine.Debug.Log("OnClickDropdown item " + selectedItem);
			m_SelectedItemIndex = selectedItem;
		});
		GetBuyButton().onClick.AddListener(delegate
		{
			if (m_PurchaseInProgress)
			{
				UnityEngine.Debug.Log("Please wait, purchasing ...");
			}
			else
			{
				if (m_IsCloudMoolahStoreSelected && !m_IsLoggedIn)
				{
					UnityEngine.Debug.LogWarning("CloudMoolah purchase notifications will not be forwarded server-to-server. Login incomplete.");
				}
				m_PurchaseInProgress = true;
				m_Controller.InitiatePurchase(m_Controller.products.all[m_SelectedItemIndex], "aDemoDeveloperPayload");
			}
		});
		if (GetRestoreButton() != null)
		{
			GetRestoreButton().onClick.AddListener(delegate
			{
				if (m_IsCloudMoolahStoreSelected)
				{
					if (!m_IsLoggedIn)
					{
						UnityEngine.Debug.LogError("CloudMoolah purchase restoration aborted. Login incomplete.");
					}
					else
					{
						m_MoolahExtensions.RestoreTransactionID(delegate(RestoreTransactionIDState restoreTransactionIDState)
						{
							UnityEngine.Debug.Log("restoreTransactionIDState = " + restoreTransactionIDState.ToString());
							bool success = restoreTransactionIDState != RestoreTransactionIDState.RestoreFailed && restoreTransactionIDState != RestoreTransactionIDState.NotKnown;
							OnTransactionsRestored(success);
						});
					}
				}
				else if (m_IsSamsungAppsStoreSelected)
				{
					m_SamsungExtensions.RestoreTransactions(OnTransactionsRestored);
				}
				else if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
				{
					m_MicrosoftExtensions.RestoreTransactions();
				}
				else
				{
					m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
				}
			});
		}
		if (GetRegisterButton() != null)
		{
			GetRegisterButton().onClick.AddListener(delegate
			{
				m_MoolahExtensions.FastRegister("CMPassword", RegisterSucceeded, RegisterFailed);
			});
		}
		if (GetLoginButton() != null)
		{
			GetLoginButton().onClick.AddListener(delegate
			{
				m_MoolahExtensions.Login(m_CloudMoolahUserName, "CMPassword", LoginResult);
			});
		}
		if (GetValidateButton() != null)
		{
			GetValidateButton().onClick.AddListener(delegate
			{
				m_MoolahExtensions.ValidateReceipt(m_LastTransationID, m_LastReceipt, delegate(string transactionID, ValidateReceiptState state, string message)
				{
					UnityEngine.Debug.Log("ValidtateReceipt transactionID:" + transactionID + ", state:" + state.ToString() + ", message:" + message);
				});
			});
		}
	}

	public void LoginResult(LoginResultState state, string errorMsg)
	{
		if (state == LoginResultState.LoginSucceed)
		{
			m_IsLoggedIn = true;
		}
		else
		{
			m_IsLoggedIn = false;
		}
		UnityEngine.Debug.Log("LoginResult: state: " + state.ToString() + " errorMsg: " + errorMsg);
	}

	public void RegisterSucceeded(string cmUserName)
	{
		UnityEngine.Debug.Log("RegisterSucceeded: cmUserName = " + cmUserName);
		m_CloudMoolahUserName = cmUserName;
	}

	public void RegisterFailed(FastRegisterError error, string errorMessage)
	{
		UnityEngine.Debug.Log("RegisterFailed: error = " + error.ToString() + ", errorMessage = " + errorMessage);
	}

	public void UpdateHistoryUI()
	{
		if (m_Controller != null)
		{
			string text = "Item\n\n";
			string text2 = "Purchased\n\n";
			Product[] all = m_Controller.products.all;
			foreach (Product product in all)
			{
				text = text + "\n\n" + product.definition.id;
				text2 += "\n\n";
				text2 += product.hasReceipt.ToString();
			}
			GetText(right: false).text = text;
			GetText(right: true).text = text2;
		}
	}

	protected void UpdateInteractable()
	{
		if (m_InteractableSelectable == null)
		{
			return;
		}
		bool flag = m_Controller != null;
		if (flag != m_InteractableSelectable.interactable)
		{
			if (GetRestoreButton() != null)
			{
				GetRestoreButton().interactable = flag;
			}
			GetBuyButton().interactable = flag;
			GetDropdown().interactable = flag;
			GetRegisterButton().interactable = flag;
			GetLoginButton().interactable = flag;
		}
	}

	public void Update()
	{
		UpdateInteractable();
	}

	private Dropdown GetDropdown()
	{
		return GameObject.Find("Dropdown").GetComponent<Dropdown>();
	}

	private Button GetBuyButton()
	{
		return GameObject.Find("Buy").GetComponent<Button>();
	}

	private Button GetRestoreButton()
	{
		return GetButton("Restore");
	}

	private Button GetRegisterButton()
	{
		return GetButton("Register");
	}

	private Button GetLoginButton()
	{
		return GetButton("Login");
	}

	private Button GetValidateButton()
	{
		return GetButton("Validate");
	}

	private Button GetButton(string buttonName)
	{
		GameObject gameObject = GameObject.Find(buttonName);
		if (gameObject != null)
		{
			return gameObject.GetComponent<Button>();
		}
		return null;
	}

	private Text GetText(bool right)
	{
		string name = (!right) ? "TextL" : "TextR";
		return GameObject.Find(name).GetComponent<Text>();
	}

	private void LogProductDefinitions()
	{
		Product[] all = m_Controller.products.all;
		Product[] array = all;
		foreach (Product product in array)
		{
			UnityEngine.Debug.Log(string.Format("id: {0}\nstore-specific id: {1}\ntype: {2}\nenabled: {3}\n", product.definition.id, product.definition.storeSpecificId, product.definition.type.ToString(), (!product.definition.enabled) ? "disabled" : "enabled"));
		}
	}
}
*/