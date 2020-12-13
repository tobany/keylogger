namespace KeyLogger
{
    public abstract class KeyLogData
    {
        private string type;
        private string data;

        public string Type
        {
            get => type;
            set => type = value;
        }

        public string Data
        {
            get => data;
            set => data = value;
        }

        public KeyLogData(string type)
        {
            this.type = type;
            this.data = "";
        }

        public KeyLogData(string type, string data)
        {
            this.type = type;
            this.data = data;
        }
    }
}