using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace urlcontent
{
    class geturlcontent
    {
        public static string Title, Content;
        public static void GetText(string url)
        {
            string substr, jsstr, html;
            int htmllen, i, j, k = 3, start, end = 0;
            int m, n, htmlrow, sublen, match, threshold = 86;
            string totalstr = "";
            string strleft = "", strright, title, contentstr = "";
            int titlestart, titleend;
            html = getHtml(url,"");
            StreamWriter sw;
            titlestart = html.IndexOf("<title>") + 7;
            if (titlestart == 6)
                titlestart=html.IndexOf("<TITLE>") + 7;
            titleend = html.IndexOf("</title>", titlestart);
            if (titleend == -1)
                titlestart = html.IndexOf("</TITLE>",titlestart);
            if (titlestart != -1)
                title = html.Substring(titlestart, titleend - titlestart);
            else
                title = "";
            title = title.Replace("渉", "涉");
            Title = title;
            html = Regex.Replace(html, "(?is)<!DOCTYPE.*?>", "");
            html = Regex.Replace(html, "(?is)<!--.*?-->", "");				// remove html comment
            html = Regex.Replace(html, "(?is)<script.*?>.*?</script>", ""); // remove javascript
            html = Regex.Replace(html, "(?is)<style.*?>.*?</style>", "");   // remove css
            html = Regex.Replace(html, "&.{2,5};|&#.{2,5};", "");			// remove special char
            html = Regex.Replace(html, "(?is)<.*?>", "");

            //sw = new StreamWriter("D:\\hello1.txt");
            //sw.Write(html);
            //sw.Close();
            //html = html.Replace("\r", "");
            //html = html.Replace("\n", "");
            string[] htmlstr = new string[10000];
            start = 0; htmlrow = 0;
            htmllen = html.Length;
            for (m = 0; m < htmllen - 1; m++)
            {
                //if (html[m] == '\r' &&html[m + 1] == '\n')
                //{
                //    sublen = m - start;
                //    htmlstr[htmlrow] = html.Substring(start, sublen);
                //    htmlrow++;
                //    start = m + 2;
                //    m++;
                //}
                if (html[m] == '\n')
                {
                    sublen = m - start;
                    htmlstr[htmlrow] = html.Substring(start, sublen);
                    htmlrow++;
                    start = m + 1;
                }
            }
            int[] count = new int[htmlrow];
            //string[] str = new string[htmlrow];
            char a;
            for (m = 0; m < htmlrow - k; m++)
            {
                count[m] = 0;
                for (n = 0; n <= k; n++)
                {
                    for (i = 0; i < htmlstr[m + n].Length; i++)
                    {
                        a = htmlstr[m + n][i];
                        if (a != '\r' && a != '\n' && a != '\t' && a != ' ')
                            count[m]++;
                    }
                }
                //str[m] = Convert.ToString(count[m])+" ";
                totalstr += Convert.ToInt32(count[m]);
                totalstr += " ";
            }
            //sw = new StreamWriter("D:/count.txt");
            //sw.Write(totalstr);
            //sw.Close();
            start = 0; int max = 0, maxi = 0; match = 0;
            for (i = 0; i < htmlrow - k; i++)
            {
                if (count[i] > max)
                {
                    max = count[i];
                    maxi = i;
                }
            }
            for (i = 0; i < htmlrow - k; i++)
            {
                if (match == 0 && count[i] > threshold)
                {
                    if (nonoise(count, i, k))
                    {
                        j = i + 1;
                        while (true)
                        {
                            if (count[j] > count[i])
                            {
                                start = j;
                                match = 1;
                                break;
                            }
                            else if (count[j] < count[i])
                                break;
                            j++;
                        }
                        if (match == 0)
                        {
                            start = i;
                            match = 1;
                        }
                    }
                }
                else if (match == 1 && count[i] == 0)
                {
                    if (maxi >= start && maxi <= i)
                    {
                        if (maxi <= i - 1)
                            end = i - 1;
                        else
                            end = i;
                        break;
                    }
                    else
                        match = 0;
                }
            }
            //contentstr += (title + "\r\n");
            for (i = start; i <= end; i++)
                contentstr += (htmlstr[i] + "\r\n");
            Content = contentstr;
            //sw=new StreamWriter("D:/content.txt");
            //sw.Write(contentstr);
            //sw.Close();
        }
        private static string html1(string sUrl)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sUrl);
            req.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            req.Accept = "*/*";
            req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            req.ContentType = "text/xml";

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Encoding enc;
            try
            {
                if (resp.CharacterSet != "ISO-8859-1")
                    enc = Encoding.GetEncoding(resp.CharacterSet);
                else
                    enc = Encoding.UTF8;
            }
            catch
            {
                // *** Invalid encoding passed
                enc = Encoding.UTF8;
            }
            string sHTML = string.Empty;
            using (StreamReader read = new StreamReader(resp.GetResponseStream(), enc))
            {
                sHTML = read.ReadToEnd();
                Match charSetMatch = Regex.Match(sHTML, "charset=(?<code>[a-zA-Z0-9\\-]+)", RegexOptions.IgnoreCase);
                string sChartSet = charSetMatch.Groups["code"].Value;
                //if it's not utf-8,we should redecode the html.
                if (!string.IsNullOrEmpty(sChartSet) && !sChartSet.Equals("utf-8", StringComparison.OrdinalIgnoreCase))
                {
                    enc = Encoding.GetEncoding(sChartSet);
                    using (StreamReader read1 = new StreamReader(resp.GetResponseStream(), enc))
                    {
                        sHTML = read1.ReadToEnd();
                    }
                }
            }
            return sHTML;
        }
        private static string getHtml(string url, string charSet)//url是要访问的网站地址，charSet是目标网页的编码，如果传入的是null或者""，那就自动分析网页的编码 
        {
            charSet = "GB2312";
            WebClient myWebClient = new WebClient(); //创建WebClient实例myWebClient 
            // 需要注意的： 
            //有的网页可能下不下来，有种种原因比如需要cookie,编码问题等等 
            //这是就要具体问题具体分析比如在头部加入cookie 
            // webclient.Headers.Add("Cookie", cookie); 
            //这样可能需要一些重载方法。根据需要写就可以了 
            //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。 
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            //如果服务器要验证用户名,密码 
            //NetworkCredential mycred = new NetworkCredential(struser, strpassword); 
            //myWebClient.Credentials = mycred; 
            //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号） 
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            //StreamWriter sw = new StreamWriter("D:/qq-utf-8.txt");
            //sw.Write(strWebData);
            //sw.Close();
            //获取网页字符编码描述信息 
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Value;

            int start = webCharSet.IndexOf("charset=");
            
            //if (start == -1)
            //{
            //    charSet = "utf-8";
            //    strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            //    charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //    webCharSet = charSetMatch.Value;
            //    start = webCharSet.IndexOf("charset=");
            //}
            if (start != -1)
            {
                int end = webCharSet.IndexOf("\"", start);
                webCharSet = webCharSet.Substring(start + 8, end - (start + 8));
                if (webCharSet !=charSet)
                    strWebData = Encoding.GetEncoding(webCharSet).GetString(myDataBuffer);
            }
            //if (charSet == null || charSet == "")
            //    charSet = webCharSet;
            
            //if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.GetEncoding("utf-8"))
            //    strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            return strWebData;
        }
        public static bool nonoise(int[] count, int start, int k)
        {
            int i;
            for (i = 1; i <= k; i++)
            {
                if (count[start + i] == 0)
                    return false;
            }
            return true;
        }
        public static string GetWebContent(string sUrl)
        {
            int i, j;
            string strResult = "";
            Stream streamReceive; Encoding encoding; StreamReader streamReader;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                //声明一个HttpWebRequest请求
                request.Timeout = 3000000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ToString() != "")
                {
                    streamReceive = response.GetResponseStream();
                    encoding = Encoding.GetEncoding("GBK");
                    streamReader = new StreamReader(streamReceive, encoding);
                    strResult = streamReader.ReadToEnd();

                    Match charSetMatch = Regex.Match(strResult, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    string webCharSet = charSetMatch.Value;

                    int start = webCharSet.IndexOf("charset=");

                    //if (start == -1)
                    //{
                    //    charSet = "utf-8";
                    //    strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
                    //    charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    //    webCharSet = charSetMatch.Value;
                    //    start = webCharSet.IndexOf("charset=");
                    //}
                    //if (start != -1)
                    //{
                    //    int end = webCharSet.IndexOf("\"", start);
                    //    webCharSet = webCharSet.Substring(start + 8, end - (start + 8));
                    //    if (webCharSet != Encoding.Default.WebName)
                    //        strWebData = Encoding.GetEncoding(webCharSet).GetString(myDataBuffer);
                    //}
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return strResult;
        }
        private static string gethtml(string url, string charSet)
        {//url是要访问的网站地址，charSet是目标网页的编码
            //如果传入的是null或者""，就自动分析网页的编码
            WebClient myWebClient = new WebClient(); //创建WebClient实例
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.Default.GetString(myDataBuffer);
            //获取网页字符编码描述信息 
            Match charSetMatch = Regex.Match(strWebData, "<meta([^>]*)charset=(\")?(.*)?\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[3].Value;
            if (charSet == null || charSet == "") charSet = webCharSet;
            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
            {
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            }
            return strWebData;
        }
        
    }
}
