using DatabaseInterface;
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
using static DatabaseInterface.RullieraDB;

namespace GalimbertiHMIgl
{
    public class CycleHundegger
    {

        private PLC plcRulliera;
        PostgresLog log = new PostgresLog();

      

        public void init(PLC plc)
        {

            this.plcRulliera = plc;

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

        private void _watcher_Created(Event ev)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            String ordiginKey = node.Attributes["OriginKey"]?.InnerText;

            log.LogRulliera("DEBUG_PART_FINISHED", "0", ev._id.ToString(), new TimeSpan());


            if (x < 600)
            {
                log.LogRulliera("DEBUG_PART_BUCA", "0",ev._id.ToString(), new TimeSpan());
                return;
            }
               

            int rotation = 99;
            if (x > 1600)
            {
                log.LogRulliera("DEBUG_PART_CALCOLO_ROTAZIONE", "0", ev._id.ToString(), new TimeSpan());
                rotation = getRotation(doc, ordiginKey);
            }

            bool invertYZ = false;
            var nesting = this.rullieraDB.getNesting(ordiginKey);
            if (nesting != null)
            {
                var result = rotate(new Vector3(0, 0, 1), new Vector3(nesting.x, nesting.y, nesting.z));
                if ( Math.Abs(Vector3.Dot(result, new Vector3(0, 0, 1))) > 0.9)
                {
                    invertYZ = false;
                }else
                {
                    invertYZ = true;
                }
            }

            this.plcRulliera.doWithPLC(c =>
            {
                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", rotation);

                if  (invertYZ)
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", y);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", z);
                }
                else
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", z);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", y);
                }

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", true);

            });

        }

        RullieraDB rullieraDB = new RullieraDB();
        public void processDB()
        {
           foreach(Event ev in rullieraDB.getEventsToProcess())
           {
                try
                {
                    if (ev.event_type == "N")
                    {
                        this._watcher_Created_Nesting(ev);

                    }
                    else if (ev.event_type == "1")
                    {
                        this._watcher_Created_Anticipo(ev);
                    }
                    else if (ev.event_type == "7")
                    {
                        this._watcher_Created(ev);
                    }

                    this.rullieraDB.markAsProcessed(ev);
                } catch(Exception ex)
                {
                    log.LogRulliera("DEBUG_ERROR", "0", ex.Message, new TimeSpan());

                }

            }
        }


        private void _watcher_Created_Nesting(Event ev)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            var nodes = doc.DocumentElement.SelectNodes("//PartReference");

            log.LogRulliera("DEBUG_NESTING", "0", ev._id.ToString(), new TimeSpan());

            foreach (XmlNode node in nodes)
            {
                String referenceKey = node.Attributes["ReferenceKey"]?.InnerText;
                if (referenceKey != null)
                {
                    var v = new Vector3();
                    v.X = (float) Double.Parse(node.Attributes["RotationX"]?.InnerText.Replace(".", "."));
                    v.Y = (float)Double.Parse(node.Attributes["RotationY"]?.InnerText.Replace(".", "."));
                    v.Z = (float)Double.Parse(node.Attributes["RotationZ"]?.InnerText.Replace(".", "."));
                    rullieraDB.logNesting(referenceKey, (int)v.X, (int)v.Y, (int)v.Z);

                    log.LogRulliera("DEBUG_NESTING_" + referenceKey, "0", v.X + " " + v.Y + " " + v.Z, new TimeSpan());

                }
            }

        }

        private void _watcher_Created_Anticipo(Event ev)
        {
          
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");

            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            if (x < 600)
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

                    log.LogRulliera("DEBUG_PART_"+ key, "0", "ROTATION FRAME part " +  frame.ToString(), new TimeSpan());

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
                    var nesting = this.rullieraDB.getNesting(key);
                    if (nesting != null)
                    {
                        log.LogRulliera("DEBUG_PART_" + key, nesting.id, "FINDED ROTATION", new TimeSpan());

                        ret = calculateFinalRotation(reference, new Vector3(nesting.x,nesting.y,nesting.z));
                    }else
                    {
                        log.LogRulliera("DEBUG_PART_" + key, "0", "ROTATION NOT FINDED!", new TimeSpan());

                        ret = calculateFinalRotation(reference, new Vector3(0,0,0));

                    }
                    log.LogRulliera("DEBUG_PART_" + key, "0", "ROTATION PLC RESULT is " + ret, new TimeSpan());

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

        public Vector3 rotate(Vector3 point, Vector3 rotation)
        {
            if (rotation != null)
            {
                point = Vector3.Transform(point, Matrix4x4.CreateRotationX(ConvertToRadians(rotation.X)));
                point = Vector3.Transform(point, Matrix4x4.CreateRotationY(ConvertToRadians(rotation.Y)));
                point = Vector3.Transform(point, Matrix4x4.CreateRotationZ(ConvertToRadians(rotation.Z)));
            }
            return point;
        }

        public int calculateFinalRotation(Vector3 point, Vector3 rotation)
        {
            try
            {

                point = rotate(point, rotation);

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

                log.LogRulliera("DEBUG_ROTATION_CALC" , "0", "ROTATION FRAME CALCULATED " + resultFrame, new TimeSpan());


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
