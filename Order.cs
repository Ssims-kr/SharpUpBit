using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
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

            /// <summary>
            /// 주문건 정보
            /// </summary>
            public class OrderInfo {
                #region 클래스
                /// <summary>
                /// 체결 정보
                /// </summary>
                public class TradeInfo {
                    #region 프로퍼티
                    /// <summary>
                    /// 마켓의 유일키
                    /// </summary>
                    [JsonProperty(PropertyName = "market")]
                    public string Market { get; set; }

                    /// <summary>
                    /// 체결의 고유 아이디
                    /// </summary>
                    [JsonProperty(PropertyName = "uuid")]
                    public string UUID { get; set; }

                    /// <summary>
                    /// 체결의 가격
                    /// </summary>
                    [JsonProperty(PropertyName = "price")]
                    public double Price { get; set; }

                    /// <summary>
                    /// 체결 양
                    /// </summary>
                    [JsonProperty(PropertyName = "volume")]
                    public double Volume { get; set; }

                    /// <summary>
                    /// 체결된 총 가격
                    /// </summary>
                    [JsonProperty(PropertyName = "funds")]
                    public double Funds { get; set; }

                    /// <summary>
                    /// 체결 종류
                    /// </summary>
                    [JsonProperty(PropertyName = "side")]
                    public string Side { get; set; }

                    /// <summary>
                    /// 체결 시각
                    /// </summary>
                    [JsonProperty(PropertyName = "created_at")]
                    public DateTime CreatedAt { get; set; }
                    #endregion
                }
                #endregion

                #region 프로퍼티
                /// <summary>
                /// 주문의 고유 아이디
                /// </summary>
                [JsonProperty(PropertyName="uuid")]
                public string UUID { get; set; }

                /// <summary>
                /// 주문 종류
                /// </summary>
                [JsonProperty(PropertyName="side")]
                public string Side { get; set; }

                /// <summary>
                /// 주문 방식
                /// </summary>
                [JsonProperty(PropertyName="ord_type")]
                public string OrderType { get; set; }

                /// <summary>
                /// 주문 당시 화폐 가격
                /// </summary>
                [JsonProperty(PropertyName="price")]
                public double Price { get; set; }

                /// <summary>
                /// 주문 상태
                /// </summary>
                [JsonProperty(PropertyName="state")]
                public string State { get; set; }

                /// <summary>
                /// 마켓의 유일키
                /// </summary>
                [JsonProperty(PropertyName="market")]
                public string Market { get; set; }

                /// <summary>
                /// 주문 생성 시간
                /// </summary>
                [JsonProperty(PropertyName="created_at")]
                public DateTime CreatedAt { get; set; }

                /// <summary>
                /// 사용자가 입력한 주문 양
                /// </summary>
                [JsonProperty(PropertyName="volume")]
                public double Volume { get; set; }

                /// <summary>
                /// 체결 후 남은 주문 양
                /// </summary>
                [JsonProperty(PropertyName="remaining_volume")]
                public double RemainingVolume { get; set; }

                /// <summary>
                /// 수수료로 예약된 비용
                /// </summary>
                [JsonProperty(PropertyName="reserved_fee")]
                public double ReservedFee { get; set; }

                /// <summary>
                /// 남은 수수료
                /// </summary>
                [JsonProperty(PropertyName="remaining_fee")]
                public double RemainingFee { get; set; }

                /// <summary>
                /// 사용된 수수료
                /// </summary>
                [JsonProperty(PropertyName="paid_fee")]
                public double PaidFee { get; set; }

                /// <summary>
                /// 거래에 사용중인 비용
                /// </summary>
                [JsonProperty(PropertyName="locked")]
                public double Locked { get; set; }

                /// <summary>
                /// 체결된 양
                /// </summary>
                [JsonProperty(PropertyName="executed_volume")]
                public double ExecutedVolume { get; set; }

                /// <summary>
                /// 해당 주문에 걸린 체결 수
                /// </summary>
                [JsonProperty(PropertyName="trades_count")]
                public int TradesCount { get; set; }

                /// <summary>
                /// 체결 정보
                /// </summary>
                [JsonProperty(PropertyName="trades")]
                public TradeInfo[] Trade { get; set; }
                #endregion
            }
            #endregion
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 마켓별 주문 가능 정보를 취득합니다.
        /// </summary>
        /// <param name="marketId">마켓 ID</param>
        /// <returns>마켓별 주문 가능 정보</returns>
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

        /// <summary>
        /// 주문 UUID를 통해 개별 주문건을 취득합니다.
        /// </summary>
        /// <param name="uuid">주문 UUID</param>
        /// <returns>개별 주문건 정보</returns>
        public async Task<Order.OrderInfo> GetOrderInfo(string uuid) {
            var client = new System.Net.Http.HttpClient();
            var request = new System.Net.Http.HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Get,
                RequestUri = new Uri(@"https://api.upbit.com/v1/order?uuid=" + uuid),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", this.GetJwtToken(this.GetQueryString(new Dictionary<string, string>() { { "uuid", uuid} })) },
                },
            };

            Order.OrderInfo rtn = new Order.OrderInfo();

            using (var response = await client.SendAsync(request)) {
                var body = await response.Content.ReadAsStringAsync();

                JObject jObj = JObject.Parse(body);
                Console.WriteLine(jObj.ToString());

                rtn.UUID = jObj["uuid"].ToString();
                rtn.Side = jObj["side"].ToString();
                rtn.OrderType = jObj["ord_type"].ToString();
                rtn.Price = double.Parse(jObj["price"].ToString());
                rtn.State = jObj["state"].ToString();
                rtn.Market = jObj["market"].ToString();
                rtn.CreatedAt = DateTime.Parse(jObj["created_at"].ToString());
                rtn.Volume = double.Parse(jObj["volume"].ToString());
                rtn.RemainingVolume = double.Parse(jObj["remaining_volume"].ToString());
                rtn.ReservedFee = double.Parse(jObj["reserved_fee"].ToString());
                rtn.RemainingFee = double.Parse(jObj["remaining_fee"].ToString());
                rtn.PaidFee = double.Parse(jObj["paid_fee"].ToString());
                rtn.Locked = double.Parse(jObj["locked"].ToString());
                rtn.ExecutedVolume = double.Parse(jObj["executed_volume"].ToString());
                rtn.TradesCount = int.Parse(jObj["trades_count"].ToString());
                rtn.Trade = jObj.SelectToken("trades").ToObject<Order.OrderInfo.TradeInfo[]>();
            }

            return rtn;
        }
        #endregion
    }
}
