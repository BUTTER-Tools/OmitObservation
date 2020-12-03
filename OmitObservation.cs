using OutputHelperLib;
using PluginContracts;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace OmitObservation
{
    public class OmitObs : Plugin
    {


        public string[] InputType { get; } = { "Tokens" };
        public string OutputType { get; } = "Tokens";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "TokenizedText" } };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Omit Observations";
        public string PluginType { get; } = "Preprocessing";
        public string PluginVersion { get; } = "1.0.1";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "This plugin will remove any observation that has fewer than N tokens, where N is defined by the user.";
        public bool TopLevel { get; } = false;
        public string PluginTutorial { get; } = "Coming Soon";


        private int FilterMin { get; set; } = 25;


        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion



        public void ChangeSettings()
        {

            using (var form = new SettingsForm_OmitObservation(FilterMin))
            {

                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;

                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {

                    FilterMin = form.FilterMin;
                }
            }

        }





        public Payload RunPlugin(Payload Input)
        {


            Payload pData = new Payload();
            pData.FileID = Input.FileID;
            pData.Type = Input.Type;

            for (int counter = 0; counter < Input.StringArrayList.Count; counter++)
            {
                
                if (Input.StringArrayList[counter].Length >= FilterMin)
                {
                    pData.StringArrayList.Add(Input.StringArrayList[counter]);
                    
                    if (Input.SegmentID.Count > 0) pData.SegmentID.Add(Input.SegmentID[counter]);
                    pData.SegmentNumber.Add(Input.SegmentNumber[counter]);
                }
                
            }

            return (pData);

        }



        public void Initialize() { }


        public bool InspectSettings()
        {
            return true;
        }


        public Payload FinishUp(Payload Input)
        {
            return (Input);
        }




        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            FilterMin = Int32.Parse(SettingsDict["FilterMin"]);
        }

        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();
            SettingsDict.Add("FilterMin", FilterMin.ToString());
            return (SettingsDict);
        }
        #endregion



    }








}



