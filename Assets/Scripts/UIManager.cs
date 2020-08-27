using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;
public class UIManager : MonoBehaviour 
{
	// Singleton
	private static UIManager instance; 
	public static UIManager Instance() { return instance;}
	void Awake () { instance = this; }

	public Text snackbarText;
	public Button hostButton;
	public Button joinButton;
	//public Button spectateButton;
	public Button endGameButton;
	public Text roomText;
	public Text ipAddressText;
	//public GameObject joinInputRoot;
	public InputField roomInputField;
	public InputField ipAddressInputField;
	public GameObject authenticationPanel;

	void Start ()
	{
		authenticationPanel.SetActive(true);
		string hostName = System.Net.Dns.GetHostName();
		ipAddressText.text = LocalIPAddress();	
	}

	public static string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "0.0.0.0";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

	public void SetSnackbarText (string _text) 
	{
		snackbarText.text = _text;
	}

	public void ShowAuthenticationPanel (bool _isActive)
	{
		authenticationPanel.SetActive(_isActive);
	}

	public void ResetMatchmakingUI ()
	{
		hostButton.gameObject.SetActive(true);
		joinButton.gameObject.SetActive(true);
		snackbarText.gameObject.SetActive(true);
		hostButton.GetComponentInChildren<Text>().text = "Host";
		hostButton.interactable = true;
		joinButton.GetComponentInChildren<Text>().text = "Join";
		joinButton.interactable = true;
		snackbarText.text = "Please select Host or Join to continue";
		//joinInputRoot.SetActive(false);
		//spectateButton.gameObject.SetActive(false);
		endGameButton.gameObject.SetActive(false);
	}

	public void ResetGameUI () 
	{
		hostButton.gameObject.SetActive(false);
		joinButton.gameObject.SetActive(false);
		snackbarText.gameObject.SetActive(false);
		//spectateButton.gameObject.SetActive(true);
		endGameButton.gameObject.SetActive(true);
	}

	/// <summary>
	/// Shows UI for the beginning phase of application "Hosting Mode".
	/// </summary>
	/// <param name="snackbarText">Optional text to put in the snackbar.</param>
	public void ShowHostingModeBegin(string snackbarText = null)
	{
		hostButton.GetComponentInChildren<Text>().text = "Cancel";
		hostButton.interactable = true;
		joinButton.GetComponentInChildren<Text>().text = "Join";
		joinButton.interactable = false;

		if (string.IsNullOrEmpty(snackbarText))
		{
			this.snackbarText.text =
				"The room code is now available. Please place an anchor to host, press Cancel to Exit.";
		}
		else
		{
			this.snackbarText.text = snackbarText;
		}

		//joinInputRoot.SetActive(false);
	}

	/// <summary>
	/// Shows UI for the attempting to host phase of application "Hosting Mode".
	/// </summary>
	public void ShowHostingModeAttemptingHost()
	{
		hostButton.GetComponentInChildren<Text>().text = "Cancel";
		hostButton.interactable = false;
		joinButton.GetComponentInChildren<Text>().text = "Join";
		joinButton.interactable = false;
		snackbarText.text = "Attempting to host anchor...";
		//joinInputRoot.SetActive(false);
	}

	/// <summary>
	/// Shows UI for the beginning phase of application "Resolving Mode".
	/// </summary>
	/// <param name="snackbarText">Optional text to put in the snackbar.</param>
	public void ShowResolvingModeBegin(string snackbarText = null)
	{
		hostButton.GetComponentInChildren<Text>().text = "Host";
		hostButton.interactable = false;
		joinButton.GetComponentInChildren<Text>().text = "Cancel";
		joinButton.interactable = true;
		
		roomText.gameObject.SetActive(false);
		ipAddressText.gameObject.SetActive(false);

		if (string.IsNullOrEmpty(snackbarText))
		{
			this.snackbarText.text = "Input Room ID to resolve anchor.";
		}
		else
		{
			this.snackbarText.text = snackbarText;
		}

		//joinInputRoot.SetActive(true);
	}

	/// <summary>
	/// Shows UI for the attempting to resolve phase of application "Resolving Mode".
	/// </summary>
	public void ShowResolvingModeAttemptingResolve()
	{
		hostButton.GetComponentInChildren<Text>().text = "Host";
		hostButton.interactable = false;
		joinButton.GetComponentInChildren<Text>().text = "Cancel";
		joinButton.interactable = false;
		snackbarText.text = "Attempting to resolve anchor.";
		//joinInputRoot.SetActive(false);
	}

	/// <summary>
	/// Shows UI for the successful resolve phase of application "Resolving Mode".
	/// </summary>
	public void ShowResolvingModeSuccess()
	{
		hostButton.GetComponentInChildren<Text>().text = "Host";
		hostButton.interactable = false;
		joinButton.GetComponentInChildren<Text>().text = "Cancel";
		joinButton.interactable = true;
		snackbarText.text = "The anchor was successfully resolved.";
		//GameManager.Instance().OnHostedandResolved();
		//joinInputRoot.SetActive(false);
	}

	/// <summary>
	/// Sets the room number in the UI.
	/// </summary>
	/// <param name="roomNumber">The room number to set.</param>
	public void SetRoomTextValue(int roomNumber)
	{
		roomText.text = "Room: " + roomNumber;
	}

	/// <summary>
	/// Gets the value of the room number input field.
	/// </summary>
	/// <returns>The value of the room number input field.</returns>
	public int GetRoomInputValue()
	{
		int roomNumber;
		if (int.TryParse(roomInputField.text, out roomNumber))
		{
			return roomNumber;
		}

		return 0;
	}

	/// <summary>
	/// Gets the value of the ip address input field.
	/// </summary>
	/// <returns>The value of the ip address input field.</returns>
	public string GetIpAddressInputValue()
	{
		return ipAddressInputField.text;
	}
}