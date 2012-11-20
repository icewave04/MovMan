using System;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using System.Net;
using System.Collections.Generic;

namespace movieData
{
    public class Imdb
    {
        private HtmlAgilityPack.HtmlDocument _markup;


        public bool Search(string query)
        {
            HtmlWeb web = new HtmlWeb();
            _markup = web.Load("http://www.imdb.com/find?s=all&q=" + WebUtility.HtmlEncode(query));

            if (!IsMoviePage())
            {
                string urlBestResult = FindBestMatch();
                if (!String.IsNullOrEmpty(urlBestResult))
                {
                    _markup = web.Load(urlBestResult);
                }
                else return false;
            }

            return IsMoviePage();
        }


        private bool IsMoviePage()
        {
            return (_markup.DocumentNode.SelectSingleNode(".//h1[contains(@itemprop, 'name')]") != null);
        }


        private string FindBestMatch()
        {
            HtmlNodeCollection headers = _markup.DocumentNode.SelectNodes("//p/b[contains(., 'Titles')]");
            if (headers != null) foreach (HtmlNode header in headers)
            {
                HtmlNode link = header.ParentNode.SelectSingleNode(".//a[contains(@href, '/title/')]");
                if (link != null)
                {
                    return "http://www.imdb.com" + link.Attributes["href"].Value;
                }
            }
            return String.Empty;
        }


        public string Title
        {
            get
            {
                HtmlNode title = _markup.DocumentNode.SelectSingleNode("//title");
                return title == null ? String.Empty : title.InnerText.Remove(title.InnerText.IndexOf("(")).Replace("&#x22;", String.Empty);
            }
        }


        public string Year
        {
            get
            {
                HtmlNode year = _markup.DocumentNode.SelectSingleNode("//title");
                return year == null ? String.Empty : Regex.Match(year.InnerText, @"\((.*?)\)").Groups[1].Value;
            }
        }


        public string Link
        {
            get
            {
                HtmlNode link = _markup.DocumentNode.SelectSingleNode("//link[contains(@rel, 'canonical')]");
                return link == null ? String.Empty : link.Attributes["href"].Value;
            }
        }


        public string Id
        {
            get
            {
                return this.Link.Replace("http://www.imdb.com/title/tt", String.Empty).TrimEnd('/');
            }
        }


        public string Director
        {
            get
            {
                HtmlNode director = _markup.DocumentNode.SelectSingleNode("//div[contains(@id, 'director-info')]/div/a[@href]");
                return director == null ? String.Empty : director.InnerText;
            }
        }


        public string Genre
        {
            get
            {
                HtmlNode header = _markup.DocumentNode.SelectSingleNode("//a[contains(@href, '/genre/')]");
                return header.InnerText;
            }
        }


        public string Plot
        {
            get
            {
                HtmlNode header = _markup.DocumentNode.SelectSingleNode("//p[contains(@itemprop, 'description')]");
                return header.InnerText;
            }
        }

        public List<string> getActors
        {
            get
            {
                List<string> returnList = new List<string>();
                HtmlNodeCollection actorNodes = _markup.DocumentNode.SelectNodes("//td[contains(@class, 'name')]");
                foreach (HtmlNode actor in actorNodes)
                {
                    returnList.Add(actor.InnerText.Trim());
                }
                return returnList;
            }
        }

        
    }
}
