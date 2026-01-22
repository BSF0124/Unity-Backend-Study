using BackEnd;
using UnityEngine;

public class DailyRankRegister : MonoBehaviour
{
    public void Process(int newScore)
    {
        //UpdateMyRankData(newScore);
        UpdateMyBestRankData(newScore);
    }

    private void UpdateMyRankData(int newScore)
    {
        string rowInDate = string.Empty;

        // 랭킹 데이터를 업데이트 하려면 게임 데이터에서 사용하는 데이터의 inDate 값이 필요
        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            if(!callback.IsSuccess())
            {
                Debug.LogError($"데이터 조회 중 문제가 발생했습니다. : {callback}");
                return;
            }

            Debug.Log($"데이터 조회에 성공했습니다. : {callback}");

            if(callback.FlattenRows().Count > 0)
            {
                rowInDate = callback.FlattenRows()[0]["inDate"].ToString();
            }
            else
            {
                Debug.LogError("데이터가 존재하지 않습니다.");
                return;
            }

            Param param = new Param()
            {
                {"dailyBestScore", newScore}
            };

            Backend.URank.User.UpdateUserScore(Constants.DAILY_RANK_UUID, Constants.USER_DATA_TABLE, rowInDate, param, callback =>
            {
                if(callback.IsSuccess())
                {
                    Debug.Log($"랭킹 등록에 성공했습니다. {callback}");
                }
                else
                {
                    Debug.LogError($"랭킹 등록에 실패했습니다. {callback}");
                }
            });
        });
    }

    private void UpdateMyBestRankData(int newScore)
    {
        Backend.URank.User.GetMyRank(Constants.DAILY_RANK_UUID, callback =>
        {
            if(callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData rankDataJson = callback.FlattenRows();

                    if(rankDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        int bestScore = int.Parse(rankDataJson[0]["score"].ToString());

                        if(newScore > bestScore)
                        {
                            UpdateMyRankData(newScore);
                            Debug.Log($"최고 점수 갱신 {bestScore} -> {newScore}");
                        }
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                if(callback.GetMessage().Contains("userRank"))
                {
                    UpdateMyRankData(newScore);
                    Debug.Log($"새로운 랭킹 데이터 생성 및 등록 : {callback}");
                }
            }
        });
    }
}
