using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class ComponentsLocationPaths
    {
        public Dictionary<string, string> componentsLocationPaths = new Dictionary<string, string>();

        public ComponentsLocationPaths()
        {
            componentsLocationPaths = PathsInitialize(componentsLocationPaths);
        }

        public static Dictionary<string, string> PathsInitialize(Dictionary<string, string> componentsLocationPaths)
        {
            string[] pathNames = {
                "assemblyPatternLocationPath",
                "mainDirPath",
                "pumpsPath",
                "mainPumpsPath",
                "controlCabinetsPath",
                "lockValvesPath",
                "shuttersPath",
                "carvesValvesPath",
                "checkValvesPath",
                "flangeCheckValvesPath",
                "carveCheckValvesPath",
                "collectorsPath",
                "pressureCollectorsPath",
                "suctionCollectorsPath",
                "teesPath",
                "suctionTeesPath",
                "pressureTeesPath",
                "coilsPath",
                "concentricCoilsPath",
                "concentricCoilsWithNippelPath",
                "essentricCoilsPath",
                "essentricCoilsPathWithNippel",
                "simpleCoilsPath",
                "simpleCoilsWithNippelPath",
                "jockeySuctionCoils",
                "jockeyPressureCoils",
                "framesPath",
                "weldedFramesPath",
                "framesFromShvellerPath",
                "flangesWithReley",
                "flangeOnCarveTransitionPath",
                "nippelsPath",
                "screwFittingsPath"
            };

            XmlNode attr;

            XmlDocument xDoc = new XmlDocument();

            var xmlSettingsLocation = Path.Combine(Path.GetDirectoryName(typeof(PumpStation).Assembly.CodeBase).Replace(@"file:\", string.Empty).Replace(@"bin\", string.Empty).Replace(@"Debug", string.Empty), "PARTS_PATHS.xml");

            Debug.Print(xmlSettingsLocation);

            xDoc.Load(xmlSettingsLocation);

            // get the root element
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode node in xRoot)
            {
                for (int i = 0; i < pathNames.Length; i++)
                {
                    if (node.Name.Equals(pathNames[i]))
                    {
                        attr = node.Attributes.GetNamedItem("name");
                        componentsLocationPaths.Add(pathNames[i], attr.Value);
                    }
                }
            }
            return componentsLocationPaths;
        }
    }
}
