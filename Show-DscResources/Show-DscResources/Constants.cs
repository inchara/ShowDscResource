
namespace Show_DscResources
{
    /// <summary>
    /// Class for all the string constants used in the other classes
    /// </summary>
    class Constants
    {

        public const string DscResourceImplementedAs = "ImplementedAs";
        public const string DscResourceResourceType = "ResourceType";
        public const string DscResourceFriendlyName = "FriendlyName";
        public const string DscResourceCompanyName = "CompanyName";
        public const string DscResourceName = "Name";
        public const string DscResourceModule = "Module";
        public const string DscResourceProperties = "Properties";
        public const string DscResourcePath = "Path";
        public const string DscResourceParentPath = "ParentPath";
        public const string DscPropertyHeaderName = "Name";
        public const string DscPropertyHeaderPropertyType = "PropertyType";
        public const string DscPropertyHeaderIsMandatory = "IsMandatory";
        public const string DscPropertyHeaderValues = "Values";
        public const string VisibilityHidden = "Hidden";
        public const string VisibilityVisible = "Visible";
        public const string DscPropertyTypeBool = "bool";
        public const string DscPropertyTypeInt = "int";
        public const string DscPropertyTypeString = "string";
        public const string RadioButtonContentTrue = "True";
        public const string RadioButtonContentFalse = "False";
        public const string DecoratorVariableStarter = "$";
        public const string DecoratorQuotation = "\"";
        public const string ErrorCannotInsert = "The add-on cannot insert it into the script pane (Make sure that you double clicked on a resource first to see its properties). ";
        public const string ErrorCannotCopy = "The add-on cannot copy it into the script pane (Make sure that you double clicked on a resource first to see its properties). ";

        public const string DscResourceAddOnHelpText = @"
                      
Create an empty configuration block in your script pane which looks like :
Configuration myconfiguration
{
    <Place Cursor here>
}

Now, double click any resource in the table and you will see details about the resource that you can fill out underneath it. Once you enter the required details, you could insert the resource snippet onto the script pane at the current cursor position, or you could copy it to clipboard by clicking on the insert or copy buttons below the table.
**********************************
Refresh button        : If a new dsc resource has been added to the system, click on the refresh button to reload all resources again.
Help button           : Click on it if you want to see this message again :)
**********************************
Insert Button         : This button helps you insert the given dsc resource snippet into your current ISE script
Copy Button           : This button helps you copy the resource onto the clipboard. You could later paste it in any window.
**********************************
Properties Region     : The resource properties will show up with their possible values (if there's a value present), or else there is a text box where you could enter plain text or a variable name. 

***Hover your mouse over any property name to know its description from the schema.mof or schema.mfl file.
";
    }
}
