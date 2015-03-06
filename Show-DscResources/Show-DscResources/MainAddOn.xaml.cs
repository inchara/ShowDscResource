using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Management.Automation;
using Microsoft.PowerShell.Host.ISE;
using System.Collections;

/*
 * To runin powershell
 * Add-type -path "E:\Inchara\MyCode\DSCResourceAddon\ShowDscResource\Show-DscResources\Show-DscResources\bin\Debug\Show-DscResources.dll"
 * $psISE.CurrentPowerShellTab.VerticalAddOnTools.Add(‘Show-DscResource’, [Show_DscResources.DscResourceAddOn], $true)
 *
 */

namespace Show_DscResources
{
    /// <summary>
    /// Interaction logic for DscResourceAddOn.xaml
    /// </summary>
    public partial class DscResourceAddOn : UserControl, IAddOnToolHostObject
    {
        #region Variables
        private int i = 0;
        public static Collection<PSObject> allDscResources;
        public ObjectModelRoot hostObject;
        private DscResourceClass selectedRow;
        public DscResourceAddOn()
        {
            InitializeComponent();
            this.DataContext = this;
            ResourceAndModule = LoadAllDscResources();

            DataGridObject.ItemsSource = ResourceAndModule;
            hostObject = null;

        }
        public ObjectModelRoot HostObject
        {
            get
            {
                return this.hostObject;
            }
            set
            {
                this.hostObject = value;
            }
        }
        public ObservableCollection<DscResourceClass> ResourceAndModule { get; set; }

        #endregion Variables
        
        #region ButtonClicks
        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataGridObject.SelectedItem == null) return;
                selectedRow = DataGridObject.SelectedItem as DscResourceClass;
                selectedRow.ExpandResource();
                DataGridNameValue.ItemsSource = selectedRow.resourceNameInfo;
                DscSampleCreatorTree.ItemsSource = selectedRow.DscResource;
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }
        private void InsertSampleButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int paddingSpaces = HostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.CaretColumn;
                string paddingSpace = "";
                for (int i = 0; i < paddingSpaces; i++)
                {
                    paddingSpace += " ";
                }
                string textToInsert = GenerateSample();
                textToInsert = "\n" + textToInsert;//Since it wouldn't align well otherwise
                textToInsert = textToInsert.Replace("\n", "\n" + paddingSpace);
                HostObject.CurrentPowerShellTab.Files.SelectedFile.Editor.InsertText(textToInsert);
            }
            catch (Exception ex)
            {
                HandleException(ex, Constants.ErrorCannotInsert);
            }
        }
        private void CopySampleButton_OnClick(object sender, RoutedEventArgs e)
        {

            try
            {

                string textToInsert = GenerateSample();
                textToInsert = "\n" + textToInsert;//Since it wouldn't align well otherwise
                Clipboard.SetText(textToInsert);

            }
            catch (Exception ex)
            {
                HandleException(ex, Constants.ErrorCannotCopy);
            }
        }

        private void ButtonRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ResourceAndModule = LoadAllDscResources();
                DataGridObject.ItemsSource = ResourceAndModule;
                DscSampleCreatorTree.ItemsSource = null;
                DataGridNameValue.ItemsSource = null;
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }
        private void HelpButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(Constants.DscResourceAddOnHelpText, "Help");
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }

        }
        #endregion

        #region Tools
        private string GenerateSample()
        {

            string text = "";
            string importDscResourceString = "";

            text = GenerateText(text, ref importDscResourceString);
            text = importDscResourceString + "\n" + text;
            return text;

        }

        private string GenerateText(string text, ref string importDscResourceString)
        {
            ArrayList modulesToImport = new ArrayList();
            ArrayList resourcesToImport = new ArrayList();


            //Start getting import information
            if (!selectedRow.IsPathInPsHome())
            {
                bool matchFound = false;
                foreach (var module in modulesToImport)
                {
                    if (module.Equals(selectedRow.Module))
                        matchFound = true;
                }
                if (!matchFound)
                {
                    modulesToImport.Add(selectedRow.Module);
                }
                resourcesToImport.Add(selectedRow.GetActualClassName());
            }
            var resourceNode = selectedRow.DscResource[0];
            //Form the text
            text += "\t" + resourceNode.Name + " " +
                ((resourceNode.textBoxValue == null) ? ("EnterNameOf_" + resourceNode.Name) : resourceNode.textBoxValue) + " \n\t{\n";

            foreach (var propertyNode in resourceNode.Children)
            {
                if (propertyNode.IsChosen)
                {
                    text += "\t\t" + String.Format("{0,-25} = {1:N}\n", propertyNode.Name,
                        (propertyNode.GetTextBoxValue() == null) ? "\"" + propertyNode.propertyType + "\"" + propertyNode.StringComment : propertyNode.GetTextBoxValue());
                }
            }
            text += "\t}\n";


            importDscResourceString = FormImportDscResourceString(modulesToImport, resourcesToImport);
            return text;
        }
       
        public ObservableCollection<DscResourceClass> LoadAllDscResources()
        {
            const string script = @"$a=Get-DscResource;$a";
            var myResources = new ObservableCollection<DscResourceClass>();
            var powerShell = PowerShell.Create();
            powerShell.AddScript(script);
            var output = powerShell.Invoke();
            allDscResources = output;
            int index = 0;
            foreach (var item in output)
            {

                index++;
                PSPropertyInfo nameProperty = item.Properties[Constants.DscResourceName];
                PSPropertyInfo moduleProperty = item.Properties[Constants.DscResourceModule];

                //string name = item.ToString().Split('=',);
                myResources.Add(new DscResourceClass(nameProperty.Value.ToString(),
                                        (moduleProperty != null && moduleProperty.Value != null ? moduleProperty.Value.ToString() : String.Empty),
                                        index));

            }
            return myResources;
        }


        //Fnction to create importdsc rsource string
        private string FormImportDscResourceString(ArrayList modulesToImport, ArrayList resourcesToImport)
        {
            var importDscResourceString = "";

            if (modulesToImport.Count != 0 && resourcesToImport.Count != 0)
            {
                var importModuleString = String.Join(",", modulesToImport.ToArray());
                var importResourceString = String.Join(",", resourcesToImport.ToArray());
                importDscResourceString = "\tImport-DscResource "   /*-Name " + importResourceString + */ + " -Module " + importModuleString + "\n";
            }
            return importDscResourceString;


        }

        public static void HandleException(Exception e)
        {
            MessageBox.Show("Something Crashed :( Error found was " + e.Message.ToString() + " \n Report this to improve the addon :)", "Error");
        }
        public static void HandleException(Exception e, string message)
        {

            MessageBox.Show(message, "Error");
        }
        #endregion
    }

}
