using System;
using UnityEngine;

public class AccountManager : MonoSingleton<AccountManager>
{
    private AccountData accountData;

    [SerializeField] private GameObject signMain = null;

    public void Initialize()
    {
        accountData = new AccountData();
        AccountData loadedAccoutnData = JsonDataManager.Instance.LoadJsonFile<AccountData>(Application.persistentDataPath, "AccountData");
        if (loadedAccoutnData == null)
        {
            ShowSignMain();
            // 로그인 창, 회원가입 창 띄우기

        }
        else
        {
            accountData = loadedAccoutnData;
            if (accountData.isLoginToGoogle)
            {
                GoogleManager.Instance.GoogleLogin();
            }
            else
            {
                GuestManager.Instance.Sign(accountData.id, accountData.pw, false);
            }
            // 로그인 완료.
        }
    }

    public void SaveAccountData()
    {
        JsonDataManager.Instance.CreateJsonFile(Application.persistentDataPath, "AccountData", JsonDataManager.Instance.ObjectToJson(accountData));

        Debug.Log("계정 정보 저장 완료!");
        Debug.Log($"ID : {accountData.id}, PW : {accountData.pw}");
    }

    public void SetAccountData(string id, string pw, bool LoginToGoole)
    {
        accountData.id = id;
        accountData.pw = pw;
        accountData.isLoginToGoogle = LoginToGoole;
    }

    public void ShowSignMain()
    {
        signMain.SetActive(true);
        UIManager.Instance.SetCanEscape(true);
    }
}
