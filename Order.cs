using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpUpBit
{
    public partial class UpBit {
        #region 클래스
        /// <summary>
        /// 주문 클래스
        /// </summary>
        public class Order {
            #region 클래스
            /// <summary>
            /// 마켓별 주문 가능 정보
            /// </summary>
            public class OrderChance {
                #region 클래스
                /// <summary>
                /// 마켓에 대한 정보
                /// </summary>
                public class Market {
                    #region 클래스
                    /// <summary>
                    /// 제약 사항
                    /// </summary>
                    public class Constraint
                    {
                        #region 프로퍼티
                        /// <summary>
                        /// 화폐를 의미하는 영문 대문자 코드
                        /// </summary>
                        [JsonProperty(PropertyName="currency")]
                        public string Currency { get; set; }

                        ///// <summary>
                        ///// 주문 금액 단위
                        ///// </summary>
                        //[JsonProperty(PropertyName="price_unit")]
                        //public string PriceUnit { get; set; }

                        /// <summary>
                        /// 최소 매도/매수 금액
                        /// </summary>
                        [JsonProperty(PropertyName="min_total")]
                        public double MinTotal { get; set; }
                        #endregion
                    }
                    #endregion

                    #region 프로퍼티
                    /// <summary>
                    /// 마켓의 유일 키
                    /// </summary>
                    [JsonProperty(PropertyName="market.id")]
                    public string ID { get; set; }

                    /// <summary>
                    /// 마켓 이름
                    /// </summary>
                    [JsonProperty(PropertyName="market.name")]
                    public string Name { get; set; }

                    /// <summary>
                    /// 지원 주문 방식
                    /// </summary>
                    [JsonProperty(PropertyName="market.order_types")]
                    public string[] OrderTypes { get; set; }

                    /// <summary>
                    /// 지원 주문 종류
                    /// </summary>
                    [JsonProperty(PropertyName="market.order_sides")]
                    public string[] OrderSides { get; set; }

                    /// <summary>
                    /// 매수 시 제약 사항
                    /// </summary>
                    [JsonProperty(PropertyName="market.bid")]
                    public Constraint Bid { get; set; }

                    /// <summary>
                    /// 매도 시 제약 사항
                    /// </summary>
                    [JsonProperty(PropertyName="market.ask")]
                    public Constraint Ask { get; set; }

                    /// <summary>
                    /// 최대 매도/매수 금액
                    /// </summary>
                    [JsonProperty(PropertyName="market.max_total")]
                    public double MaxTotal { get; set; }

                    /// <summary>
                    /// 마켓 운영 상태
                    /// </summary>
                    [JsonProperty(PropertyName="market.state")]
                    public string State { get; set; }
                    #endregion
                }
                #endregion

                #region 프로퍼티
                /// <summary>
                /// 매수 수수료 비율
                /// </summary>
                [JsonProperty(PropertyName="bid_fee")]
                public double BidFee { get; set; }

                /// <summary>
                /// 매도 수수료 비율
                /// </summary>
                [JsonProperty(PropertyName="ask_fee")]
                public double AskFee { get; set; }

                /// <summary>
                /// 메이커 매수 수수료 비율
                /// </summary>
                [JsonProperty(PropertyName="market_bid_fee")]
                public double MakerBidFee { get; set; }

                /// <summary>
                /// 메이커 매도 수수료 비율
                /// </summary>
                [JsonProperty(PropertyName="market_ask_fee")]
                public double MakerAskFee { get; set; }

                /// <summary>
                /// 마켓에 대한 정보
                /// </summary>
                [JsonProperty(PropertyName="market")]
                public Market MarketInfo { get; set; }

                /// <summary>
                /// 매수 시 사용하는 화폐의 계좌 상태
                /// </summary>
                [JsonProperty(PropertyName="bid_account")]
                public Account BidAccount { get; set; }

                /// <summary>
                /// 매도 시 사용하는 화폐의 계좌 상태
                /// </summary>
                [JsonProperty(PropertyName="ask_account")]
                public Account AskAccount { get; set; }
                #endregion
            }
            #endregion
        }
        #endregion

        #region 메서드
        public async Task<Order.OrderChance> GetOrderChance(string marketId) {
            var client = new System.Net.Http.HttpClient();
            var request = new System.Net.Http.HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Get,
                RequestUri = new Uri(@"https://api.upbit.com/v1/orders/chance?market=" + marketId),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", this.GetJwtToken(this.GetQueryString(new Dictionary<string, string>() { { "market", marketId } })) },
                },
            };

            Order.OrderChance rtn = new Order.OrderChance();

            using (var response = await client.SendAsync(request)) {
                var body = await response.Content.ReadAsStringAsync();

                JObject jObj = JObject.Parse(body);
                Console.WriteLine(jObj);

                rtn.BidFee = double.Parse(jObj["bid_fee"].ToString());
                rtn.AskFee = double.Parse(jObj["ask_fee"].ToString());
                rtn.MakerBidFee = double.Parse(jObj["maker_bid_fee"].ToString());
                rtn.MakerAskFee = double.Parse(jObj["maker_ask_fee"].ToString());
                rtn.MarketInfo = new Order.OrderChance.Market();
                rtn.MarketInfo.ID = jObj["market"]["id"].ToString();
                rtn.MarketInfo.Name = jObj["market"]["name"].ToString();
                rtn.MarketInfo.OrderTypes = jObj.SelectToken("market.order_types").ToObject<string[]>();
                rtn.MarketInfo.OrderSides = jObj.SelectToken("market.order_sides").ToObject<string[]>();
                rtn.MarketInfo.Bid = new Order.OrderChance.Market.Constraint();
                rtn.MarketInfo.Bid.Currency = jObj["market"]["bid"]["currency"].ToString();
                //rtn.MarketInfo.Bid.PriceUnit = jObj["market"]["bid"]["price_unit"].ToString();
                rtn.MarketInfo.Bid.MinTotal = double.Parse(jObj["market"]["bid"]["min_total"].ToString());
                rtn.MarketInfo.Ask = new Order.OrderChance.Market.Constraint();
                rtn.MarketInfo.Ask.Currency = jObj["market"]["ask"]["currency"].ToString();
                //rtn.MarketInfo.Ask.PriceUnit = jObj["market"]["bid"]["price_unit"].ToString();
                rtn.MarketInfo.Ask.MinTotal = double.Parse(jObj["market"]["ask"]["min_total"].ToString());
                rtn.MarketInfo.MaxTotal = double.Parse(jObj["market"]["max_total"].ToString());
                rtn.MarketInfo.State = jObj["market"]["state"].ToString();
                //rtn.BidAccount = new Account();
                //rtn.BidAccount.Currency = jObj["bid_account"]["currency"].ToString();
                //rtn.BidAccount.Balance = double.Parse(jObj["bid_account"]["balance"].ToString());
                //rtn.BidAccount.Locked = double.Parse(jObj["bid_account"]["locked"].ToString());
                //rtn.BidAccount.AvgBuyPrice = double.Parse(jObj["bid_account"]["avg_buy_price"].ToString());
                //rtn.BidAccount.AvgBuyPriceModified = bool.Parse(jObj["bid_account"]["avg_buy_price_modified"].ToString());
                //rtn.BidAccount.UnitCurrency = jObj["bid_account"]["unit_currency"].ToString();
                //rtn.AskAccount = new Account();
                //rtn.AskAccount.Currency = jObj["ask_account"]["currency"].ToString();
                //rtn.AskAccount.Balance = double.Parse(jObj["ask_account"]["balance"].ToString());
                //rtn.AskAccount.Locked = double.Parse(jObj["ask_account"]["locked"].ToString());
                //rtn.AskAccount.AvgBuyPrice = double.Parse(jObj["ask_account"]["avg_buy_price"].ToString());
                //rtn.AskAccount.AvgBuyPriceModified = bool.Parse(jObj["ask_account"]["avg_buy_price_modified"].ToString());
                //rtn.AskAccount.UnitCurrency = jObj["ask_account"]["unit_currency"].ToString();
                rtn.BidAccount = JsonConvert.DeserializeObject<Account>(jObj["bid_account"].ToString());
                rtn.AskAccount = JsonConvert.DeserializeObject<Account>(jObj["ask_account"].ToString());
            }

            return rtn;
        }
        #endregion
    }
}
