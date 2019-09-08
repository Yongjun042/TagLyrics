using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TagLyrics
{
    class LyricsFinder
    {
        public string GetLyrics(string post)
        {
            HtmlAgilityPack.HtmlWeb webPage = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument webDoc = webPage.Load(post);
            //iframe 내용 열기
            var iframe = webDoc.DocumentNode.SelectSingleNode("//body/iframe").Attributes["src"].Value;
            Console.WriteLine("iframe " + iframe);
            webDoc = webPage.Load(iframe);
            iframe = webDoc.DocumentNode.SelectSingleNode("//body/iframe").Attributes["src"].Value;
            Console.WriteLine("iframe2 " + iframe);
            webDoc = webPage.Load("http://blog.naver.com"+iframe);
            var temp = webDoc.DocumentNode.SelectSingleNode("//body/div[@id='head-skin']/div[@id='body']/div[@id='whole-border']/div[@id='whole-body']/div[@id='wrapper']/div[@id='twocols']/div[@id='content-area']/div[@id='post-area']/div[@id='postListBody']/div[1]/div[@class='post-back']/table[@id='printPost1']/tr[1]/td[2]/div[@id='postViewArea']").InnerText;
            return HttpUtility.HtmlDecode(temp);
            //*[contains(@class, 'se_textarea')]

        }
        //네이버검색으로 가사검색
        public string SearchbyNaver(string blog, string name1, string name2, string board="")
        {
            Console.WriteLine("search by "+name1);
            string gSearch = "http://blog.naver.com/PostSearchList.nhn?blogId="+blog+"&searchText=" + name1;
            HtmlAgilityPack.HtmlWeb webPage = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument webDoc = webPage.Load(gSearch);
            //게시판 조건 검색
            if (board !="")
            {
                if (webDoc.DocumentNode.SelectSingleNode("//div[@id='body']/div[@id='whole-border']/div[@id='whole-body']/div[@id='wrapper']/div[@id='twocols']/div[@id='content-area']/div[@id='post-area']/table[1]/tr[1]/td[1]/table[1]/tr[7]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[1]/td[1]/span[2]").InnerText != board)
                {
                    gSearch = "http://blog.naver.com/PostSearchList.nhn?blogId=" + blog + "&searchText=" + name2;
                    webDoc = webPage.Load(gSearch);
                    Console.WriteLine("search by " + name2);
                }
            }

            var post = webDoc.DocumentNode.SelectSingleNode("//div[@id='body']/div[@id='whole-border']/div[@id='whole-body']/div[@id='wrapper']/div[@id='twocols']/div[@id='content-area']/div[@id='post-area']/table[1]/tr[1]/td[1]/table[1]/tr[7]/td[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[1]/td[1]/a").Attributes["href"].Value;
            Console.WriteLine("post " + post);
            
            return GetLyrics(post);
        }
    }
}
