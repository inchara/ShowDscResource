
using System.Collections.ObjectModel;

namespace Show_DscResources
{
    /// <summary>
    /// Class describing properties for each property for a resource
    /// </summary>
    public class DscResourceNode
    {

        public DscResourceNode(string description)
        {
            this.Children = new ObservableCollection<DscResourcePropertyNode>();
            StringComment = "(Enter ResourceName)";
            IsChosen = true;
            this.Description = description;
            FormRunText();
            textBoxWidth = 100;
        }

        public ObservableCollection<DscResourcePropertyNode> Children { get; set; }

        public string Name { get; set; }
        public string StringComment { get; set; }
        public string textBoxValue { get; set; }
        public int textBoxWidth { get; set; }
        public bool IsChosen { get; set; }
        public string Description { get; set; }
        public string RunTextOnWindow { get; set; }
        public void FormRunText()
        {
            RunTextOnWindow = StringComment;
        }

    }
}
