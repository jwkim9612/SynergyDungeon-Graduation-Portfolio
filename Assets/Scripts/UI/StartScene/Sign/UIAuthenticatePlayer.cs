using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using GameSparks.Api.Requests;

public class UIAuthenticatePlayer : MonoBehaviour
{
    [SerializeField] private Text currUserDisplayName;
    [SerializeField] private Text authState;

    //public void GoogleLoginBtnOnClick()
    //{
    //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    //        // 서버 인증 코드가 생성되도록 요청하여
    //        // 연결된 게임스파크 서버 응용 프로그램과 토큰을 교환합니다.
    //        .RequestServerAuthCode(false)

    //        // 해당 유저의 이메일 주소를 요청합니다.
    //        // 이것이 추가되면 어플리케이션이 처음 실행될 때 동의창이 열립니다.
    //        .RequestEmail()
    //        .Build();

    //    // 초기화
    //    PlayGamesPlatform.InitializeInstance(config);

    //    // 앱 활성화
    //    PlayGamesPlatform.Activate();

    //    // 로그인 요청
    //    Social.localUser.Authenticate(success =>
    //    {
    //        Debug.Log("Issuccess: " + success);
    //        if (success == false)
    //        {
    //            return;
    //        }

    //        // 로그인이 성공되었습니다.
    //        Debug.Log("GetServerAuthCode - " + PlayGamesPlatform.Instance.GetServerAuthCode());
    //        Debug.Log("GetIdToken - " + PlayGamesPlatform.Instance.GetIdToken());
    //        Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
    //        Debug.Log("GoogleId - " + Social.localUser.id);
    //        Debug.Log("UserName - " + Social.localUser.userName);
    //        Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());

    //        // 구글 로그인으로 획득한 토큰을 가지고 
    //        // 게임스파크에 연결합니다.
    //        GameSparkGoogleLogin();
    //    });

    //}

    //void GameSparkGoogleLogin()
    //{
    //    // 유저 이름
    //    string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();

    //    // 서버 토큰
    //    string AuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();


    //    new GooglePlayConnectRequest()
    //        // 아래 링크를 통해서 우리가 게임스파크에 저장한 
    //        // 애플리케이션ID와 비밀번호 정보를 전달합니다.
    //        .SetRedirectUri("http://www.gamesparks.com/oauth2callback")
    //        // 최종 인증처리입니다.
    //        .SetCode(AuthCode)
    //        // 유저 디스플레이 이름 설정
    //        .SetDisplayName(displayName)

    //        // 아래 값들은 좀더 확실해지면 설명할게요.
    //        .SetDoNotLinkToCurrentPlayer(true)
    //        .SetSwitchIfPossible(false)
    //        .Send((googleplayAuthResponse) =>
    //        {
    //            if (!googleplayAuthResponse.HasErrors)
    //            {
    //                // 로그인 성공
    //                authState.text = "구글, 게임스파크 계정 로그인 연동 완료";
    //                currUserDisplayName.text = googleplayAuthResponse.DisplayName;
    //            }
    //            else
    //            {
    //                // 로그인 실패
    //                Debug.Log(googleplayAuthResponse.Errors.JSON);
    //                currUserDisplayName.text = "실패";
    //            }
    //        });
    //}

}

