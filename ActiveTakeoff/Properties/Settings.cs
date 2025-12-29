using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan.Properties
{
    [CompilerGenerated]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance;

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool AllowMultipleInstances
        {
            get
            {
                return (bool)this["AllowMultipleInstances"];
            }
            set
            {
                this["AllowMultipleInstances"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public int AngleType
        {
            get
            {
                return (int)this["AngleType"];
            }
            set
            {
                this["AngleType"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("5")]
        [UserScopedSetting]
        public int BackupInterval
        {
            get
            {
                return (int)this["BackupInterval"];
            }
            set
            {
                this["BackupInterval"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("C:\\PrioSoft")]
        [UserScopedSetting]
        public string COfficePath
        {
            get
            {
                return (string)this["COfficePath"];
            }
            set
            {
                this["COfficePath"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string CompanyFullAddress
        {
            get
            {
                return (string)this["CompanyFullAddress"];
            }
            set
            {
                this["CompanyFullAddress"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string CompanyName
        {
            get
            {
                return (string)this["CompanyName"];
            }
            set
            {
                this["CompanyName"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string CompanyRepresentative
        {
            get
            {
                return (string)this["CompanyRepresentative"];
            }
            set
            {
                this["CompanyRepresentative"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool ConvertPDFToColor
        {
            get
            {
                return (bool)this["ConvertPDFToColor"];
            }
            set
            {
                this["ConvertPDFToColor"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string DataFolder
        {
            get
            {
                return (string)this["DataFolder"];
            }
            set
            {
                this["DataFolder"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool DBIsReadOnly
        {
            get
            {
                return (bool)this["DBIsReadOnly"];
            }
            set
            {
                this["DBIsReadOnly"] = value;
            }
        }

        public static Settings Default
        {
            get
            {
                return Settings.defaultInstance;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("<dotnetbarlayout version=\"6\" zorder=\"2,3,4,5\"> <docksite size=\"0\" dockingside=\"Top\" originaldocksitesize=\"0\" /> <docksite size=\"0\" dockingside=\"Bottom\" originaldocksitesize=\"0\" /> <docksite size=\"333\" dockingside=\"Left\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"189\"> <bar name=\"containerBarNavigation\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemPreview\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"350\"> <bar name=\"containerBarProperties\" dockline=\"0\" layout=\"2\" dockoffset=\"193\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemProperties\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"109\"> <bar name=\"containerBarLayers\" dockline=\"0\" layout=\"2\" dockoffset=\"527\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemLayers\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <docksite size=\"333\" dockingside=\"Right\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"423\"> <bar name=\"containerBarGroups\" dockline=\"0\" layout=\"2\" dockoffset=\"-1\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemGroups\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"208\"> <bar name=\"containerBarRecentPlans\" dockline=\"999\" layout=\"2\" dockoffset=\"427\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemRecentPlans\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <bars> <bar name=\"containerBarEstimating\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"2\" visible=\"true\" autohide=\"true\" dockwidth=\"548\"> <items> <item name=\"dockContainerItemEstimating\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </bars> </dotnetbarlayout>")]
        [UserScopedSetting]
        public string DefaultLayout
        {
            get
            {
                return (string)this["DefaultLayout"];
            }
            set
            {
                this["DefaultLayout"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("-1")]
        [UserScopedSetting]
        public int DefaultPrecision
        {
            get
            {
                return (int)this["DefaultPrecision"];
            }
            set
            {
                this["DefaultPrecision"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("-1")]
        [UserScopedSetting]
        public int DefaultSystemType
        {
            get
            {
                return (int)this["DefaultSystemType"];
            }
            set
            {
                this["DefaultSystemType"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        [UserScopedSetting]
        public bool EnableAutoBackup
        {
            get
            {
                return (bool)this["EnableAutoBackup"];
            }
            set
            {
                this["EnableAutoBackup"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("<dotnetbarlayout version=\"6\" zorder=\"2,3,4,5\"> <docksite size=\"0\" dockingside=\"Top\" originaldocksitesize=\"0\" /> <docksite size=\"0\" dockingside=\"Bottom\" originaldocksitesize=\"0\" /> <docksite size=\"333\" dockingside=\"Left\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"189\"> <bar name=\"containerBarNavigation\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemPreview\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"350\"> <bar name=\"containerBarProperties\" dockline=\"0\" layout=\"2\" dockoffset=\"193\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemProperties\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"109\"> <bar name=\"containerBarLayers\" dockline=\"0\" layout=\"2\" dockoffset=\"527\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemLayers\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <docksite size=\"333\" dockingside=\"Right\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"423\"> <bar name=\"containerBarGroups\" dockline=\"0\" layout=\"2\" dockoffset=\"-1\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemGroups\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"208\"> <bar name=\"containerBarRecentPlans\" dockline=\"999\" layout=\"2\" dockoffset=\"427\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemRecentPlans\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <bars> <bar name=\"containerBarEstimating\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"2\" visible=\"true\" autohide=\"true\" dockwidth=\"548\"> <items> <item name=\"dockContainerItemEstimating\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </bars> </dotnetbarlayout>")]
        [UserScopedSetting]
        public string EstimatingLayout
        {
            get
            {
                return (string)this["EstimatingLayout"];
            }
            set
            {
                this["EstimatingLayout"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public int ExportToExcelType
        {
            get
            {
                return (int)this["ExportToExcelType"];
            }
            set
            {
                this["ExportToExcelType"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0, 0")]
        [UserScopedSetting]
        public Point FiltersWindowPosition
        {
            get
            {
                return (Point)this["FiltersWindowPosition"];
            }
            set
            {
                this["FiltersWindowPosition"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0, 0")]
        [UserScopedSetting]
        public Size FiltersWindowSize
        {
            get
            {
                return (Size)this["FiltersWindowSize"];
            }
            set
            {
                this["FiltersWindowSize"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string ImagesFolder
        {
            get
            {
                return (string)this["ImagesFolder"];
            }
            set
            {
                this["ImagesFolder"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string ImportDBPricesFileName
        {
            get
            {
                return (string)this["ImportDBPricesFileName"];
            }
            set
            {
                this["ImportDBPricesFileName"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public int ImportDBPricesPricePosition
        {
            get
            {
                return (int)this["ImportDBPricesPricePosition"];
            }
            set
            {
                this["ImportDBPricesPricePosition"] = value;
            }
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        public char ImportDBPricesSeparator
        {
            get
            {
                return (char)this["ImportDBPricesSeparator"];
            }
            set
            {
                this["ImportDBPricesSeparator"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public int ImportDBPricesSkuPosition
        {
            get
            {
                return (int)this["ImportDBPricesSkuPosition"];
            }
            set
            {
                this["ImportDBPricesSkuPosition"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("172")]
        [UserScopedSetting]
        public int ImportDpi
        {
            get
            {
                return (int)this["ImportDpi"];
            }
            set
            {
                this["ImportDpi"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("172")]
        [UserScopedSetting]
        public int ImportManualDpi
        {
            get
            {
                return (int)this["ImportManualDpi"];
            }
            set
            {
                this["ImportManualDpi"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("fr")]
        [UserScopedSetting]
        public string InstallLanguage
        {
            get
            {
                return (string)this["InstallLanguage"];
            }
            set
            {
                this["InstallLanguage"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string LastSaved
        {
            get
            {
                return (string)this["LastSaved"];
            }
            set
            {
                this["LastSaved"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("50000000")]
        [UserScopedSetting]
        public double MaxPDFImportSize
        {
            get
            {
                return (double)this["MaxPDFImportSize"];
            }
            set
            {
                this["MaxPDFImportSize"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string MruList
        {
            get
            {
                return (string)this["MruList"];
            }
            set
            {
                this["MruList"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool Ortho
        {
            get
            {
                return (bool)this["Ortho"];
            }
            set
            {
                this["Ortho"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string PDFFolder
        {
            get
            {
                return (string)this["PDFFolder"];
            }
            set
            {
                this["PDFFolder"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public int PrintPlanType
        {
            get
            {
                return (int)this["PrintPlanType"];
            }
            set
            {
                this["PrintPlanType"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("Bleu ciel")]
        [UserScopedSetting]
        public string ReportTheme
        {
            get
            {
                return (string)this["ReportTheme"];
            }
            set
            {
                this["ReportTheme"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("30")]
        [UserScopedSetting]
        public int ScrollSpeed
        {
            get
            {
                return (int)this["ScrollSpeed"];
            }
            set
            {
                this["ScrollSpeed"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        [UserScopedSetting]
        public int SlopeType
        {
            get
            {
                return (int)this["SlopeType"];
            }
            set
            {
                this["SlopeType"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("<dotnetbarlayout version=\"6\" zorder=\"2,3,4,5\"> <docksite size=\"0\" dockingside=\"Top\" originaldocksitesize=\"0\" /> <docksite size=\"0\" dockingside=\"Bottom\" originaldocksitesize=\"0\" /> <docksite size=\"333\" dockingside=\"Left\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"189\"> <bar name=\"containerBarNavigation\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemPreview\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"350\"> <bar name=\"containerBarProperties\" dockline=\"0\" layout=\"2\" dockoffset=\"193\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemProperties\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"109\"> <bar name=\"containerBarLayers\" dockline=\"0\" layout=\"2\" dockoffset=\"527\" state=\"2\" dockside=\"1\" visible=\"true\"> <items> <item name=\"dockContainerItemLayers\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <docksite size=\"333\" dockingside=\"Right\" originaldocksitesize=\"0\"> <dockcontainer orientation=\"1\" w=\"0\" h=\"0\"> <barcontainer w=\"330\" h=\"423\"> <bar name=\"containerBarGroups\" dockline=\"0\" layout=\"2\" dockoffset=\"-1\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemGroups\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> <barcontainer w=\"330\" h=\"208\"> <bar name=\"containerBarRecentPlans\" dockline=\"999\" layout=\"2\" dockoffset=\"427\" state=\"2\" dockside=\"2\" visible=\"true\"> <items> <item name=\"dockContainerItemRecentPlans\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </barcontainer> </dockcontainer> </docksite> <bars> <bar name=\"containerBarEstimating\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"2\" visible=\"true\" autohide=\"true\" dockwidth=\"548\"> <items> <item name=\"dockContainerItemEstimating\" origBar=\"\" origPos=\"-1\" pos=\"0\" /> </items> </bar> </bars> </dotnetbarlayout>")]
        [UserScopedSetting]
        public string TakeoffLayout
        {
            get
            {
                return (string)this["TakeoffLayout"];
            }
            set
            {
                this["TakeoffLayout"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("Taxe 1")]
        [UserScopedSetting]
        public string Tax1Label
        {
            get
            {
                return (string)this["Tax1Label"];
            }
            set
            {
                this["Tax1Label"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public double Tax1Rate
        {
            get
            {
                return (double)this["Tax1Rate"];
            }
            set
            {
                this["Tax1Rate"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("Taxe 2")]
        [UserScopedSetting]
        public string Tax2Label
        {
            get
            {
                return (string)this["Tax2Label"];
            }
            set
            {
                this["Tax2Label"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        [UserScopedSetting]
        public double Tax2Rate
        {
            get
            {
                return (double)this["Tax2Rate"];
            }
            set
            {
                this["Tax2Rate"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool TaxOnTax
        {
            get
            {
                return (bool)this["TaxOnTax"];
            }
            set
            {
                this["TaxOnTax"] = value;
            }
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        public Color ThemeColor
        {
            get
            {
                return (Color)this["ThemeColor"];
            }
            set
            {
                this["ThemeColor"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("-1")]
        [UserScopedSetting]
        public int ThemeStyle
        {
            get
            {
                return (int)this["ThemeStyle"];
            }
            set
            {
                this["ThemeStyle"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string UICulture
        {
            get
            {
                return (string)this["UICulture"];
            }
            set
            {
                this["UICulture"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        [UserScopedSetting]
        public bool UpdateSettings
        {
            get
            {
                return (bool)this["UpdateSettings"];
            }
            set
            {
                this["UpdateSettings"] = value;
            }
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        public Point WindowPosition
        {
            get
            {
                return (Point)this["WindowPosition"];
            }
            set
            {
                this["WindowPosition"] = value;
            }
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        public Size WindowSize
        {
            get
            {
                return (Size)this["WindowSize"];
            }
            set
            {
                this["WindowSize"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("Normal")]
        [UserScopedSetting]
        public FormWindowState WindowState
        {
            get
            {
                return (FormWindowState)this["WindowState"];
            }
            set
            {
                this["WindowState"] = value;
            }
        }

        static Settings()
        {
            Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
        }

        public Settings()
        {
        }

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }
    }
}