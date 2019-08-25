using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;
using 酷安_UWP.CoolApk;

namespace 酷安_UWP
{
    class CoolApkSDK
    {
        //超级感谢！！！👉 https://github.com/ZCKun/CoolapkTokenCrack
        public static string GetToken()
        {
            //return await Web.GetHttp("http://l.w568w.ml/api/token.php")

            String DEVICE_ID = "8513efac-09ea-3709-b214-95b366f1a185";
            long UnixDate = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string t = UnixDate.ToString();
            string hex_t = "0x" + Ten2Hex(t);
            // 时间戳加密
            string md5_t = GetMD5(t);
            // 不知道什么鬼字符串拼接
            string a = "token://com.coolapk.market/c67ef5943784d09750dcfbb31020f0ab?" + md5_t + "$" + DEVICE_ID + "&com.coolapk.market";
            // 不知道什么鬼字符串拼接 后的字符串再次加密
            //md5_a = hashlib.md5(base64.b64encode(a.encode('utf-8'))).hexdigest()
            string md5_a = GetMD5(Convert.ToBase64String(Encoding.UTF8.GetBytes(a)));
            string token = md5_a + DEVICE_ID + hex_t;

            // ==================================================
            return token;//String.Format(" t:" + t + "\n" + " hex_t:" + hex_t + "\n" + " md5_t:" + md5_t + "\n" + " a:" + a + "\n" + " md5_a:" + md5_a + "\n" + " token:" + token);//"f4522d0e6a3b93f7430648bbf51756558513efac-09ea-3709-b214-95b366f1a1850x5d6106fc";
        }

        public static string GetMD5(string inputString)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        public static string Ten2Hex(string ten)
        {


            ulong tenValue = Convert.ToUInt64(ten);
            ulong divValue, resValue;
            string hex = "";
            do
            {
                //divValue = (ulong)Math.Floor(tenValue / 16);

                divValue = (ulong)Math.Floor((decimal)(tenValue / 16));

                resValue = tenValue % 16;
                hex = tenValue2Char(resValue) + hex;
                tenValue = divValue;
            }
            while (tenValue >= 16);
            if (tenValue != 0)
                hex = tenValue2Char(tenValue) + hex;
            return hex;
        }
        public static string tenValue2Char(ulong ten)
        {
            switch (ten)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return ten.ToString();
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "";
            }
        }


