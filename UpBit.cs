using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace SharpUpBit
{
    /// <summary>
    /// UpBit API 관리 클래스
    /// </summary>
    public partial class UpBit
    {
        #region 구조체
        /// <summary>
        /// API의 정보가 담긴 구조체
        /// </summary>
        public struct APIInfo {
            #region 프로퍼티
            /// <summary>
            /// Access Key
            /// </summary>
            public string AccessKey { get; set; }

            /// <summary>
            /// 만료 일자
            /// </summary>
            public DateTime ExpireAt { get; set; }
            #endregion
        }
        #endregion

        #region 프로퍼티
        /// <summary>
        /// UpBit API를 사용하기 위한 AccessKey를 설정하거나 취득할 수 있습니다.
        /// </summary>
        public string AccessKey { get; set;}

        /// <summary>
        /// UpBit API를 사용하기 위한 SecretKey를 설정하거나 취득할 수 있습니다.
        /// </summary>
        public string SecretKey { get; set; }
        #endregion

        #region 생성자
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public UpBit() {
            this.AccessKey = string.Empty;
            this.SecretKey = string.Empty;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="accessKey">UpBit API를 사용하기 위한 AccessKey</param>
        /// <param name="secretKey">UpBit API를 사용하기 위한 SecretKey</param>
        public UpBit(string accessKey, string secretKey) {
            this.AccessKey = accessKey;
            this.SecretKey = secretKey;
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 매개변수를 쿼리 문자열로 변환해 취득합니다.
        /// </summary>
        /// <param name="parameters">매개변수</param>
        /// <returns>쿼리 문자열</returns>
        public string GetQueryString(Dictionary<string, string> parameters) {
            StringBuilder sb = new StringBuilder();

            foreach(var pair in parameters) {
                sb.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
            }
            
            return sb.ToString().TrimEnd('&');
        }

        /// <summary>
        /// Jwt Token 문자열을 취득합니다.
        /// </summary>
        /// <returns>Jwt Token 문자열</returns>
        public string GetJwtToken() {
            var payload = new JwtPayload
            {
                { "access_key", this.AccessKey },
                { "nonce", Guid.NewGuid().ToString() },
                //{ "query_hash", query_hash },
                { "query_hash_alg", "SHA512" },
            };

            byte[] keyBytes = Encoding.Default.GetBytes(this.SecretKey);
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);
            var authorizationToken = "Bearer " + jwtToken;

            return authorizationToken;
        }

        /// <summary>
        /// Jwt Token 문자열을 취득합니다.
        /// </summary>
        /// <param name="queryString">쿼리 문자열</param>
        /// <returns>Jwt Token 문자열</returns>
        public string GetJwtToken(string queryString) {
            SHA512 sha512 = SHA512.Create();
            byte[] queryHashByteArray = sha512.ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key", this.AccessKey },
                { "nonce", Guid.NewGuid().ToString() },
                { "query_hash", queryHash },
                { "query_hash_alg", "SHA512" },
            };

            byte[] keyBytes = Encoding.Default.GetBytes(this.SecretKey);
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);
            var authorizationToken = "Bearer " + jwtToken;

            return authorizationToken;
        }

        /// <summary>
        /// 본인의 UpBit API 키 목록과 만료 일자를 취득합니다.
        /// </summary>
        /// <returns>UpBit API 키 목록과 만료 일자</returns>
        public async Task<List<APIInfo>> GetMyAPIList() {
            var client = new System.Net.Http.HttpClient();
            var request = new System.Net.Http.HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Get,
                RequestUri = new Uri(@"https://api.upbit.com/v1/api_keys"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", this.GetJwtToken() },
                },
            };

            List<APIInfo> lst = new List<APIInfo>();

            using(var response = await client.SendAsync(request)) {
                var body = await response.Content.ReadAsStringAsync();

                JArray jArr = JArray.Parse(body);

                for (int i = 0; i < jArr.Count; i++) {
                    lst.Add(new APIInfo { AccessKey = jArr[i]["access_key"].ToString(), ExpireAt = DateTime.Parse(jArr[i]["expire_at"].ToString()) });
                }
            }

            return lst;
        }
        #endregion
    }
}
