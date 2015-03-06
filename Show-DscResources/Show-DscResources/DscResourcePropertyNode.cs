
using System.Collections;
using System.Collections.ObjectModel;

namespace Show_DscResources
{
    /// <summary>
    /// Class describing each property node for a resource on the UI
    /// </summary>
    public class DscResourcePropertyNode
    {
        private bool flagUsingRadioButton = false;
        private bool flagUsingCheckButton = false;
        public DscResourcePropertyNode(string propertyType, ArrayList propertyValueSet, string propertyName, string description)
        {
            this.propertyType = propertyType;
            this.Name = propertyName;
            textBoxVisibility = Constants.VisibilityVisible;
            textBoxWidth = 100;
            radioButtonVisibility = Constants.VisibilityHidden;
            checkButtonVisibility = Constants.VisibilityHidden;
            RadioValues = new ObservableCollection<RadioButtonContent>();
            CheckValues = new ObservableCollection<RadioButtonContent>();

            if (propertyType.Contains(Constants.DscPropertyTypeBool))
            {
                HideTextBox(true);
                RadioValues.Add(new RadioButtonContent(Constants.RadioButtonContentTrue, false, Name));
                RadioValues.Add(new RadioButtonContent(Constants.RadioButtonContentFalse, false, Name));
                flagUsingRadioButton = true;
            }
            //Else if it's an array type but accepts only single value
            else if (propertyValueSet.Count > 1 && !propertyType.Contains("[]"))
            {
                HideTextBox(true);
                foreach (var propertyValue in propertyValueSet)
                {
                    RadioValues.Add(new RadioButtonContent(propertyValue.ToString(), false, Name));
                }
                flagUsingRadioButton = true;
            }
            else if (propertyValueSet.Count > 1)
            {
                HideTextBox(false);
                foreach (var propertyValue in propertyValueSet)
                {
                    CheckValues.Add(new RadioButtonContent(propertyValue.ToString(), false, Name));
                }
                flagUsingCheckButton = true;
            }
            this.Description = description;
            FormRunText();
        }
        public void HideTextBox(bool ChooseRadioOrCheck)
        {
            textBoxVisibility = Constants.VisibilityHidden;
            textBoxWidth = 0;
            if (ChooseRadioOrCheck)
            {
                radioButtonVisibility = Constants.VisibilityVisible;
            }
            else
            {
                checkButtonVisibility = Constants.VisibilityVisible;
            }
        }
        public ObservableCollection<RadioButtonContent> RadioValues { get; set; }
        public ObservableCollection<RadioButtonContent> CheckValues { get; set; }
        public string Description { get; set; }
        public string textBoxVisibility { get; set; }
        public string radioButtonVisibility { get; set; }
        public string checkButtonVisibility { get; set; }
        public ArrayList propertyValueSet;
        public string Name { get; set; }
        public string StringComment { get; set; }
        public bool IsMandatory { get; set; }
        public string propertyType { get; set; }
        public string RunTextOnWindow { get; set; }
        public string textBoxValue
        {
            get;
            set;
        }
        public int textBoxWidth
        {
            get;
            set;
        }
        public bool IsChosen { get; set; }
        public string GetTextBoxValue()
        {
            if (flagUsingRadioButton)
            {
                foreach (var radioButton in RadioValues)
                {
                    if (radioButton.radioIsChecked)
                    {

                        return GetDecoratedRadioValue(radioButton.radioContent);
                    }
                }
            }
            else if (flagUsingCheckButton)
            {
                string checkButtonCombinedContent = "@(";
                bool firstTime = true;
                foreach (var checkButton in CheckValues)
                {
                    if (checkButton.radioIsChecked)
                    {
                        if (!firstTime)
                            checkButtonCombinedContent += ",";
                        checkButtonCombinedContent += GetDecoratedRadioValue(checkButton.radioContent);
                        firstTime = false;
                    }
                }
                checkButtonCombinedContent += ")";
                return checkButtonCombinedContent;
            }
            return textBoxValue == null ? textBoxValue : GetDecoratedRadioValue(textBoxValue);
        }
        public void FormRunText()
        {
            RunTextOnWindow = (textBoxVisibility.Equals(Constants.VisibilityVisible) ? propertyType : "") +
                                StringComment;
        }
        //Function to decorate the content so it matches the type.
        public string GetDecoratedRadioValue(string content)
        {
            if (propertyType.ToLower().Contains(Constants.DscPropertyTypeString))
            {
                return Constants.DecoratorQuotation + content + Constants.DecoratorQuotation;
            }
            if (propertyType.ToLower().Contains(Constants.DscPropertyTypeInt))
            {
                return content;
            }
            if (propertyType.ToLower().Contains(Constants.DscPropertyTypeBool))
            {
                return Constants.DecoratorVariableStarter + content.ToLower();
            }
            return content;
        }
    }
    public class RadioButtonContent
    {
        public string radioContent { get; set; }
        public bool radioIsChecked { get; set; }
        public string radioGroupName { get; set; }
        public RadioButtonContent(string content, bool isChecked, string propertyName)
        {
            radioContent = content;
            radioIsChecked = isChecked;
            radioGroupName = propertyName;
        }
    }
}
