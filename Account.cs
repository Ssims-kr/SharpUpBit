using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpUpBit
{
    public partial class UpBit
    {
        #region 클래스
        /// <summary>
        /// 자산 클래스
        /// </summary>
        public class Account {
            #region 프로퍼티
            /// <summary>
            /// 화폐를 의미하는 영문 대문자 코드
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// 주문 가능한 금액/수량
            /// </summary>
            public double Balance { get; set; }

            /// <summary>
            /// 주문 중 묶여있는 금액/수량
            /// </summary>
            public double Locked { get; set; }

            /// <summary>
            /// 매수 평균가
            /// </summary>
            public double AvgBuyPrice { get; set; }

            /// <summary>
            /// 매수 평균가 수정 여부
            /// </summary>
            public bool AvgBuyPriceModified { get; set; }

            /// <summary>
            /// 평단가 기준 화폐
            /// </summary>
            public string UnitCurrency { get; set; }
            #endregion
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 보유한 자산 목록을 취득합니다.
        /// </summary>
        /// <returns>자산 목록</returns>
        public async Task<List<Account>> GetMyAccount() {
            var client = new System.Net.Http.HttpClient();
            var request = new System.Net.Http.HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Get,
                RequestUri = new Uri(@"https://api.upbit.com/v1/accounts"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", this.GetJwtToken() },
                },
            };

            List<Account> lst = new List<Account>();

            using (var response = await client.SendAsync(request)) {
                var body = await response.Content.ReadAsStringAsync();

                JArray jArr = JArray.Parse(body);

                for (int i = 0; i < jArr.Count; i++) {
                    lst.Add(new Account { 
                        Currency = jArr[i]["currency"].ToString(),
                        Balance = double.Parse(jArr[i]["balance"].ToString()),
                        Locked = double.Parse(jArr[i]["locked"].ToString()),
                        AvgBuyPrice = double.Parse(jArr[i]["avg_buy_price"].ToString()),
                        AvgBuyPriceModified = bool.Parse(jArr[i]["avg_buy_price_modified"].ToString()),
                        UnitCurrency = jArr[i]["unit_currency"].ToString(),
                    });
                }
            }

            return lst;
        }
        #endregion
    }
}
