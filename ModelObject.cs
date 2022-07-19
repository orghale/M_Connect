using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M_Connect
{
    public class Form
    {
        public string url { get; set; }
        public string type { get; set; }
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
    }

    public class Response_Object
    {
        public string title { get; set; }
        [MaxLength(2000)]
        public string message { get; set; }
        public Form form { get; set; }
        public List<Link> links { get; set; }
        public Page page { get; set; }
    }

}
