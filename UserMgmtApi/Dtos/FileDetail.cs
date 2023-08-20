namespace UserMgmtApi.Dtos
{
    public class FileDetail
    {
        public FileDetail(string name, string type, long size)
        {
            Name = name;
            Type = type;
            Size = size;
        }
        public string Name { get; }
        public string Type { get; }
        public long Size { get; }
    }
}
