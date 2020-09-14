using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoSingleton<GuestManager>
{
    [SerializeField] private UIEnterDisplayName uiEnterDisplayName = null;

    public void Sign(string id, string pw, bool isFromSignUp)
    {
        if (id == "")
        {
            Debug.Log("No ID");
            return;
        }

        if (pw == "")
        {
            Debug.Log("No Password");
            return;
        }

        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(id)
            .SetPassword(pw)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    AccountManager.Instance.SetAccountData(id, pw, false);
                    AccountManager.Instance.SaveAccountData();
                    Debug.Log("로그인 성공...");
                    if (isFromSignUp)
                    {
                        UIManager.Instance.ShowNew(uiEnterDisplayName);
                        // 닉네임 설정 창 오픈.
                    }
                    else
                    {
                        Debug.Log("Check Start");
                        //SaveManager.Instance.CheckHasInGameData();
                        PlayerDataManager.Instance.LoadPlayerData();
                        GameManager.instance.LoadGameAndLoadMainScene();
                    }

                    //PlayerDataManager.Instance.LoadPlayerData();
                }
                else
                {
                    AccountManager.Instance.ShowSignMain();
                    Debug.Log("로그인 실패..." + response.Errors.JSON.ToString());
                }
            });
    }

    public void SignUp()
    {
        string id = GetRandomID();
        string pw = "1";

        Debug.Log(id);
        Debug.Log(pw);
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName("Guest") // 닉네임
            .SetUserName(id) // 계정아이디
            .SetPassword(pw) // 비밀번호
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    PlayerDataManager.Instance.InitializePlayerData();
                    Sign(id, pw, true);
                    Debug.Log("회원가입 완료");
                }
                else
                {
                    Debug.Log("회원가입 실패" + response.Errors.JSON.ToString());
                    SignUp();
                }
            }
        );
    }

    public void ChangeDisplayNameAndLoadMainScene(string displayName, bool isFromSignUp)
    {
        new GameSparks.Api.Requests.ChangeUserDetailsRequest()
            .SetDisplayName(displayName)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("닉네임 변경 완료");

                    if (isFromSignUp)
                        GameManager.instance.LoadGameAndLoadMainScene();
                }
                else
                {
                    Debug.Log("닉네임 변경 실패");
                }
            });
    }

    public string GetRandomID()
    {
        Guid new_guid = Guid.NewGuid();

        string id = new_guid.GetHashCode().ToString();
        id = id.Replace('-', 'M');

        return id;
    }
}
