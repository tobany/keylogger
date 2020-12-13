using System;
using System.Collections.Generic;
using System.Net;

namespace KeyLogger
{
    public class KeyData
    {
        private IPAddress public_source;
        private IPAddress private_source;
        private string source_name;
        private Dictionary<DateTime, KeyLogData []> data;

    }
}