        public static async Task<string> GetCoolApkMessage(string url)
        {
            //这里感谢https://github.com/bjzhou/Coolapk-kotlin提供的 HTTP 头！！！！！！！！！！！！！

            /*
            User-Agent: Dalvik/2.1.0 (Linux; U; Android 5.1.1; Nexus 4 Build/LMY48T) (#Build; google; Nexus 4; LMY48T; 5.1.1) +CoolMarket/7.3
            X-Requested-With: XMLHttpRequest
            X-Sdk-Int: 22
            X-Sdk-Locale: zh-CN
            X-App-Id: coolmarket
            X-App-Token: 2a6e2adc2897c8d8133db17c2cd3b1045834ce58-d7d5-38eb-95d5-563167a1983d0x588f16cd
            X-App-Version: 7.3
            X-App-Code: 1701135
            X-Api-Version: 7
            */
            try
            {
                //var mClient = new HttpClient();
                //mClient.DefaultRequestHeaders.Add("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.1; Nexus 4 Build/LMY48T) (#Build; google; Nexus 4; LMY48T; 5.1.1) +CoolMarket/7.3");
                //mClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                //mClient.DefaultRequestHeaders.Add("X-Sdk-Int", "25");
                //mClient.DefaultRequestHeaders.Add("X-Sdk-Locale", "zh-CN");
                //mClient.DefaultRequestHeaders.Add("X-App-Id", "com.coolapk.market");
                //mClient.DefaultRequestHeaders.Add("X-App-Token", GetToken());
                //mClient.DefaultRequestHeaders.Add("X-App-Version", "7.3");
                //mClient.DefaultRequestHeaders.Add("X-App-Code", "1701135");
                //mClient.DefaultRequestHeaders.Add("X-Api-Version", "7");
                //mClient.DefaultRequestHeaders.Add("X-App-Device", getAppDevice());


                var mClient = new HttpClient();

                mClient.DefaultRequestHeaders.Add("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 9; MI 8 SE MIUI/9.5.9) (#Build; Xiaomi; MI 8 SE; PKQ1.181121.001; 9) +CoolMarket/9.2.2-1905301");
                mClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Int", "28");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Locale", "zh-CN");
                mClient.DefaultRequestHeaders.Add("X-App-Id", "com.coolapk.market");
                mClient.DefaultRequestHeaders.Add("X-App-Token", GetToken());
                mClient.DefaultRequestHeaders.Add("X-Api-Version", "9");
                mClient.DefaultRequestHeaders.Add("X-App-Version", "9.2.2");
                mClient.DefaultRequestHeaders.Add("X-App-Code", "1903501");
                mClient.DefaultRequestHeaders.Add("Host", "api.coolapk.com");
                mClient.DefaultRequestHeaders.Add("X-Dark-Mode", "0");
                mClient.DefaultRequestHeaders.Add("Cookie", "CONTAINERID=4e5f0b1a2e32f938a6519f55dd77765b70002fbb8133e5730bbe80f581b8e536|XQhuo|XQ;hueuid=10002;sec_tc=AQAAAJSicE9VgAEAq1MWyuDtcSLw+WlZ;");


                // mClient.DefaultRequestHeaders.Add("X-App-Device", "QRTBCOgkUTgsTat9WYphFI7kWbvFWaYByO1YjOCdjOxAjOxEkOFJjODlDI7ATNxMjM5MTOxcjMwAjN0AyOxEjNwgDNxITM2kDMzcTOgsTZzkTZlJ2MwUDNhJ2MyYzM");


                return await mClient.GetStringAsync(new Uri(url));
            }
            catch (Exception)
            {
                return "";
            }
        }


        /**
        * 根據User Id獲得用戶信息，失敗返回null
        *
        * @param uid User Id
        * @return
        */

        public static async Task<string> GetUserProfileJsonByUid(dynamic uid)
        {
            string json = await GetCoolApkMessage("https://api.coolapk.com/v6/user/profile?uid=" + uid);
            return json;
        }


        public static async Task<string> GetUserIDByName(String name)
        {
            try
            {
                String uid = await Web.GetHttp("https://www.coolapk.com/n/" + name);
                uid = uid.Split("coolmarket://www.coolapk.com/u/")[1];
                uid = uid.Split(@"""")[0];
                return uid;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<User> GetUserProfileByID(dynamic uid)
        {
            string result = await GetUserProfileJsonByUid(uid);
            UserRoot user = JsonMapper.ToObject<UserRoot>(result);
            return user.data;
        }

        /**
            * 根據用戶名獲得用戶信息，失敗返回null
            *
            * @param name 用戶名
            * @return 用戶信息
            */
        public static async Task<User> GetUserProfileByName(String name)
        {
            try
            {
                String uid = await GetUserIDByName(name);
                return await GetUserProfileByID(uid);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<string> GetFeedListJsonByID(dynamic uid, dynamic page, dynamic firstItem)
        {
            try
            {
                string result = await GetCoolApkMessage("https://api.coolapk.com/v6/user/feedList?uid=" + uid + "&page=" + page + "&firstItem=" + firstItem);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<FeedRoot> GetFeedListByID(dynamic uid, dynamic page, dynamic firstItem)
        {
            //try
            //{
            string str = await GetFeedListJsonByID(uid, page, firstItem);
            FeedRoot feedRoot = JsonMapper.ToObject<FeedRoot>(str);
            return feedRoot;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        public static DataItem GetFeedDetailByJson(String feed)
        {
            try
            {
                DataItem feed1 = JsonMapper.ToObject<DataItem>(feed);
                return feed1;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<DataItem> getFeedDetailById(dynamic feedId)
        {
            try
            {
                string result = await GetCoolApkMessage("https://api.coolapk.com/v6/feed/detail?id=" + feedId);
                DataItem feed1 = JsonMapper.ToObject<DataItem>(result);
                return feed1;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static async Task<string> GetCoolApkUserFaceUri(dynamic NameOrID)
        {
            String body = await Web.GetHttp("https://www.coolapk.com/u/" + NameOrID);
            body = Regex.Split(body, @"<div class=""msg_box"">")[1];
            body = Regex.Split(body, @"src=""")[1];
            return Regex.Split(body, @"""")[0];
        }

    }
}