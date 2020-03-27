using PLCDrivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GalimbertiHMIgl
{
    public class CycleHundegger
    {

        private PLC plcRulliera;


        public void  cleanFiles()
        {
            var files = new DirectoryInfo(ConfigurationSettings.AppSettings.Get("Folder")).GetFiles("*.xml");
            foreach (var file in files)
            {
                if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(1))
                {
                    File.Delete(file.FullName);
                }
            }
        }



        public void init(PLC plc)
        {

            this.plcRulliera = plc;

            try
            {
                var _watcher_nesting = new FileSystemWatcher();
                _watcher_nesting.Path = ConfigurationSettings.AppSettings.Get("Folder");
                _watcher_nesting.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                _watcher_nesting.Filter = "*_N.xml";
                _watcher_nesting.Created += _watcher_Created_Nesting;
                _watcher_nesting.Error += new ErrorEventHandler((x, y) => Console.WriteLine("Error"));
                _watcher_nesting.EnableRaisingEvents = true;

                var _watcher = new FileSystemWatcher();
                _watcher.Path = ConfigurationSettings.AppSettings.Get("Folder");
                _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                _watcher.Filter = "*_7.xml";
                _watcher.Created += _watcher_Created;
                _watcher.Error += new ErrorEventHandler((x, y) => Console.WriteLine("Error"));
                _watcher.EnableRaisingEvents = true;

                var _watcher_anticipo = new FileSystemWatcher();
                _watcher_anticipo.Path = ConfigurationSettings.AppSettings.Get("Folder");
                _watcher_anticipo.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                _watcher_anticipo.Filter = "*_1.xml";
                _watcher_anticipo.Created += _watcher_Created_Anticipo;
                _watcher_anticipo.Error += new ErrorEventHandler((x, y) => Console.WriteLine("Error"));
                _watcher_anticipo.EnableRaisingEvents = true;

            } catch (Exception ex)
            {

            }





            this.plcRulliera.pollActions.Add(
                () => {
                    this.plcRulliera.doWithPLC(c =>
                    {
                        bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");
                        bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Anticipo_Pz_Hundegger");

                        if (presenza && presenzaAck)
                        {
                            c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", false);
                        }
                    });
                }
            );

            this.plcRulliera.pollActions.Add(
                () => {
                    this.plcRulliera.doWithPLC(c =>
                    {

                        bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger");
                        bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Scarico_Hundegger");
                        if (presenza && presenzaAck)
                        {
                            c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", false);
                        }
                    });
                }
            );
        }

        bool prev_WR_En_Anticipo_Hundegger = false;

        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {

            while (!IsFileReady(e.FullPath)) ;

            XmlDocument doc = new XmlDocument();
            doc.Load(e.FullPath);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            //Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".",","));
            //Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", ","));
            //Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", ","));

            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            String ordiginKey = node.Attributes["OriginKey"]?.InnerText;


            if (x < 500)
                return;

            getRotation(doc, ordiginKey);

            this.plcRulliera.doWithPLC(c =>
            {
                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", y);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", z);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", getRotation(doc, ordiginKey));

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", true);

            });

        }


        public Dictionary<String, Vector3> rotations = new Dictionary<string, Vector3>();
        private void _watcher_Created_Nesting(object sender, FileSystemEventArgs e)
        {

            while (!IsFileReady(e.FullPath)) ;

            XmlDocument doc = new XmlDocument();
            doc.Load(e.FullPath);

            var nodes = doc.DocumentElement.SelectNodes("//PartReference");

            foreach (XmlNode node in nodes)
            {
                var v = new Vector3();
                v.X = (float) Double.Parse(node.Attributes["RotationX"]?.InnerText.Replace(".", "."));
                v.Y = (float)Double.Parse(node.Attributes["RotationY"]?.InnerText.Replace(".", "."));
                v.Z = (float)Double.Parse(node.Attributes["RotationZ"]?.InnerText.Replace(".", "."));
                String referenceKey = node.Attributes["ReferenceKey"]?.InnerText;

                if (rotations.ContainsKey(referenceKey))
                    rotations.Remove(referenceKey);

                rotations.Add(referenceKey, v);
            }

            var files = new DirectoryInfo(ConfigurationSettings.AppSettings.Get("Folder")).GetFiles("*.xml");


        }

        private void _watcher_Created_Anticipo(object sender, FileSystemEventArgs e)
        {
            while (!IsFileReady(e.FullPath)) ;

            XmlDocument doc = new XmlDocument();
            doc.Load(e.FullPath);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            // Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", ","));
            // Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", ","));
            // Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", ","));


            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            if (x < 500)
                return;


            this.plcRulliera.doWithPLC(c =>
            {
                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Anticipo_Pz_Da_Hundegger", y);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Anticipo_Pz_Da_Hundegger", z);

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", true);

            });

        }


        public int getRotation(XmlDocument doc, String key)
        {
            try
            {
                var nodes = doc.DocumentElement.SelectNodes("//StandardFrame[@Attributes='VisibleFace']");

                if (nodes.Count > 0)
                {

                    int frame = int.Parse(nodes[0].Attributes["FrameId"].InnerText);
                    Vector3 reference = new Vector3();

                    if (frame == 1)
                    {
                        reference = new Vector3(0, 0, 1);
                    }
                    if (frame == 2)
                    {
                        reference = new Vector3(0, 1, 0);
                    }
                    if (frame == 3)
                    {
                        reference = new Vector3(0, 0, -1);
                    }
                    if (frame == 4)
                    {
                        reference = new Vector3(0, -1, 0);
                    }

                    int ret = 99;
                    if (this.rotations.ContainsKey(key))
                    {
                        ret = calculateFinalRotation(reference, this.rotations[key]);
                        rotations.Remove(key);
                    }else
                    {
                        ret = calculateFinalRotation(reference, new Vector3(0,0,0));
                    }
                    return ret;
                }
            }
            catch (Exception ex)
            {

            }
            return 99;
        }

        public float ConvertToRadians(float angle)
        {
            return (float) (Math.PI / 180) * angle;
        }

        public int calculateFinalRotation(Vector3 point, Vector3 rotation)
        {
            try
            {

                if (rotation != null)
                {
                    point = Vector3.Transform(point, Matrix4x4.CreateRotationX(ConvertToRadians(rotation.X)));
                    point = Vector3.Transform(point, Matrix4x4.CreateRotationY(ConvertToRadians(rotation.Y)));
                    point = Vector3.Transform(point, Matrix4x4.CreateRotationZ(ConvertToRadians(rotation.Z)));
                }


                int resultFrame = 0;

                if ( Vector3.Dot(point, new Vector3(0,0,1)) > 0.9)
                {
                    resultFrame = 1;
                }
                if (Vector3.Dot(point, new Vector3(0, 0, -1)) > 0.9)
                {
                    resultFrame = 3;
                }
                if (Vector3.Dot(point, new Vector3(0, 1, 0)) > 0.9)
                {
                    resultFrame = 2;
                }
                if (Vector3.Dot(point, new Vector3(0, -1, 0)) > 0.9)
                {
                    resultFrame = 4;
                }

              
                String setting = ConfigurationSettings.AppSettings.Get("FrameId" + resultFrame);
                return int.Parse(setting);
               
            }
            catch (Exception ex)
            {

            }
            return 99;
        }

        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
