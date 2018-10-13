using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utils.Helpers
{
    public class HtmlHelper
    {
        public static Hashtable HtmlCharHash
        {
            get
            {
                if (_charHash == null)
                {
                    _charHash = new Hashtable();
                    _charHash.Add("&quot;", "\"");
                    //_charHash.Add("\"", "&quot;");
                    _charHash.Add("&amp;", "&");
                    //_charHash.Add("&", "&amp;");
                    _charHash.Add("&lt;", "<");
                    // _charHash.Add("<", "&lt;");
                    _charHash.Add("&gt;", ">");
                    // _charHash.Add(">", "&gt;");
                }
                return _charHash;
            }
        }
        private static Hashtable _charHash;

        public HtmlHelper()
        {
            if (_charHash == null)
            {
                _charHash = new Hashtable();
                _charHash.Add("&quot;", "\"");
                // _charHash.Add("\"", "&quot;");
                _charHash.Add("&amp;", "&");
                // _charHash.Add("&", "&amp;");
                _charHash.Add("&lt;", "<");
                // _charHash.Add("<", "&lt;");
                _charHash.Add("&gt;", ">");
                // _charHash.Add(">", "&gt;");
            }
        }

        public static string GetHtmlChar(string str)
        {
            if (HtmlCharHash.Contains(str))
            {
                return HtmlCharHash[str].ToString();
            }
            return "";
        }

        public static string GetFinalText(string text)
        {
            string strTemp = "";
            foreach (var item in HtmlCharHash.Keys)
            {
                strTemp = item as string;
                text = text.Replace(strTemp, HtmlCharHash[strTemp] as string);
            }
            return text;
        }
    }
}
