
namespace Show_DscResources
{
    /// <summary>
    /// Class for the visual representation of every resource
    /// </summary>
    public class DscNameInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public DscNameInfo(string dscInfoType, string dscInfoValue)
        {
            Name = dscInfoType;
            Value = dscInfoValue;
        }
    }
}
