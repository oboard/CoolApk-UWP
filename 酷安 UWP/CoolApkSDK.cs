using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace 酷安_UWP
{
    class CoolApkSDK
    {

        public static string GetToken()
        {
            //return await Web.GetHttp("http://l.w568w.ml/api/token.php")

            String DEVICE_ID = "8513efac-09ea-3709-b214-95b366f1a185";
            long UnixDate =(DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
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
                var mClient = new HttpClient();
                mClient.DefaultRequestHeaders.Add("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.1; Nexus 4 Build/LMY48T) (#Build; google; Nexus 4; LMY48T; 5.1.1) +CoolMarket/7.3");
                mClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Int", "25");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Locale", "zh-CN");
                mClient.DefaultRequestHeaders.Add("X-App-Id", "com.coolapk.market");
                mClient.DefaultRequestHeaders.Add("X-App-Token", GetToken());
                mClient.DefaultRequestHeaders.Add("X-App-Version", "7.3");
                mClient.DefaultRequestHeaders.Add("X-App-Code", "1701135");
                mClient.DefaultRequestHeaders.Add("X-Api-Version", "7");
                //mClient.DefaultRequestHeaders.Add("X-App-Device", getAppDevice());
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

        public static async Task<string> GetUserProfileJsonByUid(String uid)
        {
            string json = await GetCoolApkMessage("https://api.coolapk.com/v6/user/profile?uid=" + uid);
            return json;
        }
        public static async Task<User> GetUserProfileByUid(String uid)
        {
            JsonObject jobj = JsonObject.Parse(await GetUserProfileJsonByUid(uid));
            return User.parseFrom(jobj);
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
                String id = await Web.GetHttp("https://www.coolapk.com/n/" + name);
                id = id.Split("coolmarket://www.coolapk.com/u/")[1];
                id = id.Split(@"""")[0];
                return await GetUserProfileByUid(id);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<Feed> getFeedById(String feedId)
        {
            try
            {
                return Feed.parseFrom(JsonObject.Parse(await GetCoolApkMessage("https://api.coolapk.com/v6/feed/detail?id=" + feedId)));
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static String GetBetween(String str, String left, String right)
        {
            String sssString = str.Substring(left.Length + str.IndexOf(left));
            return sssString.Substring(0, sssString.IndexOf(right));
        }

        public static async Task<string> GetCoolApkUserFace(String UName)
        {
            String body = await Web.GetHttp("https://www.coolapk.com/u/" + UName);
            body = Regex.Split(body, @"<div class=""msg_box"">")[1];
            body = Regex.Split(body, @"src=""")[1];
            return Regex.Split(body, @"""")[0];
        }

    }

    class User : BaseUser
    {
        public String bio;
        public String userGroupName;
        public String groupName;
        public String province;
        public String city;
        public String weibo;
        public Boolean isDeveloper;
        public int feeds;
        public int fans;
        public int follow;
        public int apkFollowNum;
        public int apkRatingNum;
        public int albumNum;
        public int discoveryNum;
        public int level;

        public User(String userName, int uid, String avatarUrl, String bio,
            String userGroupName, String groupName, String province,
            String city, String weibo, bool isDeveloper, int feeds, 
            int fans, int follow, int apkFollowNum, int apkRatingNum,
            int albumNum, int discoveryNum,int level) : base(userName, uid, avatarUrl)
        {
            this.bio = bio;
            this.userGroupName = userGroupName;
            this.groupName = groupName;
            this.province = province;
            this.city = city;
            this.weibo = weibo;
            this.isDeveloper = isDeveloper;
            this.feeds = feeds;
            this.fans = fans;
            this.follow = follow;
            this.apkFollowNum = apkFollowNum;
            this.apkRatingNum = apkRatingNum;
            this.albumNum = albumNum;
            this.discoveryNum = discoveryNum;
            this.level = level;
        }

        public static User parseFrom(JsonObject originData)
        {
            JsonObject data = originData.GetNamedObject("data");
            return new User(data.GetNamedString("username"),
            (int)data.GetNamedNumber("uid"),
            data.GetNamedString("userAvatar"),
            data.GetNamedString("bio"),
            data.GetNamedString("userGroupName"),
            data.GetNamedString("groupName"),
            data.GetNamedString("province"),
            data.GetNamedString("city"),
            data.GetNamedString("weibo"),
            (int)data.GetNamedNumber("isDeveloper", 0) == 1,
            (int)data.GetNamedNumber("feed"),
            (int)data.GetNamedNumber("fans"),
            (int)data.GetNamedNumber("follow"),
            (int)data.GetNamedNumber("apkFollowNum"),
            (int)data.GetNamedNumber("apkRatingNum"),
            (int)data.GetNamedNumber("albumNum"),
            (int)data.GetNamedNumber("discoveryNum"),
            (int)data.GetNamedNumber("level"));
        }
    }

    class BaseUser
    {
        public String userName;
        public int uid;
        public String avatarUrl;

        public BaseUser(String userName, int uid, String avatarUrl)
        {
            this.userName = userName;
            this.uid = uid;
            this.avatarUrl = avatarUrl;
        }
    }

    class Feed
    {
        public BaseUser user;
        public String message;
        public int rank_score;
        public String type;
        public ArrayList likeList;

        public Feed(BaseUser user, String message, int rank_score, String type, ArrayList likeList)
        {
            this.user = user;
            this.message = message;
            this.rank_score = rank_score;
            this.type = type;
            this.likeList = likeList;
        }

        public static Feed parseFrom(JsonObject originData)
        {

            JsonObject data = originData.GetNamedObject("data");
            ArrayList list = new ArrayList();
            JsonArray likeList = data.GetNamedArray("userLikeList");
            for (int index = 0; index < likeList.Count; index++)
            {
                JsonObject user = likeList.GetObjectAt((uint)index);
                list.Add(new BaseUser(user.GetNamedString("username"),
                (int)user.GetNamedNumber("uid", -1),
                user.GetNamedString("userAvatar")));
            }
            return new Feed(new BaseUser(data.GetNamedString("username"), (int)data.GetNamedNumber("uid", -1), data.GetNamedString("userAvatar")),
                    data.GetNamedString("message"),
                    (int)data.GetNamedNumber("rank_score"),
                    data.GetNamedString("feedType"),
                    list);
        }
    }

    class LoginInfo
    {
        public String token;
        public String uid;
        public String sessionId;
        public String userName;

        public LoginInfo(String token, String uid, String sessionId, String userName)
        {
            this.token = token;
            this.uid = uid;
            this.sessionId = sessionId;
            this.userName = userName;
        }
        public static LoginInfo parseFrom(JsonObject origin, String sessionId)
        {
            JsonObject data = origin.GetNamedObject("data");
            return new LoginInfo(data.GetNamedString("token"), data.GetNamedString("uid"), sessionId, data.GetNamedString("username"));
        }
    }
}