using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using MessageBox = System.Windows.MessageBox;
using System.IO;

namespace Show_DscResources
{
    /// <summary>
    /// Handles the class for each resource displayed in the table format
    /// </summary>
    public class DscResourceClass
    {
        private int Index { get; set; }
        private string _schemaMflContent { get; set; }
        public string Name { get; set; }
        public List<DscNameInfo> resourceNameInfo;
        internal ObservableCollection<DscResourceNode> DscResource { get; set; }
        public string Module { get; set; }
        private readonly PSObject _dscResourceItem;
        private Boolean _pathInPsHome = true;
        private List<DscResourcePropertyNode> propertySet = new List<DscResourcePropertyNode>();
        private string _actualClassName = "";
        public DscResourceClass(string name, string module, int index)
        {
            Name = name;
            Module = module;
            Index = index;
            _dscResourceItem = DscResourceAddOn.allDscResources[index - 1];//-1 since the output index is 1 more than the 0 indexed array

        }
        public void ExpandResource()
        {
            resourceNameInfo = GetPathEtcInfo();
            _schemaMflContent = GetSchemaMflContent();
            this.GetAllProperties();
            BuildTree();

        }
        public void BuildTree()
        {
            this.DscResource = null;
            this.DscResource = new ObservableCollection<DscResourceNode>();

            string descriptionOfClass = GetClassDescriptionFromFile(this.Name);
            DscResourceNode node = new DscResourceNode(descriptionOfClass);

            node.Name = this.Name;
            foreach (var propertyNode in propertySet)
            {
                node.Children.Add(propertyNode);
            }

            this.DscResource.Add(node);


        }
        public string GetAllProperties()
        {

            string text = "";
            this.propertySet = new List<DscResourcePropertyNode>();
            PSPropertyInfo prop = _dscResourceItem.Properties[Constants.DscResourceProperties];
            var collectionProperties = (ICollection)prop.Value;
            int count = 0;
            foreach (object dscProperty in collectionProperties)
            {
                string lhs = "";
                ArrayList valueSet = new ArrayList();

                var dscPropertyName = dscProperty.GetType().GetProperty(Constants.DscPropertyHeaderName).GetValue(dscProperty, null).ToString();
                var dscPropertyType = dscProperty.GetType().GetProperty(Constants.DscPropertyHeaderPropertyType).GetValue(dscProperty, null);
                var dscIsMandatory = Convert.ToBoolean(dscProperty.GetType().GetProperty(Constants.DscPropertyHeaderIsMandatory).GetValue(dscProperty, null));
                var dscValueSet = dscProperty.GetType().GetProperty(Constants.DscPropertyHeaderValues).GetValue(dscProperty, null);

                if (dscValueSet.GetType().IsGenericType && dscValueSet is IEnumerable)
                {
                    if (((List<String>)dscValueSet).Count > 0)
                    {
                        foreach (var item in ((List<String>)dscValueSet))
                        {
                            valueSet.Add(item);
                        }
                    }

                }
                // var dscPossibleValues =dscProperty.GetType().GetProperty(Constants.DscPropertyHeaderValues).GetValue(dscProperty, null);
                var textInsertIsMandatory = "";
                if (dscIsMandatory)
                {

                    textInsertIsMandatory = " # IsMandatory";
                    lhs = dscPropertyName;
                }
                string descriptionOfProperty = GetPropertyDescriptionFromFile(dscPropertyName);
                DscResourcePropertyNode propertyNode = new DscResourcePropertyNode(dscPropertyType.ToString(), valueSet, dscPropertyName, descriptionOfProperty) { propertyType = dscPropertyType.ToString(), StringComment = textInsertIsMandatory, IsMandatory = dscIsMandatory, IsChosen = dscIsMandatory, propertyValueSet = valueSet };
                count++;
                this.propertySet.Add(propertyNode);

            }



            return text;
        }
        string GetPropertyDescriptionFromFile(string propertyName)
        {
            string description = null;
            //Split the lines
            var linesInFile = _schemaMflContent.Split(new char[1] { '\n' });
            //Get each line 
            foreach (var line in linesInFile)
            {
                if (line.Contains("Description(") && line.Contains(propertyName))
                {
                    var afterDescriptionTag = (line.Split(new string[] { "Description(\"" }, StringSplitOptions.None))[1];

                    var afterDescriptionArray = (afterDescriptionTag.Split(new string[] { "\")" }, StringSplitOptions.None));
                    var onlyDescription = afterDescriptionArray[0];
                    if (afterDescriptionArray[1].Contains(propertyName))
                    {
                        description = onlyDescription;
                    }
                }
            }

            return description;
        }
        string GetClassDescriptionFromFile(string className)
        {
            string description = null;
            //Split the lines
            var linesInFile = _schemaMflContent.Split(new char[1] { '\n' });
            //Get each line 
            for (int i = 0; i < linesInFile.Length; i++)
            {
                string line = linesInFile[i];
                string nextLine = i < linesInFile.Length - 1 ? linesInFile[i + 1] : null;
                if (nextLine == null) break;
                if (line.Contains("Description") && nextLine.Contains(className))
                {
                    var afterDescriptionTag = (line.Split(new string[] { "Description(\"" }, StringSplitOptions.None))[1];
                    var onlyDescription = (afterDescriptionTag.Split(new string[] { "\")" }, StringSplitOptions.None))[0];
                    description = onlyDescription;
                }
            }
            foreach (var line in linesInFile)
            {

            }

            return description;
        }
        public string GetActualClassName()
        {

            return _actualClassName;
        }
        /// <summary>
        /// Function to get the path and other information from get-dscresource property execution
        /// </summary>
        /// <returns></returns>
        public List<DscNameInfo> GetPathEtcInfo()
        {
            List<DscNameInfo> newInfo = null;

            try
            {
                newInfo = new List<DscNameInfo>
                        {
                            PrintPsPropertyDetail(Constants.DscResourceName),
                            new DscNameInfo(Constants.DscResourceModule, Module),
                            PrintPsPropertyDetail(Constants.DscResourceResourceType),
                            PrintPsPropertyDetail(Constants.DscResourceFriendlyName)
                        };

                var resourceType = newInfo[2];//Strore resorucetype
                _actualClassName = (resourceType != null) ? resourceType.Value : null;


                string path = PrintPsPropertyEmptyIfNull(_dscResourceItem.Properties[Constants.DscResourcePath]);
                if (path != String.Empty && !path.StartsWith(Environment.GetEnvironmentVariable("windir")) && Module != String.Empty)
                {
                    this._pathInPsHome = false;
                }

                newInfo.Add(PrintPsPropertyDetail(Constants.DscResourcePath));
                newInfo.Add(PrintPsPropertyDetail(Constants.DscResourceParentPath));
                newInfo.Add(PrintPsPropertyDetail(Constants.DscResourceImplementedAs));
                newInfo.Add(PrintPsPropertyDetail(Constants.DscResourceCompanyName));

            }
            catch (Exception e)
            {
                
                MessageBox.Show("An error occured! Sorry, some crash due to " + e.Message, "Error");

            }
            return newInfo;
        }
        public string PrintPsPropertyEmptyIfNull(PSPropertyInfo myOutput)
        {
            return myOutput != null && myOutput.Value != null ? myOutput.Value.ToString() : String.Empty;
        }
        public DscNameInfo PrintPsPropertyDetail(string propertyName)
        {
            return
                (new DscNameInfo(propertyName, PrintPsPropertyEmptyIfNull(_dscResourceItem.Properties[propertyName])));

        }
        public Boolean IsPathInPsHome()
        {
            return _pathInPsHome;
        }
        public string GetSchemaMflContent()
        {
            string content = "";
            string parentPath = "";
            foreach (var nameInfo in resourceNameInfo)
            {
                if (nameInfo.Name.Equals(Constants.DscResourceParentPath))
                {
                    parentPath = nameInfo.Value;
                    break;
                }
            }
            //Now Add parentPath + "\\" + en-us 
            string localizedDir = parentPath + "\\" + "en-us";
            if (Directory.Exists(localizedDir))
            {
                string localizedmfl = localizedDir + "\\" + _actualClassName + ".schema.mfl";
                if (File.Exists(localizedmfl))
                {
                    using (StreamReader sr = new StreamReader(localizedmfl))
                    {
                        content = sr.ReadToEnd();
                    }
                }

            }
            else
            {//Look for it inside schema.mof
                string descriptiveMof = parentPath + "\\" + _actualClassName + ".schema.mof";
                if (File.Exists(descriptiveMof))
                {
                    using (StreamReader sr = new StreamReader(descriptiveMof))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }

    }

}
