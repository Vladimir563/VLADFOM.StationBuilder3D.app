using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VLADFOM.StationBuilder3D.Addin;

namespace StationBuilder3DAddin
{
    /// <summary>
    /// StationBuilder3D SolidWorks AddIn
    /// </summary>
    public class StationBuilder3DAddIn : ISwAddin
    {

        #region Private members

        /// <summary>
        /// The cookie to the current instance of SolidWorks we are running inside of
        /// </summary>
        private int mSwCookie;

        /// <summary>
        /// The taskpane view for our add-in
        /// </summary>
        private TaskpaneView mTaskPaneView;

        /// <summary>
        /// The UI control that is going to be inside the SolidWorks taskpane view
        /// </summary>
        private StationBuilder3DHostUI stationBuilder3dHost;

        /// <summary>
        /// The current instance of the SolidWorks application
        /// </summary>
        public SldWorks mSolidWorksApplication;

        #endregion

        #region Public members

        /// <summary>
        /// The unique Id to the taskpane used for registration in COM
        /// </summary>
        public const string SWTASKPANE_PROGID = "VladimirF.Solidworks.BlankAddin.Taskpane";

        #endregion

        #region SolidWorks add-in callbacks

        /// <summary>
        /// Called then SolidWorks has loaded our add-in and and wants us to do our connection logic
        /// </summary>
        /// <param name="ThisSW">The current SW instance</param>
        /// <param name="Cookie">The current SW cookie</param>
        /// <returns></returns>
        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            
            //Store a reference to the current SW instance
            mSolidWorksApplication = (SldWorks)ThisSW;
            //Store cookie id
            mSwCookie = Cookie;

            //Setup callback info
            var ok = mSolidWorksApplication.SetAddinCallbackInfo2(0, this, mSwCookie);

            //Create out UI
            LoadUI();

            //return ok
            return true;
        }

        /// <summary>
        /// Called then SolidWorks is about to unload our add-in and and wants us to do our disconnection logic
        /// </summary>
        /// <returns></returns>
        public bool DisconnectFromSW()
        {
            //Clean up our UI
            UnloadUI();

            //return ok
            return true;
        }

        #endregion

        #region Create UI

        /// <summary>
        /// Create our Taskpane and inject our HostUI
        /// </summary>
        private void LoadUI()
        {
            //Find location to our taskpane icon
            var imagePath = Path.Combine(Path.GetDirectoryName(typeof(StationBuilder3DAddIn).Assembly.CodeBase).Replace(@"file:\", string.Empty), "Options.bmp");

            //Create our Taskpane
            mTaskPaneView = mSolidWorksApplication.CreateTaskpaneView2(imagePath, "StationBuilder3DAddIn");

            //Load our UI into the taskpane
            stationBuilder3dHost = (StationBuilder3DHostUI)mTaskPaneView.AddControl(StationBuilder3DAddIn.SWTASKPANE_PROGID, string.Empty);
        }

        /// <summary>
        /// Cleanup the taskpane when we disconnect/unload
        /// </summary>
        private void UnloadUI()
        {
            stationBuilder3dHost = null;

            //Remove taskpane view
            mTaskPaneView.DeleteView();

            // Release COM reference and cleanup memory
            Marshal.ReleaseComObject(mTaskPaneView);

            mTaskPaneView = null;
        }

        #endregion

        #region COM registration

        /// <summary>
        /// The COM registration call to add our registry entires to the SolidWorks add-in registry
        /// </summary>
        /// <param name="t"></param>
        [ComRegisterFunction()]
        private static void ComRegister(Type t)
        {
            var keyPath = string.Format(@"SOFTWARE\SolidWorks\AddIns\{0:b}", t.GUID);

            //Create our registry folder for the add-in
            using (var rk = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath))
            {
                //Load add-in when SolidWorks opens
                rk.SetValue(null, 1);

                //Set SolidWorks title and description
                rk.SetValue("Title", "My swAdd-in");
                rk.SetValue("Descriptions", "All your pixels are belong to us. Just remeber this");
            }
        }

        /// <summary>
        /// The COM unregister call to remove our custom entires we added in the COM register function
        /// </summary>
        /// <param name="t"></param>
        [ComUnregisterFunction()]
        private static void COMUnregister(Type t)
        {
            var keyPath = string.Format(@"G:\SolidWorks2020\SOLIDWORKS\AddIns\{0:b}", t.GUID);

            //Remove our registry entry
            Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(keyPath);
        }

        #endregion


    }
}
