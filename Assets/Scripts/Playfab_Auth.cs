using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class Playfab_Auth : MonoBehaviour
{
    public MP_Manager mp;
    LoginWithPlayFabRequest loginRequest;

    public InputField User_L;
    public InputField Pass_L;
    public InputField Email_R;
    public InputField User_R;
    public InputField Pass_R;
    public bool isAuthenticated;
    public Text message;
    //public GameObject LoginUI;
    // Start is called before the first frame update
    void Start()
    {
        Email_R.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        loginRequest = new LoginWithPlayFabRequest();
        loginRequest.Username = User_L.text;
        loginRequest.Password = Pass_L.text;
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result => {
            //If account is found
            message.text = "Welcome!" + User_L.text + ",  Connecting..." ;
            isAuthenticated = true;
            mp.connectToMaster();
            Debug.Log("You are now logged in!!");
        } , error => {
            //If account is not found
            message.text = "Failed to Login[" + error.ErrorMessage + "]";
            isAuthenticated = false;
            Debug.Log(error.ErrorMessage);
        }, null);
    }
    public void Register()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email =  Email_R.text;
        request.Username = User_R.text;
        request.Password = Pass_R.text;
        PlayFabClientAPI.RegisterPlayFabUser(request, result => {
            message.text = "Your account has been created!" ;
        }, error => {
            Email_R.gameObject.SetActive(true);
            message.text = "Please enter your Email ID:"; 
        });
    } 
}