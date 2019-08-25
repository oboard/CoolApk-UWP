using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 酷安_UWP.CoolApk
{
    //如果好用，请收藏地址，帮忙分享。
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public UInt32 uid { get; set; }
        /// <summary>
        /// 一块小板子
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 admintype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 groupid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 usergroupid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 usernamestatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 avatarstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 avatar_cover_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 regdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 logintime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fetchType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entityType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 entityId { get; set; }
        /// <summary>
        /// 一块小板子
        /// </summary>
        public string displayUsername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userSmallAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userBigAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
    }

    public class TargetRow
    {
        /// <summary>
        /// 
        /// </summary>
        public UInt32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 314下载
        /// </summary>
        public string subTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entityType { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public UInt32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 fid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forwardid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 uid { get; set; }
        /// <summary>
        /// 一块小板子
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 dyh_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dyh_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 tid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ttitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tpic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string turl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message_title_md5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message_keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message_cover { get; set; }
        /// <summary>
        /// 怎么只能替换不能查找啊
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 message_length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 issummary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 istag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 is_html_article { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 media_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string media_pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string media_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 extra_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 extra_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 fromid { get; set; }
        /// <summary>
        /// 酷安客户端
        /// </summary>
        public string fromname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 likenum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 burynum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 commentnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 replynum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 forwardnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 reportnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 relatednum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 favnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 share_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 comment_block_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 question_answer_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 question_follow_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 hitnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 viewnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 feed_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 rank_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 vote_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 at_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 url_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 tag_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 recommend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 is_anonymous { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 is_hidden { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 is_headline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 disallow_reply { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 message_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 block_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 dateline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 lastupdate { get; set; }
        /// <summary>
        /// 红米Note 7
        /// </summary>
        public string device_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string device_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string device_rom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string device_build { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recent_reply_ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recent_hot_reply_ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recent_like_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string related_dyh_ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string post_signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message_signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fetchType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 entityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarFetchType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entityTemplate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entityType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string feedType { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public string feedTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string turlTarget { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infoHtml { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> picArr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> relateddata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string media_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceFeed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forwardSourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shareUrl { get; set; }
        /// <summary>
        /// 一块小板子的评论
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> replyRows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 replyRowsCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 replyRowsMore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserInfo userInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> relationRows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TargetRow targetRow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> recentLikeList { get; set; }
    }
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// 一块小板子
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int admintype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int usergroupid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int usernamestatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int avatarstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int avatar_cover_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int regdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int logintime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fetchType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entityType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int entityId { get; set; }
        /// <summary>
        /// 一块小板子
        /// </summary>
        public string displayUsername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userSmallAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userBigAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 开发者
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userGroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isBlackList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isIgnoreList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isLimitList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 广东
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 深圳
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 双子座
        /// </summary>
        public string astro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weibo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blog { get; set; }
        /// <summary>
        /// 酷安不知名开发者
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isDeveloper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int verify_type { get; set; }
        /// <summary>
        /// Hey浏览器 开发者
        /// </summary>
        public string verify_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int verify_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int apkDevNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int feed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int follow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int fans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int apkFollowNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int apkRatingNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int apkCommentNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumFavNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int discoveryNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replyNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isFollow { get; set; }
    }

    public class FeedRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
    }
    public class UserRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public User data { get; set; }
    }

}
