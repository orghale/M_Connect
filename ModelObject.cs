using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M_Connect
{
    public class Form
    {
        public string url { get; set; }
        public string type { get; set; }
        public string input { get; set; }
        public string method { get; set; }
    }

    public class Link
    {
        public string content { get; set; }
        public string url { get; set; }
    }

    public class Page
    {
        public string menu { get; set; }
        public string history { get; set; }
        public string navigation_keywords { get; set; }
        public string session_end { get; set; } = null;
        public string callback { get; set; } = null;
        public string method { get; set; } = null;

    }
    public class Push
    {
        public string url { get; set; }
        public string callback { get; set; }
    }
    public class Response_Object
    {
        public string title { get; set; }
        [MaxLength(2000)]
        public string message { get; set; }
        public Form form { get; set; }
        public List<Link> links { get; set; }
        public Page page { get; set; }
        public Push push { get; set; } = null;
    }


    public class Callback
    {
        public string url { get; set; }
        public DateTime timestamp { get; set; }
        public State state { get; set; }
        public int split_page { get; set; }
        public string session_state { get; set; }
        public string alias { get; set; }
    }

    public class State
    {
        public string description { get; set; }
        public int code { get; set; }
    }

    public class CallbackRes
    {
        public string url { get; set; }
        public string timestamp { get; set; }
        public State state { get; set; }
        public int split_page { get; set; }
        public string alias { get; set; }
    }

  

}
